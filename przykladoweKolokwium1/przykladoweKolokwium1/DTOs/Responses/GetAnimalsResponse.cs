namespace przykladoweKolokwium1.Models.Responses
{
    public class GetAnimalsResponse
    {
        public string Name { get; set; }
        public string AnimalType { get; set; }
        public string DateOfAdmission { get; set; }
        public string LastNameOfOwner { get; set; }

        public GetAnimalsResponse(string name, string animalType, string dateOfAdmission, string lastNameOfOwner)
        {
            Name = name;
            AnimalType = animalType;
            DateOfAdmission = dateOfAdmission;
            LastNameOfOwner = lastNameOfOwner;
        }
    }
}