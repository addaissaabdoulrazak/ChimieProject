using ChimieProject.Models.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ChimieProject.Models.DAL
{
    public class DAL_Publication
    {
        public static void CreateTable()
        {
            try
            {
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'EchangeLot') CREATE TABLE [dbo].[EchangeLot] (\r\n    [Id]              BIGINT        IDENTITY (1, 1) NOT NULL,\r\n    [IdLabo]          BIGINT        NOT NULL,\r\n    [IdProduit]       BIGINT        NOT NULL,\r\n    [Type]            NVARCHAR (32) NOT NULL,\r\n    [DatePublication] DATETIME      NOT NULL,\r\n    [Purete]          NVARCHAR (32) NOT NULL,\r\n    [Concentration]   NVARCHAR (32) NOT NULL,\r\n    [DatePeremption]  DATETIME      NOT NULL,\r\n    [Quantite]        REAL          NOT NULL,\r\n    [UniteQuantite]   NVARCHAR (32) NOT NULL,\r\n    PRIMARY KEY CLUSTERED ([Id] ASC),\r\n    CONSTRAINT [FK_Produit] FOREIGN KEY ([IdProduit]) REFERENCES [dbo].[Produit] ([Id]),\r\n    CONSTRAINT [FK_Laboratoire] FOREIGN KEY ([IdLabo]) REFERENCES [dbo].[Structure] ([Id])\r\n); ";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();

            }
            catch { }
        }
        #region Default Methods
        public static Publication Get(long id)
            {
                var dataTable = new DataTable();
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM [EchangeLot] WHERE [Id]=@Id";
                    var sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Id", id);

                    new SqlDataAdapter(sqlCommand).Fill(dataTable);

                }

                if (dataTable.Rows.Count > 0)
                {
                    return new Publication(dataTable.Rows[0]);
                }
                else
                {
                    return null;
                }
            }

            public static List<Publication> Get()
            {
                var dataTable = new DataTable();
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                Migration.CreationTablePublication();
                    sqlConnection.Open();
                    string query = "SELECT * FROM [EchangeLot]";
                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    new SqlDataAdapter(sqlCommand).Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows.Cast<DataRow>().Select(x => new Publication(x)).ToList();
                }
                else
                {
                    return new List<Publication>();
                }
            }

            public static long Insert(Publication item)
        {
            CreateTable();
            long response = long.MinValue;
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                 
                var sqlTransaction = sqlConnection.BeginTransaction();

                    string query = "INSERT INTO [EchangeLot] ([Concentration],[DatePeremption],[DatePublication],[IdLabo],[IdProduit],[Type],[Purete],[Quantite],[UniteQuantite])  VALUES (@Concentration,@DatePeremption,@DatePublication,@IdLabo,@IdProduit,@Type,@Purete,@Quantite,@UniteQuantite); ";
                    query += "SELECT SCOPE_IDENTITY();";

                    using (var sqlCommand = new SqlCommand(query, sqlConnection, sqlTransaction))
                    {

                        sqlCommand.Parameters.AddWithValue("Concentration", item.Concentration);
                        sqlCommand.Parameters.AddWithValue("DatePeremption", item.DatePeremption);
                        sqlCommand.Parameters.AddWithValue("DatePublication", item.DatePublication);
                        sqlCommand.Parameters.AddWithValue("IdLabo", item.IdLabo);
                        sqlCommand.Parameters.AddWithValue("IdProduit", item.IdProduit);
                        sqlCommand.Parameters.AddWithValue("Type", item.Type);
                        sqlCommand.Parameters.AddWithValue("Purete", item.Purete);
                        sqlCommand.Parameters.AddWithValue("Quantite", item.Quantite);
                        sqlCommand.Parameters.AddWithValue("UniteQuantite", item.UniteQuantite);

                        var result = sqlCommand.ExecuteScalar();
                        response = result == null ? long.MinValue : long.TryParse(result.ToString(), out var insertedId) ? insertedId : long.MinValue;
                    }
                    sqlTransaction.Commit();

                    return response;
                }
            }

            private static int insert(List<Publication> items)
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
                            query += " INSERT INTO [EchangeLot] ([Concentration],[DatePeremption],[DatePublication],[IdLabo],[IdProduit],[Type],[Purete],[Quantite],[UniteQuantite]) VALUES ( "

                                + "@Concentration" + i + ","
                                + "@DatePeremption" + i + ","
                                + "@DatePublication" + i + ","
                                + "@IdLabo" + i + ","
                                + "@IdProduit" + i + ","
                                + "@Type" + i + ","
                                + "@Purete" + i + ","
                                + "@Quantite" + i + ","
                                + "@UniteQuantite" + i
                                + "); ";


                            sqlCommand.Parameters.AddWithValue("Concentration" + i, item.Concentration);
                            sqlCommand.Parameters.AddWithValue("DatePeremption" + i, item.DatePeremption);
                            sqlCommand.Parameters.AddWithValue("DatePublication" + i, item.DatePublication);
                            sqlCommand.Parameters.AddWithValue("IdLabo" + i, item.IdLabo);
                            sqlCommand.Parameters.AddWithValue("IdProduit" + i, item.IdProduit);
                            sqlCommand.Parameters.AddWithValue("Type" + i, item.Type);
                            sqlCommand.Parameters.AddWithValue("Purete" + i, item.Purete);
                            sqlCommand.Parameters.AddWithValue("Quantite" + i, item.Quantite);
                            sqlCommand.Parameters.AddWithValue("UniteQuantite" + i, item.UniteQuantite);
                        }

                        sqlCommand.CommandText = query;

                        results = sqlCommand.ExecuteNonQuery();
                    }

                    return results;
                }

                return -1;
            }

            public static int Update(Publication item)
            {
                int results = -1;
                using (SqlConnection sqlConnection = DBConnection.GetConnection())
                {
                    sqlConnection.Open();
                    string query = "UPDATE [EchangeLot] SET [Concentration]=@Concentration, [DatePeremption]=@DatePeremption, [DatePublication]=@DatePublication, [IdLabo]=@IdLabo, [IdProduit]=@IdProduit,[Type]=@Type, [Purete]=@Purete, [Quantite]=@Quantite, [UniteQuantite]=@UniteQuantite WHERE [Id]=@Id";
                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("Concentration", item.Concentration);
                    sqlCommand.Parameters.AddWithValue("DatePeremption", item.DatePeremption);
                    sqlCommand.Parameters.AddWithValue("DatePublication", item.DatePublication);
                    sqlCommand.Parameters.AddWithValue("IdLabo", item.IdLabo);
                    sqlCommand.Parameters.AddWithValue("IdProduit", item.IdProduit);
                    sqlCommand.Parameters.AddWithValue("Type", item.Type);
                    sqlCommand.Parameters.AddWithValue("Purete", item.Purete);
                    sqlCommand.Parameters.AddWithValue("Quantite", item.Quantite);
                    sqlCommand.Parameters.AddWithValue("UniteQuantite", item.UniteQuantite);

                    results = sqlCommand.ExecuteNonQuery();
                }

                return results;
            }

            private static int update(List<Publication> items)
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
                            query += " UPDATE [EchangeLot] SET "

                                + "[Concentration]=@Concentration" + i + ","
                                + "[DatePeremption]=@DatePeremption" + i + ","
                                + "[DatePublication]=@DatePublication" + i + ","
                                + "[IdLabo]=@IdLabo" + i + ","
                                + "[IdProduit]=@IdProduit" + i + ","
                                + "[Type]=@Type" + i + ","
                                + "[Purete]=@Purete" + i + ","
                                + "[Quantite]=@Quantite" + i + ","
                                + "[UniteQuantite]=@UniteQuantite" + i + " WHERE [Id]=@Id" + i
                                + "; ";

                            sqlCommand.Parameters.AddWithValue("Id" + i, item.Id);
                            sqlCommand.Parameters.AddWithValue("Concentration" + i, item.Concentration);
                            sqlCommand.Parameters.AddWithValue("DatePeremption" + i, item.DatePeremption);
                            sqlCommand.Parameters.AddWithValue("DatePublication" + i, item.DatePublication);
                            sqlCommand.Parameters.AddWithValue("IdLabo" + i, item.IdLabo);
                            sqlCommand.Parameters.AddWithValue("IdProduit" + i, item.IdProduit);
                            sqlCommand.Parameters.AddWithValue("Type" + i, item.Type);
                            sqlCommand.Parameters.AddWithValue("Purete" + i, item.Purete);
                            sqlCommand.Parameters.AddWithValue("Quantite" + i, item.Quantite);
                            sqlCommand.Parameters.AddWithValue("UniteQuantite" + i, item.UniteQuantite);
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
                    string query = "DELETE FROM [EchangeLot] WHERE [Id]=@Id";
                    var sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Id", id);

                    results = sqlCommand.ExecuteNonQuery();
                }

                return results;
            }

        public static int DeleteForeingKeyProduitId(long id)
        {
            int results = -1;
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "DELETE FROM [EchangeLot] WHERE [IdProduit]=@Id";
                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);

                results = sqlCommand.ExecuteNonQuery();
            }

            return results;
        }
        public static int DeleteForeignKeyStructureId(long id)
        {
            int results = -1;
            using (SqlConnection sqlConnection = DBConnection.GetConnection())
            {
                sqlConnection.Open();
                string query = "DELETE FROM [EchangeLot] WHERE [IdLabo]=@Id";
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
