using System;
using System.Linq;
using System.Xml.Linq;

namespace cw2
{
    public class xmlFormatFile
    {
        public static void save(string[] filtred, string adresDolcelowy)
        {
            XElement xml = new XElement("uczelnia",
                new XAttribute("author", "Jan Kwasowski"),
                new XAttribute("createdAt", DateTime.Today.ToString("d")),
                from str in filtred
                let fields = str.Split(',')
                select new XElement("studneci",
                    new XAttribute("studnet_indexnumber", "s" + fields[2]),
                    new XElement("fname", fields[0]),
                    new XElement("lname", fields[1]),
                    new XElement("birthdatte", fields[5]),
                    new XElement("e-mails", fields[6]),
                    new XElement("mothersname", fields[7]),
                    new XElement("fathersname", fields[8]),
                    new XElement("studies",
                        new XElement("name", fields[3]),
                        new XElement("mode", fields[4])
                    )
                ),

                new XElement("activeStudies",
                    from str in filtred
                    let fields = str.Split(",")
                    group fields by fields[3]
                    into groupped
                    select new XElement("studies",
                        new XAttribute("name", groupped.Key),
                        new XAttribute("numberOfStudents", groupped.Count())
                    )
                )
            );
            xml.Save(String.Concat(adresDolcelowy + "result.xml"));
        }
    }
}