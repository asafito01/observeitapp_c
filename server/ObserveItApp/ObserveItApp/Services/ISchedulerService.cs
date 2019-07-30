using ObserveItApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserveItApp.Services
{
        public interface ISchedulerService
        {
                int[] GetSchedulerActiveHours();
                List<AppointmentModel> GetAppointments();
                AppointmentModel CreateAppointment(AppointmentModel appointmentData);
        }
}
