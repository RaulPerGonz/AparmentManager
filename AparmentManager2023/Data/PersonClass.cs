using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentManager2023.Data
{
    [Table("Person", Schema = "public")]
    //Person table data
    public class PersonClass
    {
        [Key]
        public String CardNum_ID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public long Phone { get; set; }

        [InverseProperty(nameof(Person_ApartmentClass.Person))]
        public IEnumerable<Person_ApartmentClass> ApartmentClasses { get; set; }

    }
}
