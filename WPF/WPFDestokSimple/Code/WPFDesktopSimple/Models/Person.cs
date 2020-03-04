using System;
using System.Xml.Serialization;

namespace WPFDesktopSimple.Models
{
    [Serializable()]
    public class Person
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("country")]
        public string Country { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlIgnore]
        public bool Selected { get; set; }

        public Person() { }
    }
}
