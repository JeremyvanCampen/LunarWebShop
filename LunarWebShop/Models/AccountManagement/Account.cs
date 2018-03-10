using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using LunarWebShop.Models;

namespace LunarWebShop.Models.AccountManagement
{
    public class Account
    {

        private string ConnectionString =@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jeremy van Campen\Dropbox\ICT\Semester 2\Individueel Lunar\LunarWebShop\LunarWebShop\App_Data\Lunar.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            
        //intialise  
            public Account()
            {

            }

        public string KlantToevoegen(Klant klant)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //EmailCheck
            SqlCommand checkemail = new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Email] = @email)", con);
            checkemail.Parameters.AddWithValue("@email", klant.Email);
            int EmailBestaatal = (int)checkemail.ExecuteScalar();

            //GebruikersnaamCheck
            SqlCommand checkGebruikersnaam = new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Gebruikersnaam] = @gebruikersnaam)", con);
            checkGebruikersnaam.Parameters.AddWithValue("@gebruikersnaam", klant.Gebruikersnaam);
            int GebruikerBestaatal = (int)checkGebruikersnaam.ExecuteScalar();

            if (EmailBestaatal > 0)
            {
                //Gebruiker bestaat al kan niet worden toegevoegd
                con.Close();
                return " Email bestaat al" ;

            }

            else if (GebruikerBestaatal > 0)
            {
                //Gebruiker bestaat al kan niet worden toegevoegd
                con.Close();
                return " Gebruiker bestaat al";
            }

            try
            {
                //Gebruiker toevoegen aan database
                string GebruikerQuery = "INSERT INTO Gebruiker(Voornaam,Achternaam,Gebruikersnaam,Wachtwoord,Email,Geboortedatum)";
                GebruikerQuery += " VALUES (@voornaam, @achternaam, @gebruikersnaam, @wachtwoord, @email, @geboortedatum)";
                SqlCommand cmd = new SqlCommand(GebruikerQuery, con);
                cmd.Parameters.AddWithValue("@voornaam", klant.Voornaam);
                cmd.Parameters.AddWithValue("@achternaam", klant.Achternaam);
                cmd.Parameters.AddWithValue("@gebruikersnaam", klant.Gebruikersnaam);
                cmd.Parameters.AddWithValue("@wachtwoord", klant.Wachtwoord);
                cmd.Parameters.AddWithValue("@email", klant.Email);
                cmd.Parameters.AddWithValue("@geboortedatum", klant.Geboortedatum);
                cmd.ExecuteNonQuery();

                //Klant aanmaken met Foreignkey van Gebruiker
                string KlantQuery = "INSERT INTO Klant(Saldo, GebruikerID, Straat, Huisnummer)";
                KlantQuery += " VALUES (@saldo,(SELECT GebruikerID FROM Gebruiker WHERE Gebruikersnaam = @gebruikersnaam), @straat, @huisnummer)";
                SqlCommand cmd2 = new SqlCommand(KlantQuery, con);
                cmd2.Parameters.AddWithValue("@saldo", klant.Saldo);
                cmd2.Parameters.AddWithValue("@gebruikersnaam", klant.Gebruikersnaam);
                cmd2.Parameters.AddWithValue("@straat", klant.Straat);
                cmd2.Parameters.AddWithValue("@huisnummer", klant.Huisnummer);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                string error = e.ToString();
                return error;
            }
            return "Succesvol";
        }

        public string Inloggen(string gebruikersnaam, string wachtwoord)
        {
            int result;
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT Count(*) FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname and 
                                        Wachtwoord=@pass", con);
                cmd.Parameters.AddWithValue("@uname", gebruikersnaam);
                cmd.Parameters.AddWithValue("@pass", wachtwoord);
                result = (int)cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                string error = e.ToString();
                return error;
            }
            if (result > 0)
            {
                return "Succesvol";
            }

            return "Fout";
        }
    }
}