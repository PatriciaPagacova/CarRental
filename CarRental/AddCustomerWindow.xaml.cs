using DBWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        DataController myDataController;
        Customer myEditedCustomer;

        public AddCustomerWindow(DataController dc)
        {
            myDataController = dc;
            InitializeComponent();
        }

        public AddCustomerWindow(DataController dc, Customer editedCustomer)
        {
            myDataController = dc;
            myEditedCustomer = editedCustomer;
            InitializeComponent();
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            TBName.Text = myEditedCustomer.Name;
            RBMale.IsChecked = myEditedCustomer.Sex == 0;
            RBFemale.IsChecked = myEditedCustomer.Sex == 1;
            DatePickerDOB.SelectedDate = myEditedCustomer.DateOfBirth;
            TBAddress.Text = myEditedCustomer.Address;
            TBPhone.Text = myEditedCustomer.Phone;
            TBEmail.Text = myEditedCustomer.Email;
        }

        private void BtnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!AreMandatoryFieldFilled())
            {
                MessageBox.Show("Not all mandatory field is filled!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(!string.IsNullOrWhiteSpace(TBEmail.Text) && !IsEmailValid(TBEmail.Text))
            {
                MessageBox.Show("Email has invalide value!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (myEditedCustomer != null)
            {
                SaveEditedCustomer();
            }
            else
            {
                SaveAddedCustomer();
            }
            this.Close();
        }

        private void SaveEditedCustomer()
        {
            myEditedCustomer.Name = TBName.Text;
            myEditedCustomer.Sex = SelectSexWithRadioButton();
            myEditedCustomer.DateOfBirth = DatePickerDOB.SelectedDate;
            myEditedCustomer.Address = TBAddress.Text;
            myEditedCustomer.Phone = TBPhone.Text;
            myEditedCustomer.Email = TBEmail.Text;

            myDataController.SaveChanges();
        }

        private void SaveAddedCustomer()
        {
            var newCustomer = new Customer()
            {
                Name = TBName.Text,
                Sex = SelectSexWithRadioButton(),
                DateOfBirth = DatePickerDOB.SelectedDate,
                Address = TBAddress.Text,
                Phone = TBPhone.Text,
                Email = TBEmail.Text,
            };

            myDataController.AddNewCustomer(newCustomer);
        }

        private short SelectSexWithRadioButton()
        {
            if (RBMale.IsChecked == true)
            {
                return 0;
            }

            return 1;
        }

        private bool AreMandatoryFieldFilled()
        {
            if (string.IsNullOrWhiteSpace(TBName.Text) || string.IsNullOrWhiteSpace(TBPhone.Text))   
            {
                return false;
            }

            return true;
        }

        private static bool IsEmailValid(string emailAddress)
        {
            try
            {
                new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void BtnCancelCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
