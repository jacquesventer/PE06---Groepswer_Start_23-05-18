using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using MailingList.Lib.Entities;

namespace MailingList.Lib.Services
{
    public class MailWinaars
    {
        string bestandsPad = AppDomain.CurrentDomain.BaseDirectory + "../../../MailingList.accdb";
        public List<Deelnemer> winnaars;
        OleDbConnection dbConn;
        OleDbCommand sqlCommand;

        public MailWinaars()
        {
                dbConn = new OleDbConnection();
                dbConn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + bestandsPad;
            winnaars = new List<Deelnemer>();
        }


        void SluitConnectie()
        {
            if (dbConn != null) { dbConn.Close(); }
        }

        public bool SelecteerWinaar()
        {
            bool gelukt = false;
            OleDbDataReader dbRead = null;
            string sqlopdracht = "select * FROM tblMailingList WHERE Answer = 'true' ";
            try
            {
                dbConn.Open();
                sqlCommand = new OleDbCommand(sqlopdracht, dbConn);
                dbRead = sqlCommand.ExecuteReader();
                while (dbRead.Read())
                {
                    int Id = dbRead.GetInt32(0);
                    string FirstName = dbRead.GetString(1).ToString();
                    string LastName = dbRead.GetString(2).ToString();
                    string Email = dbRead.GetString(3).ToString();
                    string Phone = dbRead.GetString(4);
                    string Street = dbRead.GetString(5).ToString();
                    int StreetNumber = dbRead.GetInt32(6);
                    string City = dbRead.GetString(7).ToString();
                    int PostalCode = dbRead.GetInt32(8);
                    string Answer = dbRead.GetString(9).ToString();
                    Deelnemer winnaar = new Deelnemer(Id, FirstName, LastName, Email, Phone, Street, StreetNumber, City, PostalCode, Answer);
                    winnaars.Add(winnaar);
                }
                gelukt = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (dbRead != null)
                {
                    dbRead.Close();
                }
                SluitConnectie();
            }
            SorteerDeelnemers();
            return gelukt;
        }
        string MaakSorteerSleutel(Deelnemer deelnemer)
        {
            string sorteerSleutel = "";
            sorteerSleutel = deelnemer.LastName + "," + deelnemer.FirstName;
            sorteerSleutel = sorteerSleutel.ToUpper();
            sorteerSleutel = sorteerSleutel.Replace(' ', '\0');
            return sorteerSleutel;
        }

        void SorteerDeelnemers()
        {
            List<string> teSorteren = new List<string>();
            foreach (Deelnemer deelnemer in winnaars)
            {
                teSorteren.Add(MaakSorteerSleutel(deelnemer));
            }
            teSorteren.Sort();
            List<Deelnemer> gesorteerd = new List<Deelnemer>();
            foreach (string sorteerNaam in teSorteren)
            {
                foreach (Deelnemer _deelnemer in winnaars)
                {
                    if (sorteerNaam == MaakSorteerSleutel(_deelnemer))
                    {
                        gesorteerd.Add(_deelnemer);
                        break;
                    }
                }
            }
            winnaars = gesorteerd;
        }

        public string OpmaakEmail(Deelnemer winnaar)
        {
            string rapport = paragraph("Beste, " + winnaar.LastName + " " + winnaar.FirstName);
            return rapport;

        }

        static string paragraph(string input)
        {
            string html = $"<p>{input}</p>";
            return html;
        }

    }
}
