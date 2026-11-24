using butakov_pr3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace butakov_pr3.Services
{
    public class DatabaseService
    {
        private static string ConnectionString
        {
            get
            {
                return ConfigurationManager
                    .ConnectionStrings["AuthDb"]
                    .ConnectionString;
            }
        }

        public static UserModel Authenticate(
            string login,
            string passwordHash)
        {
            string query = @"
                SELECT TOP (1)
                    u.Id_user AS IdUser,
                    u.first_name AS FirstName,
                    u.last_name AS LastName,
                    u.Login,
                    u.Role_id AS RoleId,
                    r.Name AS RoleName
                FROM dbo.users AS u
                INNER JOIN dbo.Roles AS r
                    ON u.Role_id = r.Role_id
                WHERE u.Login = @login
                    AND u.Password = @passwordHash;";

            using (SqlConnection connection =
                   new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.Add(
                        "@login", SqlDbType.NVarChar, 50).Value = login;

                    command.Parameters.Add(
                        "@passwordHash", SqlDbType.NVarChar, 64)
                        .Value = passwordHash;

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        UserModel user = new UserModel();

                        user.IdUser =
                            Convert.ToInt32(reader["IdUser"]);

                        user.FirstName =
                            Convert.ToString(reader["FirstName"]);

                        user.LastName =
                            Convert.ToString(reader["LastName"]);

                        user.Login =
                            Convert.ToString(reader["Login"]);

                        user.RoleId =
                            Convert.ToInt32(reader["RoleId"]);

                        user.RoleName =
                            Convert.ToString(reader["RoleName"]);

                        return user;
                    }
                }
            }
        }
    }
}