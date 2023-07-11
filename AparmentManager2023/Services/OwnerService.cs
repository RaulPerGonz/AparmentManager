using AparmentManager2023.Data;
using AparmentManager2023.Pages;
using Humanizer;
using MudBlazor;

namespace AparmentManager2023.Services
{
    public class OwnerService
    {
        protected readonly ApplicationDBContext _dbContext;
        public OwnerService(ApplicationDBContext _db)
        {
            _dbContext = _db;
        }

        public List<OwnerClass> displayowner()
        {
            return _dbContext.Owner.ToList();
        }
        public bool insertOwner(OwnerClass owner)
        {
            try { 
                _dbContext.Owner.Add(owner);
                _dbContext.SaveChanges();
            } catch(Exception ex)
            {
                return false;
            }
            
            return true;
        }
        public OwnerClass editOwner(int Owner_ID)
        {
            OwnerClass ow = new OwnerClass();
            return _dbContext.Owner.FirstOrDefault(u => u.Owner_ID == Owner_ID); 
        }
        public bool updateOwner(OwnerClass ocupdate) 
        { 
            var Ownerupdate= _dbContext.Owner.FirstOrDefault(u => u.Owner_ID==ocupdate.Owner_ID);
            if (Ownerupdate != null)
            {
                Ownerupdate.CardNum_ID = ocupdate.CardNum_ID;
                Ownerupdate.Name = ocupdate.Name;
                Ownerupdate.Email = ocupdate.Email;
                Ownerupdate.Phone = ocupdate.Phone;
                Ownerupdate.Owner_ID = Ownerupdate.Owner_ID;
                Ownerupdate.Username = ocupdate.Username;
                Ownerupdate.Password = ocupdate.Password;
                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        //Not in cascade or restricted
        public bool deleteOwner(OwnerClass ocdel)
        {
            if(ocdel != null) {
            _dbContext.Remove(ocdel);
            _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
