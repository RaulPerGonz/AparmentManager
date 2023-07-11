namespace AparmentManager2023.Data
{
    public class ApartmentOverview
    {
        public int Owner_ID { get; set; }
        public string ApartmentName { get; set; }
        public string ApartmentAddress { get; set; }
        public List<string> People { get; set; }
    }
}