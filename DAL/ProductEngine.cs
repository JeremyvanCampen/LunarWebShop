using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace DAL
{
    public class ProductEngine
    {
        private string ConnectionString =
            "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Lunar.mdf;Integrated Security = True";

        public List<Product> AlleProducten()
        {
            List<Product> producten = new List<Product>();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

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

            foreach (var item in producten)
            {
                SqlCommand cmd4 = new SqlCommand(
                    @"SELECT KeycodeID FROM Keycode where ProductID = @productid", con);
                cmd4.Parameters.AddWithValue("@productid", item.ProductID);

                using (SqlDataReader reader = cmd4.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Keycode keycode = new Keycode();
                        keycode.KeycodeID = reader.GetInt32(0);
                        item.Keycode.Add(keycode);
                    }
                }
            }
            return producten;
        }


        public void DeleteProduct(int id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            SqlCommand cmd4 = new SqlCommand(
                @"DELETE FROM [Keycode] WHERE ProductID = @productid", con);
            cmd4.Parameters.AddWithValue("@productid", id);
            cmd4.ExecuteNonQuery();

            SqlCommand cmd3 = new SqlCommand(
                @"DELETE FROM [Product] WHERE ProductID = @productid", con);
            cmd3.Parameters.AddWithValue("@productid", id);
            cmd3.ExecuteNonQuery();

            con.Close();
        }

        public void CreateProduct(Product product)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
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

            SqlCommand cmd3 = new SqlCommand(
                @"INSERT INTO [Keycode] (ProductID) VALUES (@productid)", con);
            cmd3.Parameters.AddWithValue("@productid", product.ProductID);
            cmd3.ExecuteNonQuery();

            con.Close();
        }

        public void ProductAanpassen(Product product)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
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
        public object KeycodeOphalen(int productid)
        {
            Keycode keycode = new Keycode();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            SqlCommand cmd3 = new SqlCommand(
                @"SELECT KeycodeID, ProductID FROM Keycode WHERE ProductID = @productid", con);
            cmd3.Parameters.AddWithValue("@productid", productid);

            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {
                    keycode.KeycodeID = reader.GetInt32(0);
                    keycode.ProductID = reader.GetInt32(1);
                }
            }

            return keycode;
        }

        public string ProductVerkopen(int KlantID, int ProductID)
        {
            int keycodeID = 0;
            int WinkelwagenID = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

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

            SqlCommand cmd3 = new SqlCommand(@"SELECT KeycodeID FROM Keycode 
                                        WHERE ProductID = @productid AND WinkelwagenID = @winkelwagenid", con);
            cmd3.Parameters.AddWithValue("@productid", ProductID);
            cmd3.Parameters.AddWithValue("@winkelwagenid", WinkelwagenID);
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {
                while (reader.Read())
                {
                    keycodeID = reader.GetInt32(0);
                }
            }

            try
            {
                SqlCommand cmd4 = new SqlCommand(@"UPDATE[Keycode] SET KlantID = @klantid WHERE KeycodeID = @keycodeid", con);
                cmd4.Parameters.AddWithValue("@keycodeid", keycodeID);
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
    }
}
