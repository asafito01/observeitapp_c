using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObserveItApp.ViewModels
{
        /// <summary>
        /// This object type contains the data that is needed to be displayed in the client
        /// </summary>
        public class SchedulerRowViewModel
        {
                public TimeSpan StartHour { get; set; }
                public TimeSpan EndHour { get; set; }
                public List<string> Participants { get; set; } = new List<string>();

                /// <summary>
                ///  Calculated value. Gets the participant count by checking the participants list and returning the count of the list if it is not null
                /// </summary>
                public int ParticipantsCount
                {
                        get
                        {
                                // If the list is not null, return the list item count, otherwise return 0
                                if (Participants != null)
                                        return Participants.Count;
                                return 0;
                        }
                }
        }
}