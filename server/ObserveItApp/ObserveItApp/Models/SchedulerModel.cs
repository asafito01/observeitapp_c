using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObserveItApp.Models
{
        public class SchedulerModel
        {
                public int ActiveHoursStart { get; set; }
                public int ActiveHoursEnd { get; set; }
                public List<AppointmentModel> Appointments { get; set; } = new List<AppointmentModel>();
                public List<UserModel> Users { get; set; } = new List<UserModel>();

                public SchedulerModel(int _ActiveHoursStart, int _ActiveHoursEnd)
                {
                        ActiveHoursStart = _ActiveHoursStart;
                        ActiveHoursEnd = _ActiveHoursEnd;
                }
        }
}