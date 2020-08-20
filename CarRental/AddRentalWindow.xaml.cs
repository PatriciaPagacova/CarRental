using DBWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for AddRentalWindow.xaml
    /// </summary>
    public partial class AddRentalWindow : Window
    {
        //MainWindow myMainWindow;
        DataController myDataController;

        public AddRentalWindow(DataController dc)
        {
            myDataController = dc;
            InitializeComponent();
            CBCustomer.DataContext = myDataController.Customers;
            CBCar.DataContext = myDataController.Cars;
        }

        private void BtnSaveRental_Click(object sender, RoutedEventArgs e)
        {
            var newRental = new Rental()
            {
                PickUpDate = DPPickUpDate.SelectedDate.Value,
                DropOffDate = DPDropOffDate.SelectedDate,
                CustomerID = SelectedCustomerID,
                CarID = SelectedCarID,
                Note = TbNote.Text,
            };

            myDataController.AddNewRental(newRental);
            this.Close();
        }

        //private int GetCustomerID => (CBCustomer.SelectedItem as Customer).CutomerID;
        private int SelectedCustomerID
        {
            get
            {
                if (CBCustomer.SelectedItem is Customer customer)
                {
                    return customer.CutomerID;
                }
                return -1;
            }
        }

        private int SelectedCarID
        {
            get
            {
                if (CBCar.SelectedItem is Car car)
                {
                    return car.CarID;
                }
                return -1;
            }
        }

        private void BtnCancelRental_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
