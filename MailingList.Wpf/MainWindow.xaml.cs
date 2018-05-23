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
using Email.Lib.Entities;
using Email.Lib.Services;

namespace MailingList.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DeelnemerServices beheerDeelnemers = new DeelnemerServices();
        MailWinaars beheerWinaars = new MailWinaars();
        Deelnemer deelnemer_Sel;
        //TextBoxControl beheerControls = new TextBoxControl();

        public MainWindow()
        {
            InitializeComponent();
            DataOphalen();
        }

        void VulList()
        {
            lstMailingList.ItemsSource = beheerDeelnemers.deelnemers;
            lstMailingList.Items.Refresh();
        }

        void DataOphalen()
        {
            beheerDeelnemers.deelnemers = new List<Deelnemer>();
            beheerDeelnemers.ImportData();
            VulList();
        }

        void MaakVeldenDeelnemersLeeg()
        {
            lstMailingList.SelectedIndex = -1;
            lblId.Content = "";
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtStreet.Clear();
            txtStreetNumber.Clear();
            txtCity.Clear();
            txtPostalCode.Clear();
            VulList();
        }
            
        #region eventRegion
        private void lstMailingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMailingList.SelectedIndex >= 0)
            {
                deelnemer_Sel = (Deelnemer)lstMailingList.SelectedItem;
                lblId.Content = deelnemer_Sel.Id;
                txtFirstName.Text = deelnemer_Sel.FirstName;
                txtLastName.Text = deelnemer_Sel.LastName;
                txtEmail.Text = deelnemer_Sel.Email;
                txtPhone.Text = deelnemer_Sel.Phone.ToString();
                txtStreet.Text = deelnemer_Sel.Street;
                txtStreetNumber.Text = deelnemer_Sel.StreetNumber.ToString();
                txtCity.Text = deelnemer_Sel.City;
                txtPostalCode.Text = deelnemer_Sel.PostalCode.ToString();
            }
        }
        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Syntax fout in update ik(Diego) heb nog niet gevonden waarom
            Deelnemer participant = new Deelnemer(Int32.Parse(lblId.Content.ToString()), txtFirstName.Text, txtLastName.Text, txtEmail.Text, Int32.Parse(txtPhone.Text),
                txtStreet.Text, Int32.Parse(txtStreetNumber.Text), txtCity.Text, Int32.Parse(txtPostalCode.Text));
            if (!beheerDeelnemers.WijzigDeelnemer(participant))
            {
                MessageBox.Show("De wijzigingen zijn niet opgeslagen");
            }
            else
            {
                MaakVeldenDeelnemersLeeg();
                VulList();
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Deelnemer newparticipant = new Deelnemer(0, txtFirstName.Text, txtLastName.Text, txtEmail.Text,
                Int32.Parse(txtPhone.Text), txtStreet.Text, Int32.Parse(txtStreetNumber.Text), txtCity.Text, Int32.Parse(txtPostalCode.Text));
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lblId.Content.ToString() != "")
            {
                if (!beheerDeelnemers.VerwijderDeelnemer(deelnemer_Sel))
                {
                    MessageBox.Show("De wijzigingen zijn niet opgeslagen");
                }
                else
                {
                    VulList();
                    MaakVeldenDeelnemersLeeg();
                }
            }
        }

        private void btnSendNewsLetter_Click(object sender, RoutedEventArgs e)
        {

        }
        void SelecteerWinaarList()
        {
            Random rd = new Random();
            int indexWinnaar = rd.Next(0, beheerWinaars.winnaars.Count());
            
            beheerWinaars.winnaars = new List<Deelnemer>();
            beheerWinaars.SelecteerWinaar();

            lstMailingList.ItemsSource = beheerWinaars.winnaars;
            lstMailingList.Items.Refresh();
            lstMailingList.SelectedIndex = indexWinnaar;

        }
        private void btnChooseWinner_Click(object sender, RoutedEventArgs e)
        {
            //winnende deelnemers in list plaatsen dan met randonnummer deelnemer selecteren op index in de listindex zie void hierboven
            //Ergens fout in de selectquery mensen met false answer komen ook in de list
            SelecteerWinaarList();

        }

        private void btnSendWinnersMail_Click(object sender, RoutedEventArgs e)
        {
            Deelnemer winnaar = (Deelnemer)lstMailingList.SelectedValue;
            string rapport = txtWinnersMail.Text;
            string from, to, subject, paswoord;
            from = "E-MAILADRES";   //welk emaildres gebruiken? nog gedeelde gmailaccount maken? htmlbody weglaten door gebruik string en textbox?
            to = winnaar.Email;
            subject = "Gefeliciteerd!";
            paswoord = "pswoord";

            MailingComponents mailWinnaar = new MailingComponents(from, to, subject, rapport, from, paswoord);

            if (!SendMail.SendMessage(mailWinnaar))
            {
                MessageBox.Show("De mail is niet verzonden");
            }
        }
        #endregion

    }
}
