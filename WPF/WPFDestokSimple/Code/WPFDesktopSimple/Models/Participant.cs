using System;
using System.Xml.Serialization;

namespace WPFDesktopSimple.Models
{
    [Serializable()]
    public class Participant
    {
        public Person PersonInfo;
        public int Id { get; set; }

        public Participant() { }
        public Participant(Participant p)
        {
            PersonInfo = p.PersonInfo;
            Id = p.Id;
        }

        [XmlIgnore]
        public string Name
        {
            get { return PersonInfo.Name; }
        }

        [XmlIgnore]
        public string Country
        {
            get { return PersonInfo.Country; }
        }
    }
}
