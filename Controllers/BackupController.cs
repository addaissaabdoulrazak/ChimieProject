using Automate_Backup.Models;
using ChimieProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ChimieProject.Controllers
{
    public class BackupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ILogger<BackupController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public BackupController(ILogger<BackupController> logger, IWebHostEnvironment webhost)
        {
            _logger = logger;
            _webHostEnvironment = webhost;
        }
        [Obsolete]
        public async Task<IActionResult> GenerateBackupFile()
        {

            ///<summary>
            ///get All Connection Sring
            ///</summary>
            List<DatabaseInfo> databaseInfos = new List<DatabaseInfo>();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            databaseInfos.Add(new DatabaseInfo()
                            {
                                Database = dr[0].ToString(),
                                //insert the database attribute in your connection string after data source
                                ConnectionString = connectionString.Insert(connectionString.IndexOf(';') + 1, "Database = " + dr[0].ToString() + ";")
                            });
                        }
                    }
                }
            }

            try
            {
                await Task.Run(() =>
                {

                    string contentRootPath = _webHostEnvironment.ContentRootPath;

                    string backupFolderName = "folder";

                    var path = Path.Combine(contentRootPath, backupFolderName);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    foreach (var dbItem in databaseInfos)
                    {
                        SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbItem.ConnectionString);

                        //creation de fichier(nom du dossier backup +) avec l'extension .bak
                        var backupFileName = $"{sqlConnectionStringBuilder.InitialCatalog}-{DateTime.Now.ToString("yyyy-MM-dd")}.bak";
                        if (System.IO.File.Exists(backupFileName))
                            System.IO.File.Delete(backupFileName);
                        using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                        {

                            string backupQuery = $"BACKUP DATABASE {sqlConnectionStringBuilder.InitialCatalog} TO Disk='" + path + $@"\{sqlConnectionStringBuilder.InitialCatalog}-{DateTime.Now.ToString("yyyy-MM-dd")}" + ".bak" + "'";
                            using (SqlCommand command = new SqlCommand(backupQuery, connection))
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                            }

                        }
                    }
                    ///<summary>
                    /// upload all files inside folder
                    ///</summary>
                    string sourceDirectory = path;
                    try
                    {
                        var bakFiles = Directory.EnumerateFiles(sourceDirectory, "*.bak", SearchOption.AllDirectories);

                        foreach (string currentFile in bakFiles)
                        {

                            // GoogleDriveAPIHelper gDriveAH = new GoogleDriveAPIHelper(_webHostEnvironment);
                            GoogleDriveAPIHelper gDriveAH = new GoogleDriveAPIHelper(_webHostEnvironment);
                            gDriveAH.UplaodFileOnDrive(currentFile);

                        }
                        ViewBag.Success = "File Uploaded on Google Drive";
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }

                });


                return Json(true);

            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}
