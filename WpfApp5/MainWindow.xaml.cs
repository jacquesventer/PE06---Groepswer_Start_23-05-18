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




namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DeelnemerServices beheerDeelnemers;
        bool answer = false;

        public MainWindow()
        {
            InitializeComponent();
            DeelnemerServices.bestandsPad = AppDomain.CurrentDomain.BaseDirectory + "MailingList.accdb";
            beheerDeelnemers = new DeelnemerServices();
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
        }
        #region eventRegion
        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            //Vindt inderdaad de database file niet zoals jacques vermoedde
            answer = true;

           // Deelnemer deelnemerTrue = new Deelnemer();
         //   deelnemerTrue.Id = 0;
           // deelnemerTrue.FirstName = txtFirstName.Text;
          //  deelnemerTrue.LastName = txtLastName.Text;
          //  deelnemerTrue.Email = txtEmail.Text;
            //deelnemerTrue.Phone = Int32.Parse(txtPhone.Text);
            //deelnemerTrue.Street = txtStreet.Text;
            //deelnemerTrue.StreetNumber = Int32.Parse(txtStreetNumber.Text);
            //deelnemerTrue.City = txtCity.Text;
            //deelnemerTrue.PostalCode = Int32.Parse(txtPostalCode.Text);
            //deelnemerTrue.Answer = answer.ToString();
            //if (!beheerDeelnemers.NieuwDeelnemer(deelnemerTrue))
            //{
            //    MessageBox.Show("Fout bij ingave, gegevens konden niet verwerkt worden");

            //}
            //else
            //{
             //   MessageBox.Show("Bedankt voor je deelname!");
               // MaakVeldenDeelnemersLeeg();
            //}
            Deelnemer newparticipant = new Deelnemer(0, txtFirstName.Text, txtLastName.Text, txtEmail.Text,
                Int32.Parse(txtPhone.Text), txtStreet.Text, Int32.Parse(txtStreetNumber.Text), txtCity.Text, Int32.Parse(txtPostalCode.Text),answer.ToString());
            if (!beheerDeelnemers.NieuwDeelnemer(newparticipant))
            {
                MessageBox.Show("De gegevens zijn niet opgeslagen");
            }
            else
            {
                MaakVeldenDeelnemersLeeg();
                txtFirstName.Focus();
            }
         
        }


        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            answer = false;
          
            Deelnemer deelnemerFalse = new Deelnemer();
            deelnemerFalse.Id = 0;
            deelnemerFalse.FirstName = txtFirstName.Text;
            deelnemerFalse.LastName = txtFirstName.Text;
            deelnemerFalse.Email = txtEmail.Text;
            deelnemerFalse.Phone = Int32.Parse(txtPhone.Text);
            deelnemerFalse.Street = txtStreet.Text;
            deelnemerFalse.StreetNumber = Int32.Parse(txtStreetNumber.Text);
            deelnemerFalse.City = txtCity.Text;
            deelnemerFalse.PostalCode = Int32.Parse(txtPostalCode.Text);
            deelnemerFalse.Answer = answer.ToString();
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
            answer = false;

            Deelnemer deelnemerFalse = new Deelnemer();
            deelnemerFalse.Id = 0;
            deelnemerFalse.FirstName = txtFirstName.Text;
            deelnemerFalse.LastName = txtFirstName.Text;
            deelnemerFalse.Email = txtEmail.Text;
            deelnemerFalse.Phone = Int32.Parse(txtPhone.Text);
            deelnemerFalse.Street = txtStreet.Text;
            deelnemerFalse.StreetNumber = Int32.Parse(txtStreetNumber.Text);
            deelnemerFalse.City = txtCity.Text;
            deelnemerFalse.PostalCode = Int32.Parse(txtPostalCode.Text);
            deelnemerFalse.Answer = answer.ToString();
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
