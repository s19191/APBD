using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace cw2
{
    public class Uczelnia
    {
        [JsonPropertyName("createdAt")] 
        [XmlAttribute(AttributeName = "createdAt")]
        public string date { get; set; }
        
        [XmlAttribute]
        public string author { get; set; }

        [JsonPropertyName("studenci")]
        [XmlArray("studenci")]
        public Student[] students { get; set; }

        [XmlArrayItem("studies")]
        public activeStudies[] activeStudies { get; set; }

        public Uczelnia(List<Student> students)
        {
            date = DateTime.Today.ToString("d");
            author = "Jan Kwasowski";
            this.students = students.ToArray();
            List<activeStudies> activeStudieses = new List<activeStudies>();
            var tmp = from student in students
                group student by student.studies.name into groups
                select new activeStudies(groups.Key, groups.Count());
            foreach(var studies in tmp)
            {
                activeStudieses.Add(new activeStudies(studies.name, studies.numberOfStudents));
            }
            activeStudies = activeStudieses.ToArray();
        }

        public Uczelnia()
        {
            date = null;
            author = null;
            students = null;
            activeStudies = null;
        }
    }
}