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
using InputControl.Lib;

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
        TextBoxControl beheerControls = new TextBoxControl();
        SendMail zendMail = new SendMail();

        public MainWindow()
        {
            InitializeComponent();
            DataOphalen();
            SpecialeControls();
            ValidatieDeelnemer();
            MaakVeldenDeelnemersLeeg();
        }
        #region 
        void VulList()
        {
            lstMailingList.ItemsSource = beheerDeelnemers.deelnemers;
            lstMailingList.Items.Refresh();
        }
        void VulListWinnaars()
        {
            lstMailingList.ItemsSource = beheerWinaars.winnaars;
            lstMailingList.Items.Refresh();
        }

        void DataOphalenWinnaars()
        {
            beheerWinaars.winnaars = new List<Deelnemer>();
            beheerWinaars.SelecteerWinaar();
            VulListWinnaars();
        }
        void SelecteerWinaarList()
        {
            Random rd = new Random();
            int indexWinnaar = rd.Next(0, beheerWinaars.winnaars.Count());
            lstMailingList.SelectedIndex = indexWinnaar;
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
            deelnemer_Sel = null;
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

        private void ValidatieDeelnemer()
        {
            txtFirstName.LostFocus += ControleDeelnemer;
            txtLastName.LostFocus += ControleDeelnemer;
            txtCity.LostFocus += ControleDeelnemer;
            txtPostalCode.LostFocus += ControleDeelnemer;
            txtStreet.LostFocus += ControleDeelnemer;
            txtStreetNumber.LostFocus += ControleDeelnemer;
            txtEmail.LostFocus += ControleDeelnemer;
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
            else if (int.TryParse(txtStreetNumber.Text.ToString(), out huisNr) == false) veldenOk = false;
            else if (beheerControls.CheckEmailAdres(txtEmail.Text.ToString()) == false) veldenOk = false;

            if (veldenOk)
            {
                EnableDisableButtons(true);
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }

        private void EnableDisableButtons(bool enable)
        {
            btnDelete.IsEnabled = enable;
            btnSave.IsEnabled = enable;
        }


        #endregion




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
                EnableDisableButtons(true);
            }
        }
        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Deelnemer newDeelnemer = new Deelnemer(0, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text,txtStreet.Text,
                int.Parse(txtStreetNumber.Text), txtCity.Text, int.Parse(txtPostalCode.Text));

            if (lblId.Content.ToString() == "")
            {
                // een deelnemer toevoegen in mailinglist heeft een false antwoord
                newDeelnemer.Answer = false.ToString();
                if (beheerDeelnemers.NieuwDeelnemer(newDeelnemer) == false)
                {
                    MessageBox.Show("De wijzigingen zijn niet opgeslagen!!");
                }
                else
                {
                    MaakVeldenDeelnemersLeeg();
                    VulList();
                }
            }
            else
            {
                newDeelnemer.Id = deelnemer_Sel.Id; // Id van constructor overrulen met de geselecteerd id
                newDeelnemer.Answer = deelnemer_Sel.Answer;
                if (beheerDeelnemers.VeranderDeelnemer(newDeelnemer, deelnemer_Sel) == false)
                {
                    MessageBox.Show("De wijzigingen zijn niet opgeslagen!!");
                }
                else
                {
                    MaakVeldenDeelnemersLeeg();
                    VulList();
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            MaakVeldenDeelnemersLeeg();
            txtFirstName.Focus();
            deelnemer_Sel = null;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lblId.Content.ToString() != "" && deelnemer_Sel != null)
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
            foreach (Deelnemer mailDeelnemer in beheerDeelnemers.deelnemers)
            {
                string rapport = beheerWinaars.OpmaakEmail(mailDeelnemer);
                rapport += txtNewsLetter.Text;
                string from, to, subject, paswoord;
                from = "groepsw56@gmail.com";
                to = mailDeelnemer.Email;
                subject = "Newsletter";
                paswoord = "Huiswerk104";

                MailingComponents _mailDeelnemer = new MailingComponents(from, to, subject, rapport, from, paswoord);

                if (!SendMail.SendMessage(_mailDeelnemer))
                {
                    MessageBox.Show($"De e-mail is niet verzonden {_mailDeelnemer.To}");
                }
                txtNewsLetter.Clear();
            }
            MessageBox.Show("De e-mails zijn verzonden!");

        }

        private void btnChooseWinner_Click(object sender, RoutedEventArgs e)
        {
            deelnemer_Sel = (Deelnemer)lstMailingList.SelectedValue;
            DataOphalenWinnaars();
            SelecteerWinaarList();
            lblWinner.Content = deelnemer_Sel.LastName + " " + deelnemer_Sel.FirstName;
        }

        private void btnSendWinnersMail_Click(object sender, RoutedEventArgs e)
        {
            Deelnemer winnaar = (Deelnemer)lstMailingList.SelectedValue;
            
            string rapport = beheerWinaars.OpmaakEmail(winnaar);
            rapport += txtWinnersMail.Text;
            string from, to, subject, paswoord;
            from = "groepsw56@gmail.com"; 
            to = winnaar.Email;
            subject = "Gefeliciteerd!";
            paswoord = "Huiswerk104";

            MailingComponents mailWinnaar = new MailingComponents(from, to, subject, rapport, from, paswoord);

            if (!SendMail.SendMessage(mailWinnaar))
            {
                MessageBox.Show("De e-mail is niet verzonden");
            }
            else
            {
                MessageBox.Show("De e-mail is verzonden");
            }
            txtWinnersMail.Clear();
        }
        #endregion

    }
}
