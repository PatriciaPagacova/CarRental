using System.Collections.Generic;
using System.Linq;

namespace DBWorker
{
    public class DataController
    {
        CarRentalEntities myDatabase;

        public DataController()
        {
            myDatabase = new CarRentalEntities();
        }

        public void SaveChanges()
        {
            myDatabase.SaveChanges();
        }


        public IList<Car> Cars => myDatabase.Cars.ToList();
        public IList<Customer> Customers => myDatabase.Customers.ToList();
        public IList<Rental> Rentals => myDatabase.Rentals.ToList();

        public void AddNewCustomer(Customer newCustomer)
        {
            myDatabase.Customers.Add(newCustomer);
            SaveChanges();
        }

        public void AddNewCar(Car newCar)
        {
            myDatabase.Cars.Add(newCar);
            SaveChanges();
        }

        public void AddNewRental(Rental newRental)
        {
            myDatabase.Rentals.Add(newRental);
            SaveChanges();
        }

        public bool DeleteCustomer(Customer deletedCustomer)
        {
            if(!CanRemove(deletedCustomer))
            {
                return false;
            }

            myDatabase.Customers.Remove(deletedCustomer);
            SaveChanges();
            return true;
        }

        public bool DeleteCar(Car deletedCar)
        {
            if (!CanRemove(deletedCar))
            {
                return false;
            }

            myDatabase.Cars.Remove(deletedCar);
            SaveChanges();
            return true;
        }

        public IList<Rental> GetRenatalsBelongToCutomer(Customer selectedCutomer)
        {
            return myDatabase.Rentals.Where(r => r.CustomerID == selectedCutomer.CutomerID).ToList();
        }

        public IList<Rental> GetRenatalsBelongToCar(Car selectedCar)
        {
            return myDatabase.Rentals.Where(r => r.CarID == selectedCar.CarID).ToList();
        }

        private bool CanRemove(Customer customer)
        {
            return !myDatabase.Rentals.Any(r => r.CustomerID == customer.CutomerID);
        }

        private bool CanRemove(Car car)
        {
            return !myDatabase.Rentals.Any(r => r.CarID == car.CarID);
        }
    }
}
