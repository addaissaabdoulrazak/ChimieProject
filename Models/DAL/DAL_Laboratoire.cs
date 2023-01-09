using ChimieProject.Models.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ChimieProject.Models.DAL
{

    public class DAL_Laboratoire
    {
        #region Default Methods
        public static Laboratoire Get(long id)
        {
            var dataTable = new DataTable();
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "SELECT * FROM [Laboratoire] WHERE [Id]=@Id";
                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);

                new SqlDataAdapter(sqlCommand).Fill(dataTable);

            }

            if (dataTable.Rows.Count > 0)
            {
                return new Laboratoire(dataTable.Rows[0]);
            }
            else
            {
                return null;
            }
        }


        //Get Element by name
        public static Laboratoire GetElementByName(string nom)
        {
            var dataTable = new DataTable();
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "SELECT * FROM [Laboratoire] WHERE [Nom]=@nom";
                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("Nom", nom);

                new SqlDataAdapter(sqlCommand).Fill(dataTable);

            }

            if (dataTable.Rows.Count > 0)
            {
                return new Laboratoire(dataTable.Rows[0]);
            }
            else
            {
                return null;
            }
        }



        //Get Element by Email
        public static Laboratoire GetElementByEmail(string Email)
        {
            var dataTable = new DataTable();
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "SELECT * FROM [Laboratoire] WHERE [Email]=@Email";
                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("Email", Email);

                new SqlDataAdapter(sqlCommand).Fill(dataTable);

            }

            if (dataTable.Rows.Count > 0)
            {
                return new Laboratoire(dataTable.Rows[0]);
            }
            else
            {
                return null;
            }
        }




        public static List<Laboratoire> Get()
        {
            var dataTable = new DataTable();
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "SELECT * FROM [Laboratoire]";
                var sqlCommand = new SqlCommand(query, sqlConnection);

                new SqlDataAdapter(sqlCommand).Fill(dataTable);
            }

            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows.Cast<DataRow>().Select(x => new Laboratoire(x)).ToList();
            }
            else
            {
                return new List<Laboratoire>();
            }
        }

        private static List<Laboratoire> get(List<long> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                var dataTable = new DataTable();
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                    var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;

                    string queryIds = string.Empty;
                    for (int i = 0; i < ids.Count; i++)
                    {
                        queryIds += "@Id" + i + ",";
                        sqlCommand.Parameters.AddWithValue("Id" + i, ids[i]);
                    }
                    queryIds = queryIds.TrimEnd(',');

                    sqlCommand.CommandText = $"SELECT * FROM [Laboratoire] WHERE [Id] IN ({queryIds})";
                    new SqlDataAdapter(sqlCommand).Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows.Cast<DataRow>().Select(x => new Laboratoire(x)).ToList();
                }
                else
                {
                    return new List<Laboratoire>();
                }
            }
            return new List<Laboratoire>();
        }

        public static int Insert(Laboratoire item)
        {
            int response = int.MinValue;
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                var sqlTransaction = sqlConnection.BeginTransaction();

                //testez voir
                string query = "INSERT INTO [Laboratoire] ([Acronyme],[Email],[Etablissement],[Nom],[Password],[Responsable],[Tel],[Role],[Statut])  VALUES (@Acronyme,@Email,@Etablissement,@Nom,@Password,@Responsable,@Tel,@Role,@Statut); ";
                query += "SELECT SCOPE_IDENTITY();";

                using (var sqlCommand = new SqlCommand(query, sqlConnection, sqlTransaction))
                {

                    sqlCommand.Parameters.AddWithValue("Acronyme", item.Acronyme ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("Email", item.Email ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("Etablissement", item.Etablissement ?? (object)DBNull.Value );
                    sqlCommand.Parameters.AddWithValue("Nom", item.Nom ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("Password", item.Password ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("Responsable", item.Responsable ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("Tel", item.Tel ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("Role", item.Role);
                    sqlCommand.Parameters.AddWithValue("Statut", item.Statut);
                   

                    var result = sqlCommand.ExecuteScalar();
                    response = result == null ? int.MinValue : int.TryParse(result.ToString(), out var insertedId) ? insertedId : int.MinValue;
                }
                sqlTransaction.Commit();

                return response;
            }
        }
        private static int insert(List<Laboratoire> items)
        {
            if (items != null && items.Count > 0)
            {
                int results = -1;
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                    string query = "";
                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    int i = 0;
                    foreach (var item in items)
                    {
                        i++;
                        query += " INSERT INTO [Laboratoire] ([Acronyme],[Email],[Etablissement],[Nom],[Password],[Responsable],[Tel],[Role],[Statut]) VALUES ( "

                            + "@Acronyme" + i + ","
                            + "@Email" + i + ","
                            + "@Etablissement" + i + ","
                            + "@Nom" + i + ","
                            + "@Password" + i + ","
                            + "@Responsable" + i + ","
                            + "@Tel" + i + ","
                             + "@Role" + i + ","
                            + "@Statut" + i 
                           
                            + "); ";


                        sqlCommand.Parameters.AddWithValue("Acronyme" + i, item.Acronyme);
                        sqlCommand.Parameters.AddWithValue("Email" + i, item.Email);
                        sqlCommand.Parameters.AddWithValue("Etablissement" + i, item.Etablissement);
                        sqlCommand.Parameters.AddWithValue("Nom" + i, item.Nom);
                        sqlCommand.Parameters.AddWithValue("Password" + i, item.Password);
                        sqlCommand.Parameters.AddWithValue("Responsable" + i, item.Responsable);
                        sqlCommand.Parameters.AddWithValue("Tel" + i, item.Tel);
                        sqlCommand.Parameters.AddWithValue("Role", item.Role);
                        sqlCommand.Parameters.AddWithValue("Statut" + i, item.Statut);
                     
                    }

                    sqlCommand.CommandText = query;

                    results = sqlCommand.ExecuteNonQuery();
                }

                return results;
            }

            return -1;
        }

        public static int Update(Laboratoire item)
        {
            int results = -1;
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "UPDATE [Laboratoire] SET [Acronyme]=@Acronyme, [Email]=@Email, [Etablissement]=@Etablissement, [Nom]=@Nom, [Password]=@Password, [Responsable]=@Responsable, [Tel]=@Tel,[Role]=@Role,[Statut]=@Statut WHERE [Id]=@Id";
                var sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("Id", item.Id);
                sqlCommand.Parameters.AddWithValue("Acronyme", item.Acronyme);
                sqlCommand.Parameters.AddWithValue("Email", item.Email);
                sqlCommand.Parameters.AddWithValue("Etablissement", item.Etablissement);
                sqlCommand.Parameters.AddWithValue("Nom", item.Nom);
                sqlCommand.Parameters.AddWithValue("Password", item.Password);
                sqlCommand.Parameters.AddWithValue("Responsable", item.Responsable);
                sqlCommand.Parameters.AddWithValue("Tel", item.Tel);
                sqlCommand.Parameters.AddWithValue("Role", item.Role);
                sqlCommand.Parameters.AddWithValue("Statut", item.Statut);
              

                results = sqlCommand.ExecuteNonQuery();
            }

            return results;
        }

        private static int update(List<Laboratoire> items)
        {
            if (items != null && items.Count > 0)
            {
                int results = -1;
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                    string query = "";
                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    int i = 0;
                    foreach (var item in items)
                    {
                        i++;
                        query += " UPDATE [Laboratoire] SET "

                            + "[Acronyme]=@Acronyme" + i + ","
                            + "[Email]=@Email" + i + ","
                            + "[Etablissement]=@Etablissement" + i + ","
                            + "[Nom]=@Nom" + i + ","
                            + "[Password]=@Password" + i + ","
                            + "[Responsable]=@Responsable" + i + ","
                            + "[Role]=@Role" + i + ","
                            + "[Statut]=@Statut" + i + ","
                            + "[Tel ]=@Tel_" + i + " WHERE [Id]=@Id" + i
                            + "; ";

                        sqlCommand.Parameters.AddWithValue("Id" + i, item.Id);
                        sqlCommand.Parameters.AddWithValue("Acronyme" + i, item.Acronyme);
                        sqlCommand.Parameters.AddWithValue("Email" + i, item.Email);
                        sqlCommand.Parameters.AddWithValue("Etablissement" + i, item.Etablissement);
                        sqlCommand.Parameters.AddWithValue("Nom" + i, item.Nom);
                        sqlCommand.Parameters.AddWithValue("Password" + i, item.Password);
                        sqlCommand.Parameters.AddWithValue("Responsable" + i, item.Responsable);
                        sqlCommand.Parameters.AddWithValue("Tel" + i, item.Tel);
                        sqlCommand.Parameters.AddWithValue("Role", item.Role);
                        sqlCommand.Parameters.AddWithValue("Statut" + i, item.Statut);
                        sqlCommand.Parameters.AddWithValue("Tel" + i, item.Tel);
                    }

                    sqlCommand.CommandText = query;

                    results = sqlCommand.ExecuteNonQuery();
                }

                return results;
            }

            return -1;
        }

        public static int Delete(long id)
        {
            int results = -1;
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "DELETE FROM [Laboratoire] WHERE [Id]=@Id";
                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);

                results = sqlCommand.ExecuteNonQuery();
            }

            return results;
        }

        #endregion

        #region Custom Methods



        #endregion
    }
}
