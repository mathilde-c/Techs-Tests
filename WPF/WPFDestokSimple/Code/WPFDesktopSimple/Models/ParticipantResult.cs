
using System;
using System.Xml.Serialization;
using ThirdPartySimulator;

namespace WPFDesktopSimple.Models
{
    [Serializable()]
    public class ParticipantResult : Participant
    {
        [XmlElement("NetTime")]
        public double? NetTime;

        [XmlElement("Irm")]
        public EndingEventType Irm;

        public ParticipantResult() { }
        public ParticipantResult(Participant participant, EndingEventType irm, double? netTime = null) : base(participant)
        {
            NetTime = netTime;
            Irm = irm;
        }

        [XmlIgnore]
        public string NetTimeString
        {
            get { return NetTime.HasValue ? NetTime.Value.ToString() : string.Empty; }
        }

        [XmlIgnore]
        public string IrmString
        {
            get { return Irm == EndingEventType.None ? string.Empty : Irm.ToString("g"); }
        }
    }
}
