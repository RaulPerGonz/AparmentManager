using AparmentManager2023.Data;
using System;
using AparmentManager2023.Services;
using AparmentManager2023.Data.FrontEnd;
using Microsoft.IdentityModel.Tokens;

namespace AparmentManager2023.Services

{
    public class BillService
    {
        protected readonly ApplicationDBContext _dbContext;
        public BillService(ApplicationDBContext _db)
        {
            _dbContext = _db;
        }

        public List<PersonClass> displayperson()
        {
            return _dbContext.Person.ToList();
        }
        public bool insertPerson(PersonModel person)
        {
            try
            {
                PersonClass personClass = new PersonClass();
                personClass.CardNum_ID = person.Id;
                personClass.Name = person.Name;
                personClass.Email = person.Email;
                personClass.Phone = person.Phone;

                _dbContext.Person.Add(personClass);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public bool Exists_Payer_Apartment(int Apartment_Id)
        {
            var existsPayer = (from person_apartment in _dbContext.Person_Apartment
                               where person_apartment.Apartment_ID == Apartment_Id && person_apartment.Payer == true
                               select new Person_ApartmentClass()
                               {
                                   CardNum_ID = person_apartment.CardNum_ID,
                                   Apartment_ID = person_apartment.Apartment_ID,
                                   Payer = person_apartment.Payer,
                                   Contract_Start = person_apartment.Contract_Start,
                                   Contract_End = person_apartment.Contract_End,
                               });
            if (!existsPayer.IsNullOrEmpty())
            {
                return true;
            }
            return false;
        }
        public PersonClass editPerson(int CardNum_ID)
        {
            PersonClass pe = new PersonClass();
            return _dbContext.Person.FirstOrDefault(u => u.CardNum_ID.Equals(CardNum_ID));
        }
        public bool updatePerson_PersonModel(PersonModel person)
        {
            var Personupdate = _dbContext.Person.FirstOrDefault(u => u.CardNum_ID == person.Id);

           

            if (Personupdate != null)
            {
                Personupdate.CardNum_ID = person.Id;
                Personupdate.Name = person.Name;
                Personupdate.Email = person.Email;
                Personupdate.Phone = person.Phone;
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool updatePayingFromPerson(bool isPaying,int apartmentId , string personId)
        {
            var person_Apartment = (from p in _dbContext.Person
                         join pa in _dbContext.Person_Apartment on p.CardNum_ID equals pa.CardNum_ID
                         where pa.Apartment_ID == apartmentId && p.CardNum_ID == personId
                         select pa).FirstOrDefault();

            if (person_Apartment != null)
            {
                person_Apartment.Payer = isPaying;
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool updatePerson(PersonClass pcupdate)
        {
            var Personupdate = _dbContext.Person.FirstOrDefault(u => u.CardNum_ID == pcupdate.CardNum_ID);
            
            if (Personupdate != null)
            {
                Personupdate.CardNum_ID = pcupdate.CardNum_ID;
                Personupdate.Name = pcupdate.Name;
                Personupdate.Email = pcupdate.Email;
                Personupdate.Phone = pcupdate.Phone;
                
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool CheckDuplicate_Email_Phone(string email, long phone, string id)
        {
            var email_check = (from p in _dbContext.Person
                               where p.CardNum_ID != id && p.Email == email
                               select new PersonClass() { CardNum_ID = p.CardNum_ID, Name = p.Name, Email = p.Email, Phone = p.Phone }
                                    );
            var phone_check = (from p in _dbContext.Person
                               where p.CardNum_ID != id && p.Phone == phone
                               select new PersonClass() { CardNum_ID = p.CardNum_ID, Name = p.Name, Email = p.Email, Phone = p.Phone }
                                    );
            if (!email_check.IsNullOrEmpty() || !phone_check.IsNullOrEmpty())
            {
                return true;
            }
            return false;
        }


        public bool CheckIfIsPayer(int ap_id)
        {
            var payer = (from p in _dbContext.Person
                         join pa in _dbContext.Person_Apartment on p.CardNum_ID equals pa.CardNum_ID
                         where pa.Apartment_ID == ap_id && pa.Payer == true
                         select p).FirstOrDefault();

            return payer != null;
        }
        //Not in cascade or restricted
        public bool deletePerson(String CardNum_ID)
        {


            PersonClass pcdel = GetPersonClass(CardNum_ID);
            if (pcdel != null)
            {
                _dbContext.Remove(pcdel);
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        public List<PersonClass> displaypeople_in_apartment(int ap_id)
        {
            var person_apartment = (from p in _dbContext.Person
                                    join p_ap in _dbContext.Person_Apartment on p.CardNum_ID equals p_ap.CardNum_ID
                                    where p_ap.Apartment_ID == ap_id
                                    select new PersonClass() { CardNum_ID = p.CardNum_ID, Name = p.Name, Email = p.Email, Phone = p.Phone }
                                    );
            return person_apartment.ToList();
        }

        public List<PersonModel> GetListOfTenants(int appartmentId)
        {
            return _dbContext.Person.Where(x => x.ApartmentClasses.Any(y => y.Apartment_ID == appartmentId)).Select(person => new PersonModel()
            {
                Id = person.CardNum_ID,
                Name = person.Name,
                Email = person.Email,
                Phone = person.Phone,
                IsPaying = person.ApartmentClasses.Where(appartment => appartment.CardNum_ID == person.CardNum_ID).Select(y => y.Payer).FirstOrDefault(),
            }).ToList();
        }
        public PersonModel GetPersonModel(String CardNum_ID)
        {
            var PersonClass = _dbContext.Person.Where(person => person.CardNum_ID == CardNum_ID).FirstOrDefault();
            if (PersonClass == null)
            {
                return null;
            }
            PersonModel person = new PersonModel();
            person.Name = PersonClass.Name;
            person.Email = PersonClass.Email;
            person.Phone = PersonClass.Phone;
            person.IsPaying = false;
            person.Id = PersonClass.CardNum_ID;

            return person;
        }

        public PersonClass GetPersonClass(String CardNum_ID)
        {
            var PersonClass = _dbContext.Person.Where(person => person.CardNum_ID == CardNum_ID).FirstOrDefault();


            return PersonClass;
        }
    }
}
