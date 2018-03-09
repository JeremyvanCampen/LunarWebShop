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

            //public void CreateAdministrator(Administrator admin)
            //{
            //    //Toevoeging aan database
            //    SqlConnection con = new SqlConnection(ConnectionString);
            //    con.Open();
            //    string query = "INSERT INTO Gebruiker(Voornaam,Achternaam,Gebruikersnaam,Wachtwoord,Email,Geboortedatum)";
            //    query += " VALUES (@voornaam, @achternaam, @gebruikersnaam, @wachtwoord, @email, @geboortedatum)";
            //    SqlCommand cmd = new SqlCommand(query, con);
            //    cmd.Parameters.AddWithValue("@voornaam", admin.Voornaam);
            //    cmd.Parameters.AddWithValue("@achternaam", admin.Achternaam);
            //    cmd.Parameters.AddWithValue("@gebruikersnaam", admin.Gebruikersnaam);
            //    cmd.Parameters.AddWithValue("@wachtwoord", admin.Wachtwoord);
            //    cmd.Parameters.AddWithValue("@email", admin.Email);
            //    cmd.Parameters.AddWithValue("@geboortedatum", admin.Geboortedatum);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //}

        public bool CreateKlant(Gebruiker klant)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //EmailCheck
            SqlCommand checkemail = new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Email] = @email)", con);
            checkemail.Parameters.AddWithValue("@email", klant.Email);
            int EmailBestaatal = (int) checkemail.ExecuteScalar();

            //GebruikersnaamCheck
            SqlCommand checkGebruikersnaam = new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Gebruikersnaam] = @gebruikersnaam)", con);
            checkGebruikersnaam.Parameters.AddWithValue("@gebruikersnaam", klant.Gebruikersnaam);
            int GebruikerBestaatal = (int)checkGebruikersnaam.ExecuteScalar();

            if (EmailBestaatal > 0)
            {
                //Gebruiker bestaat al kan niet worden toegevoegd
                con.Close();
                return false;

            }
            else if (GebruikerBestaatal > 0)
            {
                //Gebruiker bestaat al kan niet worden toegevoegd
                con.Close();
                return false;
            }
                //Gebruiker toevoegen aan database
                string query = "INSERT INTO Gebruiker(Voornaam,Achternaam,Gebruikersnaam,Wachtwoord,Email,Geboortedatum)";
                query += " VALUES (@voornaam, @achternaam, @gebruikersnaam, @wachtwoord, @email, @geboortedatum)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@voornaam", klant.Voornaam);
                cmd.Parameters.AddWithValue("@achternaam", klant.Achternaam);
                cmd.Parameters.AddWithValue("@gebruikersnaam", klant.Gebruikersnaam);
                cmd.Parameters.AddWithValue("@wachtwoord", klant.Wachtwoord);
                cmd.Parameters.AddWithValue("@email", klant.Email);
                cmd.Parameters.AddWithValue("@geboortedatum", klant.Geboortedatum);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
        }
        public string Inloggen(string gebruikersnaam, string wachtwoord)
        {

            //Controleren van de inloggegevens
            //Toevoegen van sqlconnection en de juiste tabellen controleren zodra deze zijn aangemaakt
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT Count(*) FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname and 
                                        Wachtwoord=@pass", con);
                cmd.Parameters.AddWithValue("@uname", gebruikersnaam);
                cmd.Parameters.AddWithValue("@pass", wachtwoord);
                int result = (int)cmd.ExecuteScalar();
            if (result > 0)
            {
                return "Goed";
            }
            return "Fout";

        }
    }
}