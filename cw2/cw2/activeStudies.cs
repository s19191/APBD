using System.Xml.Serialization;

namespace cw2
{
    public class activeStudies
    {
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public int numberOfStudents { get; set; }

        public activeStudies(string name, int numberOfStudents)
        {
            this.name = name;
            this.numberOfStudents = numberOfStudents;
        }

        public activeStudies()
        {
            name = null;
            numberOfStudents = 0;
        }
    }
}