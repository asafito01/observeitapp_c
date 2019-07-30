using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObserveItApp.Models
{
        public class AppointmentModel
        {
                public TimeSpan StartHour { get; set; }
                public TimeSpan EndHour { get; set; }
                public UserModel User { get; set; } = null;
        }
}