using System.Collections.Generic;
using cw3.Models;

namespace cw3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Studnet> _studnets;

        static MockDbService()
        {
            _studnets = new List<Studnet>
            {
                // new Studnet {IdStudent = 1, FirstName = "Jan", LastName = "Kowalski"},
                // new Studnet {IdStudent = 2, FirstName = "Anna", LastName = "Malewski"},
                // new Studnet {IdStudent = 3, FirstName = "Andrzej", LastName = "Andrzejewicz"}
            };
        }

        public IEnumerable<Studnet> GetStudents()
        {
            return _studnets;
        }
    }
}