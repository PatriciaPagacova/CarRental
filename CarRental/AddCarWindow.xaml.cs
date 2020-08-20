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
    /// Interaction logic for AddCarWindow.xaml
    /// </summary>
    public partial class AddCarWindow : Window
    {
        DataController myDataController;
        Car myEditedCar;

        public AddCarWindow(DataController dc)
        {
            myDataController = dc;
            InitializeComponent();
        }

        public AddCarWindow(DataController dc, Car editedCar)
        {
            myDataController = dc;
            myEditedCar = editedCar;
            InitializeComponent();
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            TBLicencePlate.Text = myEditedCar.LicencePlate;
            TBBrand.Text = myEditedCar.Brand;
            TBModel.Text = myEditedCar.Model;
            TBVin.Text = myEditedCar.VIN;
        }


        private void BtnSaveCar_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckOfMandatoryFields())
            {
                return;
            }

            if(myEditedCar != null)
            {
                SaveEditedCar();
            }
            else
            {
                SaveAddedCustomer();
            }

            
            this.Close();
        }

        private void SaveEditedCar()
        {
            myEditedCar.LicencePlate = TBLicencePlate.Text;
            myEditedCar.Brand = TBBrand.Text;
            myEditedCar.Model = TBModel.Text;
            myEditedCar.VIN = TBVin.Text;

            myDataController.SaveChanges();
        }

        private void SaveAddedCustomer()
        {
            var newCar = new Car()
            {
                LicencePlate = TBLicencePlate.Text,
                Brand = TBBrand.Text,
                Model = TBModel.Text,
                VIN = TBVin.Text,
            };

            myDataController.AddNewCar(newCar);
        }

        private bool CheckOfMandatoryFields()
        {
            if (string.IsNullOrWhiteSpace(TBLicencePlate.Text) ||
                string.IsNullOrWhiteSpace(TBBrand.Text) ||
                string.IsNullOrWhiteSpace(TBModel.Text) ||
                string.IsNullOrWhiteSpace(TBVin.Text))
            {
                MessageBox.Show("Not all mandatory field is filled!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void BtnCancelCar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
