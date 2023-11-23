using System.Xml;

namespace laab2
{
    public class XMLDataHandlers
    {
        public List<MainPageViewModel.StudentParliament> students;

        XmlDocument document;

        public XMLDataHandlers()
        {
            document = new XmlDocument();
            document.Load(@"/Users/yanayalovska/Projects/laab2/laab2/assets/data.xml");
            students = new List<MainPageViewModel.StudentParliament>();
            XmlNodeList nodes = document.GetElementsByTagName("event");

            foreach (XmlNode node in nodes)
            {
                ReadEventData(node);
            }
        }

        private void ReadEventData(XmlNode node)
        {
            string name = node.SelectSingleNode("name")?.InnerText ?? "";
            string faculty = node.SelectSingleNode("faculty")?.InnerText ?? "";
            string department = node.SelectSingleNode("department")?.InnerText ?? "";
            string specialty = node.SelectSingleNode("specialty")?.InnerText ?? "";
            string timing = node.SelectSingleNode("timing")?.InnerText ?? "";

            students.Add(new MainPageViewModel.StudentParliament
            {
                Name = "Name: " + name,
                Faculty = "Faculty: " + faculty,
                Department = "Department: " + department,
                Specialty = "Specialty: " + specialty,
                Timing = "Timing: " + timing,
            });
        }

        public List<MainPageViewModel.StudentParliament> DOM_Search(MainPageViewModel.StudentParliament @event)
        {
            students.Clear();
            using (XmlReader reader = XmlReader.Create(@"/Users/yanayalovska/Projects/laab2/laab2/assets/data.xml"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "students_parliament")
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "event")
                            {
                                ReadStudentData(reader, @event);
                            }
                        }
                    }
                }
            }
            return students;
        }

        private void ReadStudentData(XmlReader reader, MainPageViewModel.StudentParliament @event)
        {
            string name = "";
            string faculty = "";
            string department = "";
            string specialty = "";
            string timing = "";

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "name":
                            name = reader.ReadElementContentAsString();
                            break;
                        case "faculty":
                            faculty = reader.ReadElementContentAsString();
                            break;
                        case "department":
                            department = reader.ReadElementContentAsString();
                            break;
                        case "specialty":
                            specialty = reader.ReadElementContentAsString();
                            break;
                        case "timing":
                            timing = reader.ReadElementContentAsString();
                            break;
                    }
                }

                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "event")
                {
                    if (!faculty.Contains(@event.Faculty)) break;
                    if (!department.Contains(@event.Department)) break;
                    if (!name.Contains(@event.Name)) break;
                    if (!specialty.Contains(@event.Specialty)) break;
                    if (!timing.Contains(@event.Timing)) break;

                    students.Add(new MainPageViewModel.StudentParliament
                    {
                        Name = "Name: " + name,
                        Faculty = "Faculty: " + faculty,
                        Department = "Department: " + department,
                        Specialty = "Specialty: " + specialty,
                        Timing = "Timing: " + timing,
                    });
                    break;
                }
            }
        }

        public List<MainPageViewModel.StudentParliament> SAX_Search(MainPageViewModel.StudentParliament student)
        {
            return DOM_Search(student);
        }

        public XmlDocument CreateXmlDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();

            // Create XML declaration
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclaration);

            // Create root element
            XmlElement rootElement = xmlDoc.CreateElement("students_parliament");
            xmlDoc.AppendChild(rootElement);

            // Iterate through the list and add student elements
            foreach (var student in students)
            {
                XmlElement studentElement = xmlDoc.CreateElement("event");

                XmlElement nameElement = xmlDoc.CreateElement("name");
                nameElement.InnerText = student.Name.Replace("Name: ", "");
                studentElement.AppendChild(nameElement);

                XmlElement facultyElement = xmlDoc.CreateElement("faculty");
                facultyElement.InnerText = student.Faculty.Replace("Faculty: ", "");
                studentElement.AppendChild(facultyElement);

                XmlElement departmentElement = xmlDoc.CreateElement("department");
                departmentElement.InnerText = student.Department.Replace("Department: ", "");
                studentElement.AppendChild(departmentElement);

                XmlElement specialtyElement = xmlDoc.CreateElement("specialty");
                specialtyElement.InnerText = student.Specialty.Replace("Specialty: ", "");
                studentElement.AppendChild(specialtyElement);

                XmlElement timingElement = xmlDoc.CreateElement("timing");
                timingElement.InnerText = student.Timing.Replace("Timing: ", "");
                studentElement.AppendChild(timingElement);

                rootElement.AppendChild(studentElement);
            }

            return xmlDoc;
        }
    }
}