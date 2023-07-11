using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentManager2023.Data
{
    [Table("Person_Apartment", Schema = "public")]
    //Person_Apartment table data
    public class Person_ApartmentClass
    {
        [Key]
        public String CardNum_ID { get; set; }
        public int  Apartment_ID { get; set; }
        public bool Payer { get; set; }
        public DateTime? Contract_Start{get; set;}
        public DateTime? Contract_End { get; set; }
        [ForeignKey(nameof(CardNum_ID))]
        [InverseProperty(nameof(PersonClass.ApartmentClasses))]
        public PersonClass Person { get; set; }

        [ForeignKey(nameof(Apartment_ID))]
        [InverseProperty(nameof(ApartmentClass.People))]
        public ApartmentClass Apartment { get; set; }

    }
}
