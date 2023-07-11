using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentManager2023.Data.FrontEnd
{
    public class OwnerModel
    {
        public String CardNum_ID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public long Phone { get; set; }
        public int Owner_ID { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
    }
}
