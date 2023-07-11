using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AparmentManager2023.Data.FrontEnd
{
    public class ApartmentModel
    {
        public int Apartment_ID { get; set; }
        public int Owner_ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public float Monthly_Price { get; set; }
        public int Num_Bedrooms { get; set; }
        public int M2 { get; set; }

        public float PricePerM { get { return Monthly_Price/M2; }}
        public string? Image { get; set; }

        [InverseProperty(nameof(Person_ApartmentClass.Apartment))]
        public IEnumerable<Person_ApartmentClass> People { get; set; }
    }
}
