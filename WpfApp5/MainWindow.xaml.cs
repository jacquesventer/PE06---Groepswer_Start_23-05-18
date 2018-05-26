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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using MailingList.Lib.Entities;
using MailingList.Lib.Services;
using InputControl.Lib;


namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DeelnemerServices beheerDeelnemers = new DeelnemerServices();
        bool answer = false;
        TextBoxControl beheerControls = new TextBoxControl();

        public MainWindow()
        {
            InitializeComponent();
            SpecialeControls();
            EnableDisableButtons(false);
            ValidatieDeelnemer(grdTop);
            beheerControls.AlleTextBoxenNormaal();
        }

        void MaakVeldenDeelnemersLeeg()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtStreet.Clear();
            txtStreetNumber.Clear();
            txtCity.Clear();
            txtPostalCode.Clear();
            beheerControls.AlleTextBoxenNormaal();
            EnableDisableButtons(false);
        }

        void SpecialeControls()
        {
            beheerControls.nietLeeg = new List<TextBox>();
            beheerControls.nietLeeg.Add(txtCity);
            beheerControls.nietLeeg.Add(txtFirstName);
            beheerControls.nietLeeg.Add(txtLastName);
            beheerControls.nietLeeg.Add(txtPostalCode);
            beheerControls.nietLeeg.Add(txtStreet);
            beheerControls.nietLeeg.Add(txtStreetNumber);
            beheerControls.SetupControleLeeg();

            beheerControls.adresInput = new List<TextBox>();
            beheerControls.adresInput.Add(txtEmail);
            beheerControls.SetupControleEmailAdres();

            beheerControls.intInput = new List<TextBox>();
            beheerControls.intInput.Add(txtStreetNumber);
            beheerControls.SetupControleInt();
        }

        private void ControleDeelnemer(object sender, RoutedEventArgs e)
        {
            bool veldenOk = true;
            int huisNr;

            if (txtCity.Text.Trim(" -".ToArray()) == "") veldenOk = false;
            else if (txtFirstName.Text.Trim(" -".ToArray()) == "") veldenOk = false;
            else if (txtLastName.Text.Trim(" -".ToArray()) == "") veldenOk = false;
            else if (txtPostalCode.Text.Trim(" -".ToArray()) == "") veldenOk = false;
            else if (txtStreet.Text == "") veldenOk = false;
            else if ( int.TryParse(txtStreetNumber.Text.ToString(), out huisNr)  == false ) veldenOk = false;
            else if ( beheerControls.CheckEmailAdres(txtEmail.Text.ToString()) == false ) veldenOk = false;

            if (veldenOk)
            {
                EnableDisableButtons(true);
            }
            else
            {
                EnableDisableButtons(false);
            }
        }

        private void EnableDisableButtons( bool enable)
        {
            btnBlue.IsEnabled = enable;
            btnGreen.IsEnabled = enable;
            btnOrange.IsEnabled = enable;
        }

        private void ValidatieDeelnemer(Grid ouder)
        {
            foreach (Object control in ouder.Children)
            {
                if (control.GetType().ToString().Contains("TextBox"))
                {
                    TextBox textBox = (TextBox)control;
                    textBox.LostFocus += ControleDeelnemer;
                }
            }
        }

        private Deelnemer MaakDeelnemer( bool antwoord )
        {
            Deelnemer deelnemer = new Deelnemer();
            deelnemer.Id = 0;
            deelnemer.FirstName = txtFirstName.Text;
            deelnemer.LastName = txtLastName.Text;
            deelnemer.Email = txtEmail.Text;
            deelnemer.Phone = txtPhone.Text;
            deelnemer.Street = txtStreet.Text;
            deelnemer.StreetNumber = Int32.Parse(txtStreetNumber.Text);
            deelnemer.City = txtCity.Text;
            deelnemer.PostalCode = Int32.Parse(txtPostalCode.Text);
            deelnemer.Answer = antwoord.ToString();

            return deelnemer;
        }

        #region eventRegion
        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            Deelnemer deelnemerTrue = MaakDeelnemer(true);

            if (!beheerDeelnemers.NieuwDeelnemer(deelnemerTrue))
            {
                MessageBox.Show("Fout bij ingave, gegevens konden niet verwerkt worden");

            }
            else
            {
                MessageBox.Show("Bedankt voor je deelname!");
                MaakVeldenDeelnemersLeeg();
            }
        }


        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            Deelnemer deelnemerFalse = MaakDeelnemer(false);
            if (!beheerDeelnemers.NieuwDeelnemer(deelnemerFalse))
            {
                MessageBox.Show("Fout bij ingave, gegevens konden niet verwerkt worden");

            }
            else
            {
                MessageBox.Show("Bedankt voor je deelname!");
                MaakVeldenDeelnemersLeeg();
            }
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            Deelnemer deelnemerFalse = MaakDeelnemer(false);
            if (!beheerDeelnemers.NieuwDeelnemer(deelnemerFalse))
            {
                MessageBox.Show("Fout bij ingave, gegevens konden niet verwerkt worden");

            }
            else
            {
                MessageBox.Show("Bedankt voor je deelname!");
                MaakVeldenDeelnemersLeeg();               
            }
        }
        #endregion
    }
}
