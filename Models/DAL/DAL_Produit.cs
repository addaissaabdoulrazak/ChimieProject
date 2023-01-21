using ChimieProject.Models.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ChimieProject.Models.DAL
{
    public class DAL_Produit
    {

        public static void CreateTable()
        {
            try
            {
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'Publication') CREATE TABLE [dbo].[EchangeLot] (\r\n    [Id]              BIGINT        IDENTITY (1, 1) NOT NULL,\r\n    [IdLabo]          BIGINT        NOT NULL,\r\n    [IdProduit]       BIGINT        NOT NULL,\r\n    [Type]            NVARCHAR (32) NOT NULL,\r\n    [DatePublication] DATETIME      NOT NULL,\r\n    [Purete]          NVARCHAR (32) NOT NULL,\r\n    [Concentration]   NVARCHAR (32) NOT NULL,\r\n    [DatePeremption]  DATETIME      NOT NULL,\r\n    [Quantite]        REAL          NOT NULL,\r\n    [UniteQuantite]   NVARCHAR (32) NOT NULL,\r\n    PRIMARY KEY CLUSTERED ([Id] ASC),\r\n    CONSTRAINT [FK_Produit] FOREIGN KEY ([IdProduit]) REFERENCES [dbo].[Produit] ([Id]),\r\n    CONSTRAINT [FK_Laboratoire] FOREIGN KEY ([IdLabo]) REFERENCES [dbo].[Structure] ([Id])\r\n); ";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();

            }
            catch { }
        }

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
            CreateTable();
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
                 Produit product = Get(id);
                 DAL_Publication.Delete(product.Id);
                 
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

        public static List<Produit> recherche(string Nom,string Formule,string CAS,string EtatPhysique)
        {
            var dataTable = new DataTable();
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
              //  Migration.CreationTableProduit();
                sqlConnection.Open();
                string query = "SELECT * FROM [Produit] WHERE (Nom=@Nom or Formule=@Formule or CAS=@CAS or EtatPhysique=@EtatPhysique)";
                var sqlCommand = new SqlCommand(query, sqlConnection);
                if (CAS == null)
                {
                    sqlCommand.Parameters.AddWithValue("CAS",  DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("CAS", CAS);

                }

                if (EtatPhysique == null)
                {
                    sqlCommand.Parameters.AddWithValue("EtatPhysique", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("EtatPhysique", EtatPhysique);

                }
                if (Nom == null)
                {
                    sqlCommand.Parameters.AddWithValue("Nom", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("Nom", Nom);

                }
                if (Formule == null)
                {
                    sqlCommand.Parameters.AddWithValue("Formule", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("Formule", Formule);

                }
                       
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

        #endregion

        #region Custom Methods



        #endregion

    }
}
