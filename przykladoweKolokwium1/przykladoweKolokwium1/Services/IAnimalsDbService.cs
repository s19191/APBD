using System.Collections.Generic;
using przykladoweKolokwium1.Models.Reguests;
using przykladoweKolokwium1.Models.Responses;

namespace przykladoweKolokwium1.Services
{
    public interface IAnimalsDbService
    {
        public List<GetAnimalsResponse> GetAnimals(string sortBy);
        public bool AddAnimal(AddAnimalsReguest reguest);
    }
}