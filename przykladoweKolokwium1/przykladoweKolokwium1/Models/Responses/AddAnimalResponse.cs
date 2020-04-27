namespace przykladoweKolokwium1.Models.Responses
{
    public class AddAnimalResponse
    {
        public string Name { get; set; }
        public string AnimalType { get; set; }
        public string DateOfAdmission { get; set; }
        public int IdOwner { get; set; }

        public AddAnimalResponse(string name, string animalType, string dateOfAdmission, int idOwner)
        {
            Name = name;
            AnimalType = animalType;
            DateOfAdmission = dateOfAdmission;
            IdOwner = idOwner;
        }
    }
}