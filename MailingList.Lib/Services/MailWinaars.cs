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
        string bestandsPad = AppDomain.CurrentDomain.BaseDirectory + "MailingList.accdb";
        public List<Deelnemer> winnaars;
        OleDbConnection dbConn;
        OleDbCommand sqlCommand;

        public MailWinaars()
        {
                dbConn = new OleDbConnection();
                dbConn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + bestandsPad;
        }


        void SluitConnectie()
        {
            if (dbConn != null) { dbConn.Close(); }
        }

        public bool SelecteerWinaar()
        {
            bool gelukt = false;
            OleDbDataReader dbRead = null;
            string sqlopdracht = "select * FROM tblMailingList WHERE Answer = true ";
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
                    int Phone = dbRead.GetInt32(4);
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
            return gelukt;
        }
    }
}
