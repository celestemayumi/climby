namespace climby.Models
{
    public class Shelter
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public int AdressNumber { get; set; }
        public string District { get; set; }
        public int IsFull { get; set; }
        public int Cep { get; set; }
    }
}
