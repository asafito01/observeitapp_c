using ObserveItApp.Models;
using ObserveItApp.ViewModels;
using ObserveItApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ObserveItApp.Controllers
{
        [RoutePrefix("api/v1/scheduler")]
        public class SchedulerController : ApiController
        {
                private readonly ISchedulerService SchedulerService;

                public SchedulerController(ISchedulerService _SchedulerService)
                {
                        // Initiate scheduler service
                        if (_SchedulerService == null)
                                throw new ArgumentNullException(nameof(_SchedulerService));
                        SchedulerService = _SchedulerService;
                }

                /// <summary>
                /// Gets all the scheduler data
                /// </summary>
                /// <returns></returns>
                [HttpGet]
                [Route("")]
                public IHttpActionResult GetAll()
                {
                        // Ask the scheduler service for the appointment list. If not found
                        List<AppointmentModel> appointmentList = SchedulerService.GetAppointments();
                        if (appointmentList == null)
                                return NotFound();

                        // Create the final datato be returned
                        List<SchedulerRowViewModel> schedulerRows = new List<SchedulerRowViewModel>();

                        // Ask the scheduler service for the schedule active hours
                        // Loop the hours from the active start hour (one hour after because we check the previous hour) to the end active hour
                        int[] activeHours = SchedulerService.GetSchedulerActiveHours();
                        for (int hour = activeHours[0] + 1; hour <= activeHours[1]; hour++)
                        {
                                // Get the current hour range from hour - 1 to hour
                                var startHour = new TimeSpan(hour - 1, 0, 0);
                                var endHour = new TimeSpan(hour, 0, 0);

                                // Set a scheduler row object to insert the data
                                SchedulerRowViewModel scheduleRow = new SchedulerRowViewModel();
                                scheduleRow.StartHour = startHour;
                                scheduleRow.EndHour = endHour.Subtract(new TimeSpan(0, 1, 0));

                                // Loop all the appointments available in these range of hours and add the user if not added already
                                foreach (var appointmentData in appointmentList.Where(x => (startHour >= x.StartHour & startHour < x.EndHour)).ToList())
                                {
                                        // If the user is not assigned to the appointment in these hours, add it as a participant
                                        if (!scheduleRow.Participants.Any(x => x == appointmentData.User.Name))
                                                scheduleRow.Participants.Add(appointmentData.User.Name);
                                }

                                // Add it to the rows
                                schedulerRows.Add(scheduleRow);
                        }
                        return Ok(schedulerRows);
                }

                /// <summary>
                /// Creates a new appointment
                /// </summary>
                /// <param name="appointmentData"></param>
                /// <returns></returns>
                [HttpPost]
                [Route("")]
                public IHttpActionResult PostAppointment([FromBody] AppointmentModel appointmentData)
                {
                        // Ask the scheduler service to create a new appointment from the received data
                        // If succeded get the appointment data, otherwise return a bad request
                        AppointmentModel newAppointmentData = SchedulerService.CreateAppointment(appointmentData);

                        /// The response object sould be a class variable that contains all the response data, instead of just returning the data or null if failed.
                        /// The object should contain the status of the operation with a propper message for each situation and a data variable if it is available
                        if (newAppointmentData == null)
                                return BadRequest("An error ocurred. Could not insert");

                        return Ok(newAppointmentData);
                }
        }

}