using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AparmentManager2023.Data.FrontEnd
{
    public class PersonApartmentModel
    {
        public String CardNum_ID { get; set; }
        public int Apartment_ID { get; set; }
        public bool Payer { get; set; }
        public DateTime? Contract_Start { get; set; }
        public DateTime? Contract_End { get; set; }
        [ForeignKey(nameof(CardNum_ID))]
        [InverseProperty(nameof(PersonClass.ApartmentClasses))]
        public PersonClass Person { get; set; }

        [ForeignKey(nameof(Apartment_ID))]
        [InverseProperty(nameof(ApartmentClass.People))]
        public ApartmentClass Apartment { get; set; }
    }
}
