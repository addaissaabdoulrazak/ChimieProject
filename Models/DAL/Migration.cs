using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using ChimieProject.Models.Entities;
using System.Web.Helpers;

namespace ChimieProject.Models.DAL
{
    public class Migration
    {
        public static Message CreationTableProduit()
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                try
                {

                    connection.Open();
                    string requette = "If not exists (select * from sysobjects where name = 'Produit')" +
                        " CREATE TABLE [dbo].[Produit] " +
                        "([Id]  BIGINT  IDENTITY(1, 1) NOT NULL," +
                        "[Nom] VARCHAR(256) NOT NULL," +
                        "[Formule] VARCHAR(32) NOT NULL," +
                        "[CAS]  VARCHAR(32) NOT NULL," +
                        "[EtatPhysique] VARCHAR(32) NOT NULL," +
                        "PRIMARY KEY CLUSTERED([Id] ASC));" +
                        "ALTER TABLE [dbo].[Produit] ADD CONSTRAINT nom_unique UNIQUE (Nom);";
                    SqlCommand command = new SqlCommand(requette, connection);
                    command.ExecuteNonQuery();

                    return new Message(true, "Table crée avec succé ");
                }
                catch (Exception ex)
                {
                    return new Message(false, ex.Message.ToString());
                }
                finally { connection.Close(); }
            }
        }

        public static Message CreationTablePublication()
        {

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                try
                {

                    connection.Open();
                    string requette = "If not exists (select * from sysobjects where name = 'Publication')" +
                        " CREATE TABLE [dbo].[Publication] " +
                        "([Id]  BIGINT  IDENTITY(1, 1) NOT NULL," +
                        " [IdLabo]  BIGINT NOT NULL," +
                        "[IdProduit] BIGINT  NOT NULL," +
                        "[Type] NVARCHAR(32) NOT NULL," +
                        "[DatePublication] DATETIME NOT NULL," +
                        "[Purete] NVARCHAR(32) NOT NULL," +
                        "[Concentration] NVARCHAR(32) NOT NULL," +
                        "[DatePeremption] DATETIME NOT NULL," +
                        "[Quantite] REAL NOT NULL," +
                        "[UniteQuantite] NVARCHAR(32) NOT NULL," +
                        "PRIMARY KEY CLUSTERED([Id] ASC));" +
                        "ALTER TABLE [dbo].[Publication] add CONSTRAINT  FK_Produit FOREIGN KEY  (IdProduit)  REFERENCES [dbo].[Produit]([Id]);"+
                    "ALTER TABLE [dbo].[Publication] add CONSTRAINT  FK_Laboratoire FOREIGN KEY  (IdLabo)  REFERENCES [dbo].[Publication]([Id]);";
                    SqlCommand command = new SqlCommand(requette, connection);
                    command.ExecuteNonQuery();

                    return new Message(true, "Table crée avec succé ");
                }
                catch (Exception ex)
                {
                    return new Message(false, ex.Message.ToString());
                }
                finally { connection.Close(); }
            }
        
   
        }

        public static Message CreationTableLaboratoire()
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                try
                {

                    connection.Open();
                    string requette = "If not exists (select * from sysobjects where name = 'Publication')" +
                   " CREATE TABLE[dbo].[Structure]"+
                   "([Id]            BIGINT IDENTITY(1, 1) NOT NULL,"+
                   "[Nom]           NVARCHAR(256) NOT NULL,"+
                   "[Acronyme]      NVARCHAR(32)  NOT NULL,"+
                   "[Etablissement] NVARCHAR(256) NOT NULL,"+
                   "[Responsable]   NVARCHAR(64)  NOT NULL,"+
                   "[Type]          NVARCHAR(64)  NOT NULL,"+
                   "[Email]         NVARCHAR(32)  NOT NULL,"+
                   "[Tel]           NVARCHAR(32)  NOT NULL,"+
                   "[Password]      NVARCHAR(200) NOT NULL,"+
                   "[Statut]        INT NOT NULL,"+
                   "[Role] VARCHAR(50)   NOT NULL,"+
                    "PRIMARY KEY CLUSTERED([Id] ASC),"+
                   " CONSTRAINT[AK_Email_Laboratoire] UNIQUE NONCLUSTERED([Email] ASC));";
                    SqlCommand command = new SqlCommand(requette, connection);
                    command.ExecuteNonQuery();

                    return new Message(true, "Table crée avec succé ");
                }
                catch (Exception ex) { return new Message(false, ex.Message.ToString()); }
                finally { connection.Close(); }
            }
        }
    }
}
