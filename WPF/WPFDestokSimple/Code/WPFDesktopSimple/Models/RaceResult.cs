using System;
using System.Collections.Generic;

namespace WPFDesktopSimple.Models
{
    [Serializable()]
    public class RaceResult
    {
        public List<ParticipantResult> participantResults = new List<ParticipantResult>();
        public DateTime StartTime;
    }
}
