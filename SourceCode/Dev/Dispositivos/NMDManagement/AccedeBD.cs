using System;
using System.Data.SqlClient;


namespace NMDManagement
{
    class AccedeBD
    {
        public bool UpdatePassword(string pUsuario, string pClave)
        {
            string connectionString = "Data Source=.;Initial Catalog=CAI;User ID=Admin;Password=cajeros2008;";
            string query = @"update tcjusuarios set Clave='" + pClave + "' where CodUsuario='" + pUsuario + "'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    // open connection, execute query, close connection
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            // create connection and command in "using" blocks

        }
        public User ValidatePassword(string pUsuario, string pClave)
        {
            string connectionString = "Data Source=.;Initial Catalog=CAI;User ID=Admin;Password=cajeros2008;";
            string query = @"SELECT NombreCompleto,Cargo,Estado ,IdPerfil FROM tcjusuarios WHERE  Clave='" + pClave + "' AND CodUsuario='" + pUsuario + "'";
            User checkUser = new User();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    // open connection, execute query, close connection
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            checkUser.FullName = reader["NombreCompleto"].ToString();
                            checkUser.IdPerfil = Convert.ToInt32(reader["IdPerfil"]);
                            checkUser.Position = reader["Cargo"].ToString();
                            checkUser.State = reader["Estado"].ToString();
                        }
                    }
                    conn.Close();
                }
                return checkUser;
            }
            catch (Exception error)
            {
                checkUser.FullName = error.Message;
                return checkUser;
            }
        }
    }
    public class User
    {
        public string FullName { get; set; }
        public string State { get; set; }
        public int IdPerfil { get; set; }
        public string Position { get; set; }
    }
}
