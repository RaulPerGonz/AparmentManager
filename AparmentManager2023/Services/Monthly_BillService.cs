using AparmentManager2023.Data;
using AparmentManager2023.Pages;

namespace AparmentManager2023.Services
{
    public class Monthly_BillService
    {
        protected readonly ApplicationDBContext _dbContext;
        public Monthly_BillService(ApplicationDBContext _db)
        {
            _dbContext = _db;
        }

        public List<Monthly_BillClass> displayMonthly_Bill()
        {
            return _dbContext.Monthly_Bill.ToList();
        }
        public bool insertMonthly_Bill(Monthly_BillClass mb)
        {
            try
            {
                _dbContext.Monthly_Bill.Add(mb);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public Monthly_BillClass editMonthly_Bill(int Bill_ID)
        {
            Monthly_BillClass mb = new Monthly_BillClass();
            return _dbContext.Monthly_Bill.FirstOrDefault(u => u.Bill_ID == Bill_ID);
        }

        public List<Monthly_BillClass> GetMonthly_Bill_Payer(string Id, int ApartmentID)
        {
            var bills = (from monthly_bill in _dbContext.Monthly_Bill
                         where monthly_bill.CardNum_ID == Id && monthly_bill.Apartment_ID == ApartmentID
                         select new Monthly_BillClass()
                         {
                             Bill_ID = monthly_bill.Bill_ID,
                             CardNum_ID = monthly_bill.CardNum_ID,
                             Apartment_ID = monthly_bill.Apartment_ID,
                             Month = monthly_bill.Month,
                             Year = monthly_bill.Year,
                             Amount_Paid = monthly_bill.Amount_Paid,
                             Paid = monthly_bill.Paid
                         }
                                    );
            return bills.ToList();
        }

        public void CreateMonthlyBillsInRange(int apartmentID, DateTime startDate, DateTime endDate, float amountPaid,String cardNumID)
        {
            Monthly_BillService billService = new Monthly_BillService(_dbContext);

            if (startDate != null && endDate != null)
            {
                DateTime currentDate = startDate.Date;
                Random random = new Random();

                while (currentDate <= endDate.Date)
                {
                    int randomBillID = random.Next(100000, 999999);

                    // Comprobar si el ID de factura aleatorio ya existe en la base de datos
                    bool isExistingBillID = billService.displayMonthly_Bill().Any(b => b.Bill_ID == randomBillID);

                    if (!isExistingBillID)
                    {
                        Monthly_BillClass bill = new Monthly_BillClass
                        {
                            Bill_ID = randomBillID,
                            CardNum_ID = cardNumID,
                            Apartment_ID = apartmentID,
                            Month = currentDate.Month,
                            Year = currentDate.Year,
                            Amount_Paid = amountPaid,
                            Paid = false
                        };

                        billService.insertMonthly_Bill(bill);
                    }

                    // Moverse al siguiente mes
                    currentDate = currentDate.AddMonths(1);
                }
            }
        }

        public bool deleteBillsByPerson(string cardNumID)
        {
            try
            {
                var billsToDelete = _dbContext.Monthly_Bill.Where(b => b.CardNum_ID == cardNumID);
                _dbContext.Monthly_Bill.RemoveRange(billsToDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<Monthly_BillClass> GetBillsFromApartment(int ApartmentID)
        {
            var bills = (from monthly_bill in _dbContext.Monthly_Bill
                         where monthly_bill.Apartment_ID == ApartmentID
                         select new Monthly_BillClass()
                         {
                             Bill_ID = monthly_bill.Bill_ID,
                             CardNum_ID = monthly_bill.CardNum_ID,
                             Apartment_ID = monthly_bill.Apartment_ID,
                             Month = monthly_bill.Month,
                             Year = monthly_bill.Year,
                             Amount_Paid = monthly_bill.Amount_Paid,
                             Paid = monthly_bill.Paid
                         }
                                    );
            return bills.ToList();
        }



        public bool updateMonthly_Bill(Monthly_BillClass mbcupdate)
        {
            var Monthly_Billupdate = _dbContext.Monthly_Bill.FirstOrDefault(u => u.Bill_ID == mbcupdate.Bill_ID);
            if (Monthly_Billupdate != null)
            {
                Monthly_Billupdate.Bill_ID = mbcupdate.Bill_ID;
                Monthly_Billupdate.CardNum_ID = mbcupdate.CardNum_ID;
                Monthly_Billupdate.Apartment_ID = mbcupdate.Apartment_ID;
                Monthly_Billupdate.Month = mbcupdate.Month;
                Monthly_Billupdate.Year = mbcupdate.Year;
                Monthly_Billupdate.Amount_Paid = mbcupdate.Amount_Paid;
                Monthly_Billupdate.Paid = mbcupdate.Paid;

                _dbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        //Not in cascade or restricted
        public bool deleteMonthly_Bill(Monthly_BillClass bill)
        {
            if (bill != null)
            {
                _dbContext.Remove(bill);
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
