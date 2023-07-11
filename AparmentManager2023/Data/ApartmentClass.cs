using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentManager2023.Data
{
    [Table("Apartment", Schema = "public")]
    public class ApartmentClass
    { 
        [Key]
        public int Apartment_ID { get; set; }
        public int Owner_ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public float Monthly_Price { get; set; }
        public int Num_Bedrooms { get; set; }
        public int M2 { get; set; }
        public string? Image { get; set; }

        [InverseProperty(nameof(Person_ApartmentClass.Apartment))]
        public IEnumerable<Person_ApartmentClass> People { get; set; }

        [InverseProperty(nameof(Monthly_BillClass.Apartment))]
        public IEnumerable<Monthly_BillClass> Bills { get; set; }
    }
}

