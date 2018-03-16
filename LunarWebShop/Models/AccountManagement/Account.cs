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

        public string AdmministratorToevoegen(Administrator Admin)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //EmailCheck
            SqlCommand checkemail = new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Email] = @email)", con);
            checkemail.Parameters.AddWithValue("@email", Admin.Email);
            int EmailBestaatal = (int)checkemail.ExecuteScalar();

            //GebruikersnaamCheck
            SqlCommand checkGebruikersnaam = new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Gebruikersnaam] = @gebruikersnaam)", con);
            checkGebruikersnaam.Parameters.AddWithValue("@gebruikersnaam", Admin.Gebruikersnaam);
            int GebruikerBestaatal = (int)checkGebruikersnaam.ExecuteScalar();

            if (EmailBestaatal > 0)
            {
                //Gebruiker bestaat al kan niet worden toegevoegd
                con.Close();
                return " Email bestaat al";

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
                cmd.Parameters.AddWithValue("@voornaam", Admin.Voornaam);
                cmd.Parameters.AddWithValue("@achternaam", Admin.Achternaam);
                cmd.Parameters.AddWithValue("@gebruikersnaam", Admin.Gebruikersnaam);
                cmd.Parameters.AddWithValue("@wachtwoord", Admin.Wachtwoord);
                cmd.Parameters.AddWithValue("@email", Admin.Email);
                cmd.Parameters.AddWithValue("@geboortedatum", Admin.Geboortedatum);
                cmd.ExecuteNonQuery();

                //Klant aanmaken met Foreignkey van Gebruiker
                string KlantQuery = "INSERT INTO Administrator(GebruikerID)";
                KlantQuery += " VALUES ((SELECT GebruikerID FROM Gebruiker WHERE Gebruikersnaam = @gebruikersnaam))";
                SqlCommand cmd2 = new SqlCommand(KlantQuery, con);
                cmd2.Parameters.AddWithValue("@gebruikersnaam", Admin.Gebruikersnaam);
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

        public object Inloggen(string gebruikersnaam, string wachtwoord)
        {

            int resultGebruiker;
            int resultKlant;
            int resultAdmin;
            SqlConnection con = new SqlConnection(ConnectionString);

            //GebruikerCheck

            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT GebruikerID FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname and 
                                        Wachtwoord=@pass", con);
            cmd.Parameters.AddWithValue("@uname", gebruikersnaam);
            cmd.Parameters.AddWithValue("@pass", wachtwoord);
            resultGebruiker = (int)cmd.ExecuteScalar();
            con.Close();

            //KlantCheck

            con.Open();
            SqlCommand cmd1 = new SqlCommand(@"SELECT Count(*) FROM Klant 
                                        WHERE GebruikerID=@gebruikerid", con);
            cmd1.Parameters.AddWithValue("@gebruikerid", resultGebruiker);
            resultKlant = (int)cmd1.ExecuteScalar();
            con.Close();

            //AdminCheck

            con.Open();
            SqlCommand cmd2 = new SqlCommand(@"SELECT Count(*) FROM Administrator 
                                        WHERE GebruikerID=@gebruikerid", con);
            cmd2.Parameters.AddWithValue("@gebruikerid", resultGebruiker);
            resultAdmin = (int)cmd2.ExecuteScalar();
            con.Close();


            //Inloggegevens kloppen en diegene die inlogt is een Klant

            if (resultKlant > 0)
            {
               //Klant aanmaken en gegevens invullen vanuit de database
                Klant klant = new Klant();
                SqlCommand cmd3 = new SqlCommand(@"SELECT GebruikerID, Voornaam, Achternaam, Email, Geboortedatum  FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname and 
                                        Wachtwoord=@pass", con);
                cmd3.Parameters.AddWithValue("@uname", gebruikersnaam);
                cmd3.Parameters.AddWithValue("@pass", wachtwoord);
                con.Open();

                using (SqlDataReader reader = cmd3.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        klant.GebruikerID = reader.GetInt32(0);
                        klant.Voornaam = reader.GetString(1);
                        klant.Achternaam = reader.GetString(2);
                        klant.Email = reader.GetString(3);
                        klant.Geboortedatum = reader.GetDateTime(4);
                        klant.Gebruikersnaam = gebruikersnaam;
                    }
                }

                SqlCommand cmd8 = new SqlCommand(@"SELECT Saldo, Straat, Huisnummer FROM Klant 
                                        WHERE GebruikerID=@gebruikerid", con);
                cmd8.Parameters.AddWithValue("@gebruikerid", klant.GebruikerID);

                using (SqlDataReader reader2 = cmd8.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        klant.Saldo = reader2.GetDecimal(0);
                        klant.Straat = reader2.GetString(1);
                        klant.Huisnummer = reader2.GetInt32(2);
                    }
                }
                con.Close();
                return klant;

            }

            //Inloggegevens kloppen en diegene die inlogt is een Administrator

            if (resultAdmin > 0)
            {
                //Klant aanmaken en gegevens invullen vanuit de database
                Administrator Administrator = new Administrator();
                SqlCommand cmd3 = new SqlCommand(@"SELECT GebruikerID, Voornaam, Achternaam, Email, Geboortedatum  FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname and 
                                        Wachtwoord=@pass", con);
                cmd3.Parameters.AddWithValue("@uname", gebruikersnaam);
                cmd3.Parameters.AddWithValue("@pass", wachtwoord);
                con.Open();

                using (SqlDataReader reader = cmd3.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Administrator.GebruikerID = reader.GetInt32(0);
                        Administrator.Voornaam = reader.GetString(1);
                        Administrator.Achternaam = reader.GetString(2);
                        Administrator.Email = reader.GetString(3);
                        Administrator.Geboortedatum = reader.GetDateTime(4);
                        Administrator.Gebruikersnaam = gebruikersnaam;
                    }
                }
                con.Close();
                return Administrator;
            }

            return "Fout";
        }
    }
}