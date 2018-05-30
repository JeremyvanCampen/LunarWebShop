using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class GebruikerEngine
    {
        //private string ConnectionString =
        //    "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Lunar.mdf;Integrated Security = True";

        //Deze connectionstring is voor de unittests
        private string ConnectionString = "Server=tcp:jeremyserver.database.windows.net,1433;Initial Catalog=LunarDatabase;Persist Security Info=False;User ID=ketqckrt1;Password=Ump617648;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public string KlantToevoegen(Klant klant)
        {
          int GebruikerID = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //EmailCheck
            SqlCommand checkemail = new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Email] = @email)", con);
            checkemail.Parameters.AddWithValue("@email", klant.Email);
            int EmailBestaatal = (int)checkemail.ExecuteScalar();

            //GebruikersnaamCheck
            SqlCommand checkGebruikersnaam =
                new SqlCommand("SELECT COUNT(*) FROM [Gebruiker] WHERE ([Gebruikersnaam] = @gebruikersnaam)", con);
            checkGebruikersnaam.Parameters.AddWithValue("@gebruikersnaam", klant.Gebruikersnaam);
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
                string GebruikerQuery =
                    "INSERT INTO Gebruiker(Voornaam,Achternaam,Gebruikersnaam,Wachtwoord,Email,Geboortedatum)";
                GebruikerQuery +=
                    " VALUES (@voornaam, @achternaam, @gebruikersnaam, @wachtwoord, @email, @geboortedatum)";
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
                KlantQuery +=
                    " VALUES (@saldo,(SELECT GebruikerID FROM Gebruiker WHERE Gebruikersnaam = @gebruikersnaam), @straat, @huisnummer)";
                SqlCommand cmd2 = new SqlCommand(KlantQuery, con);
                cmd2.Parameters.AddWithValue("@saldo", klant.Saldo);
                cmd2.Parameters.AddWithValue("@gebruikersnaam", klant.Gebruikersnaam);
                cmd2.Parameters.AddWithValue("@straat", klant.Straat);
                cmd2.Parameters.AddWithValue("@huisnummer", klant.Huisnummer);
                cmd2.ExecuteNonQuery();

                //GebruikerID ophalen voor winkelwagen creatie
                SqlCommand cmd3 = new SqlCommand(@"SELECT GebruikerID FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname and 
                                        Wachtwoord=@pass", con);
                cmd3.Parameters.AddWithValue("@uname", klant.Gebruikersnaam);
                cmd3.Parameters.AddWithValue("@pass", klant.Wachtwoord);
                using (SqlDataReader reader = cmd3.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GebruikerID = reader.GetInt32(0);
                    }
                }

                //Winkelwagen aanmaken
                string WinkelwagenQuery =
                    "INSERT INTO Winkelwagen(KlantID) Values((SELECT KlantID FROM [Klant] WHERE GebruikerID = @gebruikerID))";
                SqlCommand cmd5 = new SqlCommand(WinkelwagenQuery, con);
                cmd5.Parameters.AddWithValue("@gebruikerID", GebruikerID);
                cmd5.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                string error = e.ToString();
                con.Close();
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
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT GebruikerID FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname and 
                                        Wachtwoord=@pass", con);
                cmd.Parameters.AddWithValue("@uname", gebruikersnaam);
                cmd.Parameters.AddWithValue("@pass", wachtwoord);
                resultGebruiker = (int)cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return " Account Gegevens bestaan niet.";
            }

            try
            {
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
                    SqlCommand cmd3 = new SqlCommand(
                        @"SELECT GebruikerID, Voornaam, Achternaam, Email, Geboortedatum  FROM Gebruiker 
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

                    SqlCommand cmd8 = new SqlCommand(@"SELECT KlantID,Saldo, Straat, Huisnummer FROM Klant 
                                        WHERE GebruikerID=@gebruikerid", con);
                    cmd8.Parameters.AddWithValue("@gebruikerid", klant.GebruikerID);

                    using (SqlDataReader reader2 = cmd8.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            klant.KlantID = reader2.GetInt32(0);
                            klant.Saldo = reader2.GetDecimal(1);
                            klant.Straat = reader2.GetString(2);
                            klant.Huisnummer = reader2.GetInt32(3);
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
                    SqlCommand cmd3 = new SqlCommand(
                        @"SELECT GebruikerID, Voornaam, Achternaam, Email, Geboortedatum  FROM Gebruiker 
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
            }
            catch (Exception e)
            {
                string error = e.ToString();
                return error;
                ;
            }

            return " Onbekende fout";
        }

        public List<Product> WinkelwagenProducten(int KlantID)
        {
            int WinkelwagenID = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            List<Keycode> keycodes = new List<Keycode>();
            List<Product> producten = new List<Product>();
            List<int> productids = new List<int>();
            con.Open();

            //Selecteren van de juiste winkelwagen behorende bij de juiste klant doormiddel van een KlantID
            SqlCommand cmd3 = new SqlCommand(@"SELECT WinkelwagenID FROM Winkelwagen 
                                        WHERE KlantID = @klantid", con);
            cmd3.Parameters.AddWithValue("@klantid", KlantID);
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {
                    WinkelwagenID = reader.GetInt32(0);
                }
            }

            //Selecteren van alle keycode's die zich in de winkelwagen bevinden zodat de bijbehorende producten kunnen worden opgehaald
            SqlCommand cmd4 = new SqlCommand(@"SELECT KeycodeID FROM KeycodeWinkelwagen
                                        WHERE WinkelwagenID = @winkelwagenid", con);
            cmd4.Parameters.AddWithValue("@winkelwagenid", WinkelwagenID);
            using (SqlDataReader reader = cmd4.ExecuteReader())
            {
                while (reader.Read())
                {
                    Keycode keycode = new Keycode();
                    keycode.KeycodeID = reader.GetInt32(0);
                    keycodes.Add(keycode);
                }
            }
            //Ophalen van de productID's die bij de opgehaalde keycodes behoren zodat informatie over deze producten kunnen worden opgehaald
            foreach (var item in keycodes)
            {
                SqlCommand cmd5 = new SqlCommand(@"SELECT ProductID FROM Keycode WHERE keycodeid = @keycodeid", con);
                cmd5.Parameters.AddWithValue("@keycodeid", item.KeycodeID);

                using (SqlDataReader reader = cmd5.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productids.Add(reader.GetInt32(0));

                    }
                }
            }

            //Selecteren van alle gegevens van de producten die bij de eerder opgehaalde productID's behoren (Naam, prijs etc.)
            foreach (var item in productids)
            {
                SqlCommand cmd6 =
                    new SqlCommand(
                        @"SELECT ProductID, Naam, Uitgever, Genre, Prijs, Foto, AchtergrondFoto FROM Product WHERE ProductID = @productid", con);
                cmd6.Parameters.AddWithValue("@productid", item);

                using (SqlDataReader reader = cmd6.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = reader.GetInt32(0);
                        product.Naam = reader.GetString(1);
                        product.Uitgever = reader.GetFieldValue<Uitgever>(2);
                        product.Genre = reader.GetFieldValue<Genre>(3);
                        product.Prijs = reader.GetDecimal(4);
                        product.Foto = reader.GetString(5);
                        product.AchtergrondFoto = reader.GetString(6);
                        producten.Add(product);
                    }
                }
            }

            //Alle keycodes en producten aan elkaar koppelen en dat returnen zodat dit kan worden weergegeven in de View
            for (int i = 0; i != producten.Count; i++)
            {
                producten[i].Keycode.Add(keycodes[i]);
            }
            con.Close();
            return producten;
        }

        public String VoegToeAanWinkelwagen(int KlantID, int ProductID)
        {
            int WinkelwagenID = 0;
            int keycodeid = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Selecteren van de juiste winkelwagen behorende bij de juiste klant doormiddel van een KlantID
            SqlCommand cmd3 = new SqlCommand(@"SELECT WinkelwagenID FROM Winkelwagen 
                                        WHERE KlantID = @klantid", con);
            cmd3.Parameters.AddWithValue("@klantid", KlantID);
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {
                    WinkelwagenID = reader.GetInt32(0);
                }
            }

            //Selecteren van de eerste vrije keycode in de database
            SqlCommand cmd4 = new SqlCommand(@"SELECT TOP 1 KeycodeID FROM Keycode WHERE ProductID = @productID AND KlantID IS NULL AND KeycodeID not in (Select keycodeID FROM KeycodeWinkelwagen WHERE WinkelwagenID = (SELECT Winkelwagenid from Winkelwagen Where WinkelwagenID = @winkelwagenid))", con);
            cmd4.Parameters.AddWithValue("@productID", ProductID);
            cmd4.Parameters.AddWithValue("@winkelwagenid", WinkelwagenID);
            using (SqlDataReader reader = cmd4.ExecuteReader())
            {
                    while (reader.Read())
                    {
                        keycodeid = reader.GetInt32(0);
                    }
            }
                
                //Als er geen keycode in de database aanwezig is zal er Null worden gereturnd en dan return ik naar mijn View "Geen voorraad"
                if (keycodeid == 0)
                {
                    return "Geen Voorraad";
                }

                    //De keycode die is geselecteerd koppelen aan de winkelwagen van de Klant
                    string UpdateWinkelwagen = "INSERT INTO [KeycodeWinkelwagen](WinkelwagenID,KeycodeID) Values(@winkelwagenid,@keycodeid)";
                    SqlCommand cmd5 = new SqlCommand(UpdateWinkelwagen, con);
                    cmd5.Parameters.AddWithValue("@keycodeid", keycodeid);
                    cmd5.Parameters.AddWithValue("@winkelwagenid", WinkelwagenID);
                    cmd5.ExecuteNonQuery();
                    con.Close();

                    return "Succes";
        }

        public void VerwijderUitWinkelwagen(int KeycodeID, int productid, int klantid)
        {
            int WinkelwagenID = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Selecteren van de juiste winkelwagen behorende bij de juiste klant doormiddel van een KlantID
            SqlCommand cmd3 = new SqlCommand(@"SELECT WinkelwagenID FROM Winkelwagen 
                                        WHERE KlantID = @klantid", con);
            cmd3.Parameters.AddWithValue("@klantid", klantid);
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {
                    WinkelwagenID = reader.GetInt32(0);
                }
            }

            //Verwijderen van de keycode uit een winkelwagen waar de keycodeID en winkelwagenID allebei aanwezig zijn
            string WinkelwagenQuery = "DELETE FROM KeycodeWinkelwagen WHERE KeycodeID = @KeycodeID AND WinkelwagenID = @winkelwagenID;";
            SqlCommand cmd5 = new SqlCommand(WinkelwagenQuery, con);
            cmd5.Parameters.AddWithValue("@keycodeID", KeycodeID);
            cmd5.Parameters.AddWithValue("@winkelwagenid", WinkelwagenID);
            cmd5.ExecuteNonQuery();
            con.Close();
        }

        public void SaldoToevoegen(decimal Saldotoevoegen, int id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Selecteren van de juiste saldo van de klant aan de hand van de KlantID
            SqlCommand cmd3 = new SqlCommand(@"SELECT Saldo FROM Klant 
                                        WHERE KlantID = @klantid", con);
            cmd3.Parameters.AddWithValue("@klantid", id);
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {

                    //De huidige saldo (vanuit de database) + de gewenste bedrag dat wordt toegevoegd
                    Saldotoevoegen = reader.GetDecimal(0) + Saldotoevoegen;
                }
            }

            //Updaten van de saldo van de gebruiker
            string WinkelwagenQuery = "UPDATE Klant SET Saldo = @saldo WHERE KlantID = @klantid;";
            SqlCommand cmd5 = new SqlCommand(WinkelwagenQuery, con);

            cmd5.Parameters.AddWithValue("@saldo", Saldotoevoegen);
            cmd5.Parameters.AddWithValue("@klantid", id);
            cmd5.ExecuteNonQuery();

            con.Close();

        }

        public object KlantgegevensZonderSaldo(int id, string gebruikersnaam)
        {

            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            Klant klant = new Klant();

            //Selecteren van alle gegevens van een klant min de de saldo aan de hand van de gebruikersnaam uit de Gebruiker tabel
            SqlCommand cmd3 = new SqlCommand(
                @"SELECT GebruikerID, Voornaam, Achternaam, Email, Geboortedatum  FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname", con);
            cmd3.Parameters.AddWithValue("@uname", gebruikersnaam);

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

            //Selecteren van straat en huisnummer uit de Klant tabel
            SqlCommand cmd8 = new SqlCommand(@"SELECT KlantID, Straat, Huisnummer FROM Klant 
                                        WHERE KlantID=@klantid", con);
            cmd8.Parameters.AddWithValue("@klantid", id);

            using (SqlDataReader reader2 = cmd8.ExecuteReader())
            {
                while (reader2.Read())
                {
                    klant.KlantID = reader2.GetInt32(0);
                    klant.Straat = reader2.GetString(1);
                    klant.Huisnummer = reader2.GetInt32(2);

                }
            }

            con.Close();
            return klant;
        }
        public object KlantgegevensVolledig(int id, string gebruikersnaam)
        {

            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            Klant klant = new Klant();

            //Selecteren van alle gegevens van een klant aan de hand van de gebruikersnaam uit de Gebruiker tabel
            SqlCommand cmd3 = new SqlCommand(
                @"SELECT GebruikerID, Voornaam, Achternaam, Email, Geboortedatum  FROM Gebruiker 
                                        WHERE Gebruikersnaam=@uname", con);
            cmd3.Parameters.AddWithValue("@uname", gebruikersnaam);

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


            //Selecteren van straat, saldo en huisnummer uit de Klant tabel
            SqlCommand cmd8 = new SqlCommand(@"SELECT KlantID,Saldo, Straat, Huisnummer FROM Klant 
                                        WHERE KlantID=@klantid", con);
            cmd8.Parameters.AddWithValue("@klantid", id);

            using (SqlDataReader reader2 = cmd8.ExecuteReader())
            {
                while (reader2.Read())
                {
                    klant.KlantID = reader2.GetInt32(0);
                    klant.Saldo = reader2.GetDecimal(1);
                    klant.Straat = reader2.GetString(2);
                    klant.Huisnummer = reader2.GetInt32(3);

                }
            }

            con.Close();
            return klant;
        }

    }
}
