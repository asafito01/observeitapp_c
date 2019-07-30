using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObserveItApp.Models;

namespace ObserveItApp.Services
{
        public class SchedulerService : ISchedulerService
        {
                const int ActiveHoursStart = 8;
                const int ActiveHoursEnd = 17;

                private SchedulerModel Scheduler;
                private static readonly object LockObject = new object();

                /// <summary>
                /// Constructor
                /// </summary>
                /// <param name="_Scheduler"></param>
                public SchedulerService(SchedulerModel _Scheduler)
                {
                        Scheduler = _Scheduler;
                }

                /// <summary>
                ///  Gets the active hours of the scheduler
                /// </summary>
                /// <returns></returns>
                public int[] GetSchedulerActiveHours()
                {
                        return new[] { ActiveHoursStart, ActiveHoursEnd };
                }

                /// <summary>
                /// Gets the lists of the current appointments
                /// </summary>
                /// <returns>A list of appointments, otherwise null
                /// (The response object sould be a class variable that contains all the response data, instead of just returning the data or null if failed. The object should contain the status of the operation with a propper message for each situation and a data variable if it is available)
                /// </returns>
                public List<AppointmentModel> GetAppointments()
                {
                        try
                        {
                                // Thread safety lock when reading the data
                                lock (LockObject)
                                {
                                        return Scheduler.Appointments;
                                }
                        }
                        catch (Exception ex)
                        {
                                return null;
                        }
                }

                /// <summary>
                /// Creates a new appointment into the scheduler from a given appointment data object
                /// </summary>
                /// <param name="appointmentData"></param>
                /// <returns>A new appointment object if succeeded, otherwise null
                /// (The response object sould be a class variable that contains all the response data, instead of just returning the data or null if failed. The object should contain the status of the operation with a propper message for each situation and a data variable if it is available)
                /// </returns>
                public AppointmentModel CreateAppointment(AppointmentModel appointmentData)
                {
                        try
                        {
                                // Thread safety lock when writing the data
                                lock (LockObject)
                                {
                                        // Create new appointment object
                                        AppointmentModel newAppointmentData = new AppointmentModel();

                                        // Check if the active hours are correct.
                                        // If the start hour is bigger or equal than the end hour return null
                                        newAppointmentData.StartHour = appointmentData.StartHour;
                                        newAppointmentData.EndHour = appointmentData.EndHour;
                                        if (newAppointmentData.StartHour >= newAppointmentData.EndHour)
                                                return null;

                                        // If no user was received, then return error (nothing in our case)
                                        if (string.IsNullOrEmpty(appointmentData.User.Name))
                                                return null;

                                        // Check if a user already exists, if not add it, otherwise get it.
                                        UserModel foundUser = Scheduler.Users.FirstOrDefault(x => x.Name == appointmentData.User.Name);
                                        if (foundUser == null)
                                        {
                                                // Add a new user into the scheduler users list
                                                foundUser = new UserModel() { Name = appointmentData.User.Name };
                                                Scheduler.Users.Add(foundUser);
                                        }
                                        newAppointmentData.User = foundUser; // Set the user to the appointment 

                                        // Add the new appointment to the scheduler
                                        Scheduler.Appointments.Add(newAppointmentData);
                                        return newAppointmentData;
                                }
                        }
                        catch (Exception ex)
                        {
                                return null;
                        }
                }


        }
}