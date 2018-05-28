using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace DAL
{
    public class ProductEngine
    {
        //private string ConnectionString =
        //    "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Lunar.mdf;Integrated Security = True";

        //Deze connectionstring is voor de unittests
        private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Jeremy van Campen\\OneDrive\\ICT\\Semester 2\\Individueel Lunar\\LunarWebShop\\LunarWebShop\\App_Data\\Lunar.mdf";

        public List<Product> AlleProducten()
        {
            List<Product> producten = new List<Product>();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();


            //Alle bestaande producten ophalen uit de database
            SqlCommand cmd3 = new SqlCommand(
                @"SELECT ProductID, Naam, Uitgever, Genre, Prijs, Foto, AchtergrondFoto FROM Product", con);

            using (SqlDataReader reader = cmd3.ExecuteReader())
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

            //De huidige voorraad ophalen vanuit de database voor elk product aan de hand van een Stored Procedure
            foreach (var item in producten)
            {
                    // 1.  create a command object identifying the stored procedure
                    SqlCommand cmd = new SqlCommand("ProductVoorraad", con);

                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 3. add parameter to command, which will be passed to the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@ProductID", item.ProductID));

                    // execute the command
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        // iterate through results, printing each to console
                        while (rdr.Read())
                        {
                            Keycode keycode = new Keycode();
                            keycode.KeycodeID = rdr.GetInt32(0);
                            item.Keycode.Add(keycode);
                        }
                    }
                foreach (var keycode in item.Keycode)
                {
                    item.Hoeveelheid = item.Hoeveelheid + 1;
                }
            }
            con.Close();
            return producten;
        }


        public void DeleteProduct(int id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Verwijderen van alle keycodes die bij de ProductID behoren
            SqlCommand cmd4 = new SqlCommand(
                @"DELETE FROM [Keycode] WHERE ProductID = @productid", con);
            cmd4.Parameters.AddWithValue("@productid", id);
            cmd4.ExecuteNonQuery();

            //Verwijderen van de Product uit de database aan de hand van de ProductID
            SqlCommand cmd3 = new SqlCommand(
                @"DELETE FROM [Product] WHERE ProductID = @productid", con);
            cmd3.Parameters.AddWithValue("@productid", id);
            cmd3.ExecuteNonQuery();

            con.Close();
        }

        public void CreateProduct(Product product, int hoeveeelheid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Alle gegevens van de doorgegeven product toevoegen aan de database (Product tabel)
            SqlCommand cmd6 =
                new SqlCommand(
                    @"INSERT INTO [Product] (Naam, Uitgever, Genre, Prijs, Foto, AchtergrondFoto) VALUES (@naam, @uitgever, @genre, @prijs, @foto, @achtergrondFoto)", con);
            cmd6.Parameters.AddWithValue("@naam", product.Naam);
            cmd6.Parameters.AddWithValue("@uitgever", product.Uitgever);
            cmd6.Parameters.AddWithValue("@genre", product.Genre);
            cmd6.Parameters.AddWithValue("@prijs", product.Prijs);
            cmd6.Parameters.AddWithValue("@foto", product.Foto);
            cmd6.Parameters.AddWithValue("@achtergrondfoto", product.AchtergrondFoto);
            cmd6.ExecuteNonQuery();

            //De nieuwe automatisch aangemaakte productID ophalen 
            SqlCommand cmd4 = new SqlCommand(
                @"SELECT ProductID FROM [Product] WHERE Naam = @naam AND Uitgever = @uitgever AND Genre = @genre AND Prijs = @prijs AND Foto = @foto AND AchtergrondFoto = @achtergrondfoto", con);
            cmd4.Parameters.AddWithValue("@naam", product.Naam);
            cmd4.Parameters.AddWithValue("@uitgever", product.Uitgever);
            cmd4.Parameters.AddWithValue("@genre", product.Genre);
            cmd4.Parameters.AddWithValue("@prijs", product.Prijs);
            cmd4.Parameters.AddWithValue("@foto", product.Foto);
            cmd4.Parameters.AddWithValue("@achtergrondfoto", product.AchtergrondFoto);

            using (SqlDataReader reader = cmd4.ExecuteReader())
            {
                while (reader.Read())
                {
                    product.ProductID = reader.GetInt32(0);

                }
            }

            //Keycodes toevoegen voor het nieuwe aangemaakte product op bases van de doorgegeven hoeveelheid dus als er 3 wordt doorgegeven worden er 3 keycodes aan verbonden
            for (int i = 0; i != hoeveeelheid; i++)
            {
                SqlCommand cmd3 = new SqlCommand(
                    @"INSERT INTO [Keycode] (ProductID) VALUES (@productid)", con);
                cmd3.Parameters.AddWithValue("@productid", product.ProductID);
                cmd3.ExecuteNonQuery();
            }

            con.Close();
        }

        public void ProductAanpassen(Product product)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Product gegevens aanpassen aan de hand van de doorgegeven ProductID
            SqlCommand cmd6 =
                new SqlCommand(
                    @"UPDATE [Product] SET Naam = @naam, Uitgever = @uitgever, Genre = @genre, Prijs = @prijs, Foto = @foto, AchtergrondFoto = @achtergrondfoto WHERE ProductID = @productid", con);
            cmd6.Parameters.AddWithValue("@naam", product.Naam);
            cmd6.Parameters.AddWithValue("@uitgever", product.Uitgever);
            cmd6.Parameters.AddWithValue("@genre", product.Genre);
            cmd6.Parameters.AddWithValue("@prijs", product.Prijs);
            cmd6.Parameters.AddWithValue("@foto", product.Foto);
            cmd6.Parameters.AddWithValue("@achtergrondfoto", product.AchtergrondFoto);
            cmd6.Parameters.AddWithValue("@productid", product.ProductID);
            cmd6.ExecuteNonQuery();
            con.Close();
        }

        public object ProductOphalen(int id)
        {
            Product product = new Product();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Gegevens van een specifieke product ophalen aan de hand van de ProductID
            SqlCommand cmd3 = new SqlCommand(
                @"SELECT ProductID, Naam, Uitgever, Genre, Prijs, Foto, AchtergrondFoto FROM Product WHERE ProductID = @productid", con);
            cmd3.Parameters.AddWithValue("@productid", id);

            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {
                    product.ProductID = reader.GetInt32(0);
                    product.Naam = reader.GetString(1);
                    product.Uitgever = reader.GetFieldValue<Uitgever>(2);
                    product.Genre = reader.GetFieldValue<Genre>(3);
                    product.Prijs = reader.GetDecimal(4);
                    product.Foto = reader.GetString(5);
                    product.AchtergrondFoto = reader.GetString(6);

                }
            }
            return product;
        }
        public List<Keycode> KeycodeOphalen(int productid)
        {
            List<Keycode> keycodes = new List<Keycode>();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Alle keycodes ophalen van een specifieke product doormiddel van een Stored Procedure die deze allemaal returnt

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("ProductVoorraad", con);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@ProductID", productid));

            // execute the command
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                // iterate through results, printing each to console
                while (rdr.Read())
                {
                    Keycode keycode = new Keycode();
                    keycode.KeycodeID = rdr.GetInt32(0);
                    keycodes.Add(keycode);
                }
            }

            con.Close();
            return keycodes;
        }

        public string ProductVerkopen(int KlantID, int keycodeID)
        {
            int KeycodeID = 0;
            int WinkelwagenID = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //De winkelwagen selecteren die bij de Klant behoord
            SqlCommand cmd5 = new SqlCommand(@"SELECT WinkelwagenID FROM Winkelwagen 
                                        WHERE KlantID = @klantid", con);
            cmd5.Parameters.AddWithValue("@klantid", KlantID);
            using (SqlDataReader reader = cmd5.ExecuteReader())
            {
                while (reader.Read())
                {
                    WinkelwagenID = reader.GetInt32(0);
                }
            }

            //Alle keycodes ophalen die zich in de winkelwagen bevinden
            SqlCommand cmd3 = new SqlCommand(@"SELECT KeycodeID FROM KeycodeWinkelwagen 
                                        WHERE WinkelwagenID = @winkelwagenid AND KeycodeID = @keycode", con);
            cmd3.Parameters.AddWithValue("@keycode", keycodeID);
            cmd3.Parameters.AddWithValue("@winkelwagenid", WinkelwagenID);
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {
                    KeycodeID = reader.GetInt32(0);
                }
            }

            //De keycode koppelen aan de klant zodat hij 'verkocht' is de saldo check etc wordt door een trigger gedaan in mijn database 
            try
            {
                SqlCommand cmd4 = new SqlCommand(@"UPDATE[Keycode] SET KlantID = @klantid WHERE KeycodeID = @keycodeid", con);
                cmd4.Parameters.AddWithValue("@keycodeid", KeycodeID);
                cmd4.Parameters.AddWithValue("@klantid", KlantID);
                cmd4.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                con.Close();
                if (e.ToString().Contains("Niet genoeg Saldo"))
                {
                    return "Onvoldoende Saldo";
                }
                return e.ToString();
            }
            con.Close();
            return String.Empty;
        }

        public List<Product> AlleProductenvanGebruiker(int id)
        {
            List<Keycode> keycodes = new List<Keycode>();
            List<Product> producten = new List<Product>();

            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Selecteren van alle keycodes die bij een klant behoren
            SqlCommand cmd4 = new SqlCommand(
                @"SELECT KeycodeID FROM Keycode where klantID = @klantid", con);
            cmd4.Parameters.AddWithValue("@klantid", id);

            using (SqlDataReader reader = cmd4.ExecuteReader())
            {
                while (reader.Read())
                {
                    Keycode keycode = new Keycode();
                    keycode.KeycodeID = reader.GetInt32(0);
                    keycodes.Add(keycode);
                }
            }

            //ophalen van de gegevens van alle producten die bij de keycodes van de Klant behoren
            foreach (var item in keycodes)
            {
                Product product = new Product();
                SqlCommand cmd3 = new SqlCommand(
                    @"SELECT ProductID FROM Keycode WHERE KeycodeID = @keycodeid", con);
                cmd3.Parameters.AddWithValue("@keycodeid", item.KeycodeID);

                using (SqlDataReader reader = cmd3.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        product.ProductID = reader.GetInt32(0);
                    }
                }

                SqlCommand cmd1 = new SqlCommand(
                    @"SELECT Naam, Uitgever, Genre, Prijs, Foto, AchtergrondFoto FROM Product Where ProductID = @productid", con);
                cmd1.Parameters.AddWithValue("productid", product.ProductID);

                using (SqlDataReader reader2 = cmd1.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        product.Naam = reader2.GetString(0);
                        product.Uitgever = reader2.GetFieldValue<Uitgever>(1);
                        product.Genre = reader2.GetFieldValue<Genre>(2);
                        product.Prijs = reader2.GetDecimal(3);
                        product.Foto = reader2.GetString(4);
                        product.AchtergrondFoto = reader2.GetString(5);
                    }
                }
                product.Keycode.Add(item);
                producten.Add(product);

            }
            return producten;
        }

        public void Voorraadbijvullen(int productid, int hoeveelheid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Aan de hand van de doorgegeven hoeveelheid extra keycodes toevoegen en koppelen aan een product zodat de voorraad wordt bijgevuld
            for (int i = 0; i != hoeveelheid; i++)
            {
                SqlCommand cmd3 = new SqlCommand(
                    @"INSERT INTO [Keycode] (ProductID) VALUES (@productid)", con);
                cmd3.Parameters.AddWithValue("@productid", productid);
                cmd3.ExecuteNonQuery();
            }
        }

        public List<Product> ProductenGenre(Genre Genre)
        {
            List<Product> producten = new List<Product>();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Ophalen van alle producten aan de hand van een bepaald Genre zodat de klant kan sorteren op genre
            SqlCommand cmd3 = new SqlCommand(
                @"SELECT ProductID, Naam, Uitgever, Genre, Prijs, Foto, AchtergrondFoto FROM Product Where Genre = @genre", con);
            cmd3.Parameters.AddWithValue("@genre", Genre);

            using (SqlDataReader reader = cmd3.ExecuteReader())
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

            foreach (var item in producten)
            {
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("ProductVoorraad", con);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@ProductID", item.ProductID));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        Keycode keycode = new Keycode();
                        keycode.KeycodeID = rdr.GetInt32(0);
                        item.Keycode.Add(keycode);
                    }
                }
                foreach (var keycode in item.Keycode)
                {
                    item.Hoeveelheid = item.Hoeveelheid + 1;
                }
            }
            con.Close();
            return producten;
        }

        public List<Product> ProductenPrijsHoogLaag()
        {
            List<Product> producten = new List<Product>();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Ophalen van alle producten gesorteerd op prijs van hoog naar laag
            SqlCommand cmd3 = new SqlCommand(
                @"SELECT* FROM Product ORDER BY Prijs DESC", con);
            using (SqlDataReader reader = cmd3.ExecuteReader())
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

            foreach (var item in producten)
            {
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("ProductVoorraad", con);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@ProductID", item.ProductID));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        Keycode keycode = new Keycode();
                        keycode.KeycodeID = rdr.GetInt32(0);
                        item.Keycode.Add(keycode);
                    }
                }
                foreach (var keycode in item.Keycode)
                {
                    item.Hoeveelheid = item.Hoeveelheid + 1;
                }
            }
            con.Close();
            return producten;
        }
        public List<Product> ProductenPrijsLaagHoog()
        {
            List<Product> producten = new List<Product>();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            //Ophalen van alle producten gesorteerd op prijs van laag naar hoog
            SqlCommand cmd3 = new SqlCommand(
                @"SELECT* FROM Product ORDER BY Prijs ASC", con);
            using (SqlDataReader reader = cmd3.ExecuteReader())
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

            foreach (var item in producten)
            {
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("ProductVoorraad", con);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@ProductID", item.ProductID));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        Keycode keycode = new Keycode();
                        keycode.KeycodeID = rdr.GetInt32(0);
                        item.Keycode.Add(keycode);
                    }
                }
                foreach (var keycode in item.Keycode)
                {
                    item.Hoeveelheid = item.Hoeveelheid + 1;
                }
            }
            con.Close();
            return producten;
        }
    }

}