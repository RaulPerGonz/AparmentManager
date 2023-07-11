using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentManager2023.Data.FrontEnd
{
    public class Monthly_BillModel
    {
        public int Bill_ID { get; set; }
        public String CardNum_ID { get; set; }
        public int Apartment_ID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float Amount_Paid { get; set; }

        public bool Paid { get; set; }
    }
}
