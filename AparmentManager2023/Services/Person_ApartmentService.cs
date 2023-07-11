using AparmentManager2023.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;

namespace AparmentManager2023.Services
{
    public class Person_ApartmentService
    {
        protected readonly ApplicationDBContext _dbContext;
        //private IDbContextFactory<ApplicationDBContext> _dbContextFactory;
        public Person_ApartmentService(ApplicationDBContext _db)
        {
            _dbContext = _db;
        }

        public List<Person_ApartmentClass> displayperson_apartment()
        {
            return _dbContext.Person_Apartment.ToList();
        }


        public List<Person_ApartmentClass> findPesonAparmentById( int apartmentId)
        {
            var personAparments = (from personApartment in _dbContext.Person_Apartment
                         where personApartment.Apartment_ID == apartmentId 
                         select new Person_ApartmentClass()
                         {
                             CardNum_ID = personApartment.CardNum_ID,
                             Apartment_ID = personApartment.Apartment_ID,
                             Payer = personApartment.Payer,
                             Contract_Start = personApartment.Contract_Start,
                             Contract_End =  personApartment.Contract_End,
                         }
         );
            return personAparments.ToList();
        }


        public bool insertPerson_Apartment(string personId, int apartmentId, bool payer)
        {
            try
            {
                Person_ApartmentClass person_Apartment = new Person_ApartmentClass();
                person_Apartment.Apartment_ID = apartmentId;
                person_Apartment.CardNum_ID = personId;
                person_Apartment.Payer = payer;
                person_Apartment.Contract_Start = null;
                person_Apartment.Contract_End = null;
                _dbContext.Person_Apartment.Add(person_Apartment);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public Person_ApartmentClass editPerson_Apartment(String CardNum_ID)
        {
            Person_ApartmentClass pa = new Person_ApartmentClass();
            return _dbContext.Person_Apartment.FirstOrDefault(u => u.CardNum_ID.Equals(CardNum_ID));
        }
        public bool updatePerson_Apartment(Person_ApartmentClass pacupdate)
        {
            var Person_Apartmentupdate = _dbContext.Person_Apartment.FirstOrDefault(u => u.CardNum_ID == pacupdate.CardNum_ID);
            if (Person_Apartmentupdate != null)
            {
                Person_Apartmentupdate.CardNum_ID = pacupdate.CardNum_ID;
                Person_Apartmentupdate.Apartment_ID = pacupdate.Apartment_ID;
                Person_Apartmentupdate.Payer = pacupdate.Payer;
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        //Not in cascade or restricted
        public bool deletePeopleApartment( int apartmentId)
        {

            List<Person_ApartmentClass> pesonsAparmenstList =  this.findPesonAparmentById(apartmentId);
            if (pesonsAparmenstList != null)
            {

                foreach (Person_ApartmentClass personApartment in pesonsAparmenstList)
                {
                    _dbContext.Remove(personApartment);
                    _dbContext.SaveChanges();
                }
                    
            }
            else
            {
                return false;
            }
            return true;
        }
    }
    
}
