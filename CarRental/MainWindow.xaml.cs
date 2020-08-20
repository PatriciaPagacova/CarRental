using DBWorker;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataController myDataController;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                myDataController = new DataController();
            }
            catch(Exception e)
            {
                MessageBox.Show("The database is unavailable. Program can not continue!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SetContexts();
        }

        public void SetContexts()
        {
            DataGridCars.DataContext = myDataController.Cars;
            DataGridCustomers.DataContext = myDataController.Customers;
            //DataGridRentals.DataContext = myDataController.Rentals;
        }

        private void BtnAddGeneric_Click(object sender, RoutedEventArgs e)
        {
            if(TabCustomers.IsSelected == true)
            {
                AddCustomerWindow addCustomerWindow = new AddCustomerWindow(myDataController);
                addCustomerWindow.Show();
                addCustomerWindow.Closing += (s, eventArgs) => SetContexts();
            }
            else
            {
                AddCarWindow addCarWindow = new AddCarWindow(myDataController);
                addCarWindow.Show();
                addCarWindow.Closing += (s, eventArgs) => SetContexts();
            }
        }

        private void BtnEditGeneric_Click(object sender, RoutedEventArgs e)
        {
            if (TabCustomers.IsSelected == true)
            {
                AddCustomerWindow addCustomerWindow = new AddCustomerWindow(myDataController, DataGridCustomers.SelectedItem as Customer);
                addCustomerWindow.Show();
                addCustomerWindow.Closing += (s, eventArgs) => SetContexts();
                addCustomerWindow.Closing += (s, eventArgs) => ShowRentalsOfSelectedItem();
            }
            else
            {
                AddCarWindow addCarWindow = new AddCarWindow(myDataController, DataGridCars.SelectedItem as Car);
                addCarWindow.Show();
                addCarWindow.Closing += (s, eventArgs) => SetContexts();
                addCarWindow.Closing += (s, eventArgs) => ShowRentalsOfSelectedItem(); 
            }
            
        }

        private void BtnDeleteGeneric_Click(object sender, RoutedEventArgs e)
        {
            if (TabCustomers.IsSelected == true)
            {
                if(myDataController.DeleteCustomer(DataGridCustomers.SelectedItem as Customer))
                {
                    SetContexts();
                }
                else
                {
                    MessageBox.Show("The customer can not be removed. Please remove all his rentals first.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if(myDataController.DeleteCar(DataGridCars.SelectedItem as Car))
                {
                    SetContexts();
                }
                else
                {
                    MessageBox.Show("The car can not be removed. Please remove all its rentals first.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void ShowRentalsOfSelectedItem()
        {
            if (TabCustomers.IsSelected == true && DataGridCustomers.SelectedItem is Customer)
            {
                DataGridRentals.DataContext = myDataController.GetRenatalsBelongToCutomer(DataGridCustomers.SelectedItem as Customer);
            }
            else if (TabCars.IsSelected == true && DataGridCars.SelectedItem is Car)
            {
                DataGridRentals.DataContext = myDataController.GetRenatalsBelongToCar(DataGridCars.SelectedItem as Car);
            }
            else
            {
                DataGridRentals.DataContext = new List<Rental>();
            }
        }

        private void DataGridCustomers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowRentalsOfSelectedItem();
        }

        private void DataGridCars_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowRentalsOfSelectedItem();
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowRentalsOfSelectedItem();
        }

        private void BtnAddRental_Click(object sender, RoutedEventArgs e)
        {
            AddRentalWindow addRentalWindow = new AddRentalWindow(myDataController);
            addRentalWindow.Show();
            addRentalWindow.Closing += (s, eventArgs) => ShowRentalsOfSelectedItem();
        }

        
    }
}
