using System.Xml.Serialization;
using XMLData.People.Nodes;

namespace XMLData.People
{
    [XmlRoot("people")]
    public class Document : IDocument
    {
        public static IDocument New()
        {
            return new Document();
        }

        // XML document objects. Child nodes are expressed as member objects.
        [XmlElement("person")]
        public readonly List<Person> people;

        public Document()
        {
            people = [];
        }

        // Populates member list people to contain all instances of xml document objects.
        public void Populate(StreamReader fs)
        {
            if (fs == null)
            {
                throw new Exception("No file stream provided!");
            }

            // Read and parse all lines from the file.
            string? line = fs.ReadLine();
            while (line != null)
            {
                var data = line.Split('|');
                var t = data[0];

                // In case input document contains lines not preceded by "P" assume these belong to unknown person.
                Person person = new();
                if (t == "P")
                {
                    person = ParsePerson(data);
                }

                line = fs.ReadLine();
                while (line != null)
                {
                    data = line.Split('|');
                    t = data[0];
                    if (t == "T")
                    {
                        person.Phone = ParsePhone(data);
                    }
                    else if (t == "A")
                    {
                        person.Address = ParseAddress(data);
                    }
                    else if (t == "F")
                    {
                        var family = ParseFamily(data);
                        while ((line = fs.ReadLine()) != null)
                        {
                            data = line.Split('|');
                            t = data[0];
                            if (t == "T")
                            {
                                family.Phone = ParsePhone(data);
                            }
                            else if (t == "A")
                            {
                                family.Address = ParseAddress(data);
                            }
                            else if (t == "F")
                            {
                                // New family node. Add currently parsed then construct new.
                                person.Family.Add(family);
                                family = ParseFamily(data);
                            }
                            else
                            {
                                person.Family.Add(family);
                                break;
                            }
                        }
                    }

                    // If on a new person node update current person object. 
                    if (t == "P")
                    {
                        people.Add(person);
                        person = ParsePerson(data);
                    }

                    line = fs.ReadLine();
                }

                // Add final person node.
                people.Add(person);
            }
        }

        static Person ParsePerson(string[] data)
        {
            return new Person
            {
                FirstName = data[1],
                LastName = data[2]
            };
        }

        static Phone ParsePhone(string[] data)
        {
            return new Phone
            {
                Mobile = data[1],
                Landline = data[2]
            };
        }

        static Address ParseAddress(string[] data)
        {
            var address = new Address
            {
                Street = data[1],
                City = data[2]
            };

            if (data.Length > 3)
            {
                address.Zip = data[3];
            }

            return address;
        }

        static Family ParseFamily(string[] data)
        {
            return new Family
            {
                Name = data[1],
                Born = data[2]
            };
        }
    }
}