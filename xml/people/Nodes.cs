using System.Xml;
using System.Xml.Serialization;

namespace XMLData.People.Nodes
{
    public class Person
    {
        public Person()
        {
            Family = [];
        }

        [XmlElement("firstname")]
        public string FirstName = "";

        [XmlElement("lastname")]
        public string LastName = "";

        [XmlElement("phone")]
        public Phone? Phone;

        [XmlElement("address")]
        public Address? Address;

        [XmlElement("family")]
        public List<Family?> Family;
    }

    public class Phone
    {
        [XmlElement("mobile")]
        public string Mobile = "";

        [XmlElement("landline")]
        public string Landline = "";
    }

    public class Family
    {
        [XmlElement("name")]
        public string Name = "";

        [XmlElement("born")]
        public string? Born;

        [XmlElement("phone")]
        public Phone? Phone;

        [XmlElement("address")]
        public Address? Address;
    }

    public class Address
    {
        [XmlElement("street")]
        public string Street = "";

        [XmlElement("city")]
        public string City = "";

        [XmlElement("zip")]
        public string? Zip;
    }
}