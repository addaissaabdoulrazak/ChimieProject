using ChimieProject.Models.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ChimieProject.Models.DAL
{
    public class DAL_Produit
    {
       
        #region Default Methods
        public static Produit Get(long id)
            {
                var dataTable = new DataTable();
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                  
                sqlConnection.Open();
                    string query = "SELECT * FROM [Produit] WHERE [Id]=@Id";
                    var sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Id", id);

                    new SqlDataAdapter(sqlCommand).Fill(dataTable);

                }

                if (dataTable.Rows.Count > 0)
                {
                    return new Produit(dataTable.Rows[0]);
                }
                else
                {
                    return null;
                }
            }

            public static List<Produit> Get()
            {
                var dataTable = new DataTable();
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    Migration.CreationTableProduit();
                    sqlConnection.Open();
                    string query = "SELECT * FROM [Produit]";
                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    new SqlDataAdapter(sqlCommand).Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows.Cast<DataRow>().Select(x => new Produit(x)).ToList();
                }
                else
                {
                    return new List<Produit>();
                }
            }

            private static List<Produit> get(List<long> ids)
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

                        sqlCommand.CommandText = $"SELECT * FROM [Produit] WHERE [Id] IN ({queryIds})";
                        new SqlDataAdapter(sqlCommand).Fill(dataTable);
                    }

                    if (dataTable.Rows.Count > 0)
                    {
                        return dataTable.Rows.Cast<DataRow>().Select(x => new Produit(x)).ToList();
                    }
                    else
                    {
                        return new List<Produit>();
                    }
                }
                return new List<Produit>();
            }

            public static long Insert(Produit item)
            {
                long response = long.MinValue;
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                    var sqlTransaction = sqlConnection.BeginTransaction();

                    string query = "INSERT INTO [Produit] ([CAS],[EtatPhysique],[Formule],[Nom])  VALUES (@CAS,@EtatPhysique,@Formule,@Nom); ";
                    query += "SELECT SCOPE_IDENTITY();";

                    using (var sqlCommand = new SqlCommand(query, sqlConnection, sqlTransaction))
                    {

                        sqlCommand.Parameters.AddWithValue("CAS", item.CAS);
                        sqlCommand.Parameters.AddWithValue("EtatPhysique", item.EtatPhysique);
                        sqlCommand.Parameters.AddWithValue("Formule", item.Formule);
                        sqlCommand.Parameters.AddWithValue("Nom", item.Nom);

                        var result = sqlCommand.ExecuteScalar();
                        response = result == null ? long.MinValue : long.TryParse(result.ToString(), out var insertedId) ? insertedId : long.MinValue;
                    }
                    sqlTransaction.Commit();

                    return response;
                }
            }

            private static int insert(List<Produit> items)
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
                            query += " INSERT INTO [Produit] ([CAS],[EtatPhysique],[Formule],[Nom]) VALUES ( "

                                + "@CAS" + i + ","
                                + "@EtatPhysique" + i + ","
                                + "@Formule" + i + ","
                                + "@Nom" + i
                                + "); ";


                            sqlCommand.Parameters.AddWithValue("CAS" + i, item.CAS);
                            sqlCommand.Parameters.AddWithValue("EtatPhysique" + i, item.EtatPhysique);
                            sqlCommand.Parameters.AddWithValue("Formule" + i, item.Formule);
                            sqlCommand.Parameters.AddWithValue("Nom" + i, item.Nom);
                        }

                        sqlCommand.CommandText = query;

                        results = sqlCommand.ExecuteNonQuery();
                    }

                    return results;
                }

                return -1;
            }

            public static int Update(Produit item)
            {
                int results = -1;
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                    string query = "UPDATE [Produit] SET [CAS]=@CAS, [EtatPhysique]=@EtatPhysique, [Formule]=@Formule, [Nom]=@Nom WHERE [Id]=@Id";
                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("CAS", item.CAS);
                    sqlCommand.Parameters.AddWithValue("EtatPhysique", item.EtatPhysique);
                    sqlCommand.Parameters.AddWithValue("Formule", item.Formule);
                    sqlCommand.Parameters.AddWithValue("Nom", item.Nom);

                    results = sqlCommand.ExecuteNonQuery();
                }

                return results;
            }

            private static int update(List<Produit> items)
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
                            query += " UPDATE [Produit] SET "

                                + "[CAS]=@CAS" + i + ","
                                + "[EtatPhysique]=@EtatPhysique" + i + ","
                                + "[Formule]=@Formule" + i + ","
                                + "[Nom]=@Nom" + i + " WHERE [Id]=@Id" + i
                                + "; ";

                            sqlCommand.Parameters.AddWithValue("Id" + i, item.Id);
                            sqlCommand.Parameters.AddWithValue("CAS" + i, item.CAS);
                            sqlCommand.Parameters.AddWithValue("EtatPhysique" + i, item.EtatPhysique);
                            sqlCommand.Parameters.AddWithValue("Formule" + i, item.Formule);
                            sqlCommand.Parameters.AddWithValue("Nom" + i, item.Nom);
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
                    string query = "DELETE FROM [Produit] WHERE [Id]=@Id";
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
