using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InputControl.Lib
{
    public class TextBoxControl
    {
        public List<TextBox> nietLeeg;
        public List<TextBox> floatInput;
        public List<TextBox> intInput;
        public List<TextBox> adresInput;

        string decimaalTeken;

        public void BepaalDecimaalTeken()
        {
            float testGetal = 0.1f;

            string testString = testGetal.ToString("0.0");
            decimaalTeken = testString.Substring(1,1);
        }

        public string ZetKommaOm( string getal )
        {
            getal = getal.Replace(".", decimaalTeken);
            getal = getal.Replace(",", decimaalTeken);

            return getal;
        }

        void VerwerkDecimaalTeken( object sender, RoutedEventArgs e)
        {
            BepaalDecimaalTeken();
            TextBox textBox = (TextBox)sender;

            textBox.Text = ZetKommaOm(textBox.Text);
            CursorAchteraan(textBox);
        }

        void CursorAchteraan( TextBox textBox )
        {
            textBox.SelectionStart = textBox.Text.Length;
            textBox.SelectionLength = 0;
        }

        public void SetupControleLeeg()
        {
            foreach (TextBox textBox in nietLeeg)
            {
                textBox.LostFocus += MarkeerLegeTextBox;
                textBox.TextChanged += MarkeerLegeTextBox;
            }
        }

        public void SetupControleFloat()
        {
            foreach (TextBox textBox in floatInput)
            {
                textBox.LostFocus += VerkeerdeFloat;
                textBox.TextChanged += VerwerkDecimaalTeken;
            }
        }

        public void SetupControleInt()
        {
            foreach (TextBox textBox in intInput)
            {
                textBox.LostFocus += VerkeerdeInt;
                textBox.TextChanged += VerkeerdeInt;
            }
        }

        public void SetupControleEmailAdres()
        {
            foreach (TextBox textBox in adresInput)
            {
                textBox.LostFocus   += VerkeerdeEmailAdres;
            }
        }

        public void AlleTextBoxenNormaal()
        {
            if( nietLeeg != null )
            {
                foreach ( TextBox textBox in nietLeeg)
                {
                    textBox.BorderBrush = Brushes.Green;
                    textBox.BorderThickness = new Thickness(1);
                }
            }
            if (floatInput != null)
            {
                foreach (TextBox textBox in floatInput)
                {
                    textBox.BorderBrush = Brushes.Green;
                    textBox.BorderThickness = new Thickness(1);
                }
            }
            if (intInput != null)
            {
                foreach (TextBox textBox in intInput)
                {
                    textBox.BorderBrush = Brushes.Green;
                    textBox.BorderThickness = new Thickness(1);
                }
            }
            if (adresInput != null)
            {
                foreach (TextBox textBox in adresInput)
                {
                    textBox.BorderBrush = Brushes.Green;
                    textBox.BorderThickness = new Thickness(1);
                }
            }
        }

        void MarkeerLegeTextBox(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(3);
            }
            else
            {
                textBox.BorderBrush = Brushes.Green;
                textBox.BorderThickness = new Thickness(1);
            }
        }

        void VerkeerdeFloat(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            float getal;

            if (float.TryParse(textBox.Text, out getal))
            {
                textBox.BorderBrush = Brushes.Green;
                textBox.BorderThickness = new Thickness(1);
            }
            else
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(3);
                textBox.Text = "";
            }
        }

        void VerkeerdeInt(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int getal;

            if (int.TryParse(textBox.Text, out getal))
            {
                textBox.BorderBrush = Brushes.Green;
                textBox.BorderThickness = new Thickness(1);
            }
            else
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(3);
                textBox.Text = "";
            }
        }

        // Deze functie gaat na of het email adres zinvol is
        // Dit is geen waterdichte check, er wordt enkel gecheckt op veel voorkomende fouten
        public bool CheckEmailAdres(string emailAdres)
        {
            string userName;
            string domainName;

            bool emailAdresOK = true;

            string[] emailElementen = emailAdres.Split('@');

            // enkele verboden chars
            if (emailAdres.Contains("#") || emailAdres.Contains("/") || emailAdres.Contains("\\") || 
                emailAdres.Contains(" ") || emailAdres.Contains("..") )
            {
                emailAdresOK = false;
            }
            // adres moet uit twee delen bestaan gescheiden door @
            else if (emailElementen.Count() != 2)
            {
                emailAdresOK = false;
            }
            else
            {
                int ampersandPos = emailAdres.IndexOf('@');
                
                if (ampersandPos == emailAdres.Length - 1 || ampersandPos == 0 )
                {
                    emailAdresOK = false;
                }
                else
                {
                    userName = emailAdres.Substring(0, ampersandPos);
                    domainName = emailAdres.Substring(ampersandPos + 1, emailAdres.Length - ampersandPos -1);
                    // domain en user name mogen niet leeg zijn
                    if (userName.Length == 0 || domainName.Length == 0)
                    {
                        emailAdresOK = false;
                    }
                    // domain naam moet minstens 1 punt bevatten (denk ik)
                    else if (!domainName.Contains("."))
                    {
                        emailAdresOK = false;
                    }
                    // niet eindigen op een punt
                    else if (domainName.Substring(domainName.Length - 1, 1) == "." || userName.Substring(userName.Length - 1, 1) == ".")
                    {
                        emailAdresOK = false;
                    }
                    else if (domainName.Substring(0, 1) == "." || userName.Substring(0, 1) == ".")
                    {
                        emailAdresOK = false;
                    }
                }
            }
            return emailAdresOK;
        }

        void VerkeerdeEmailAdres(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if ( CheckEmailAdres(textBox.Text.ToString()) )
            {
                textBox.BorderBrush = Brushes.Green;
                textBox.BorderThickness = new Thickness(1);
            }
            else
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(3);
            }
        }


    }
}
