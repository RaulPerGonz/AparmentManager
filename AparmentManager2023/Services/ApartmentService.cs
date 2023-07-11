using AparmentManager2023.Data;
using AparmentManager2023.Data.FrontEnd;
using Microsoft.EntityFrameworkCore;


namespace AparmentManager2023.Services
{
    public class ApartmentService
    {
        protected readonly ApplicationDBContext _dbContext;

        private Monthly_BillService billService;
        private Person_ApartmentService personApartmentService;
        public ApartmentService(ApplicationDBContext _db, Monthly_BillService billService)
        {
            _dbContext = _db;
            this.billService = billService;
        }

        public List<ApartmentClass> displayapartment()
        {
            return _dbContext.Apartment
                .Include(x => x.People).ThenInclude(x => x.Person)
                .ToList();
        }

        public List<ApartmentOverview> loadOverviews(string? searchQuery = null)
        {
            IQueryable<ApartmentClass> apartments = _dbContext.Apartment;

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                string[] queries = searchQuery.Split(' ');

                foreach (string query in queries)
                {
                    apartments = apartments.Where(x => x.Name.Contains(query));
                }
            }

            return apartments
                .Select(apartment => new ApartmentOverview()
                {
                    Owner_ID = apartment.Owner_ID,
                    ApartmentName = apartment.Name,
                    ApartmentAddress = apartment.Address,
                    People = apartment.People.Select(apartmentPersonRelation => apartmentPersonRelation.Person.Name).ToList()
                })
                .ToList();
        }
        public bool insertApartment(ApartmentClass apartment)
        {
            try
            {
                _dbContext.Apartment.Add(apartment);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public ApartmentClass editApartment(int Apartment_ID)
        {
            ApartmentClass ap = new ApartmentClass();
            return _dbContext.Apartment.FirstOrDefault(u => u.Apartment_ID == Apartment_ID);
        }
        public bool updateApartment(ApartmentClass acupdate)
        {
            var Apartmentupdate = _dbContext.Apartment.FirstOrDefault(u => u.Apartment_ID == acupdate.Apartment_ID);
            if (Apartmentupdate != null)
            {
                Apartmentupdate.Name = acupdate.Name;
                Apartmentupdate.Address = acupdate.Address;
                Apartmentupdate.Monthly_Price = acupdate.Monthly_Price;
                Apartmentupdate.Num_Bedrooms = acupdate.Num_Bedrooms;
                Apartmentupdate.M2 = acupdate.M2;
                Apartmentupdate.Image = acupdate.Image;
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        //Not in cascade or restricted
        public bool deleteApartment(ApartmentClass apartment)
        {
            if (apartment != null)
            {
                ApartmentClass toDelete = _dbContext.Apartment.First(x => x.Apartment_ID == apartment.Apartment_ID);
                //List <Monthly_BillClass> apartmentBills = billService.GetBillsFromApartment(apartment.Apartment_ID);

                //foreach(Monthly_BillClass bill in apartmentBills)
                //{
                //    billService.deleteMonthly_Bill(bill);
                //}

                //personApartmentService.deletePeopleApartment(apartment.Apartment_ID);

                _dbContext.Remove(apartment);
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }


        public ApartmentModel GetApartmentModel(int Apartment_ID)
        {
            var AparmentClass = _dbContext.Apartment.Where(aparment => aparment.Apartment_ID == Apartment_ID).FirstOrDefault();
            if (AparmentClass == null)
            {
                return null;
            }
            ApartmentModel aparment = new ApartmentModel();
            aparment.Address = AparmentClass.Address;
            aparment.Num_Bedrooms = AparmentClass.Num_Bedrooms;
            aparment.Apartment_ID = AparmentClass.Apartment_ID;
            aparment.M2 = AparmentClass.M2;
            aparment.Owner_ID = AparmentClass.Owner_ID;
           

            return aparment;
        }

        public ApartmentClass GetApartmentClass(int Apartment_ID)
        {
            var ApartmentClass = _dbContext.Apartment.Where(aparment => aparment.Apartment_ID == Apartment_ID).FirstOrDefault();


            return ApartmentClass;
        }
    }
}
