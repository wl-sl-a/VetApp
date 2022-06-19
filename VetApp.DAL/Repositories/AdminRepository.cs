using System;
using System.Collections.Generic;
using System.Text;
using VetApp.Core.Repositories;
using Microsoft.Data.SqlClient;

namespace VetApp.DAL.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public AdminRepository() { }
        SqlConnection con = new SqlConnection();
        SqlCommand sqlcmd = new SqlCommand();
        public string GetBackup()
        {
            con.ConnectionString = @"Data Source=Nastya;Initial Catalog=VetApp; User = qwer; Password = qwerty123;";
            string backupDIR = "D:\\BackUp";
            if (!System.IO.Directory.Exists(backupDIR))
            {
                System.IO.Directory.CreateDirectory(backupDIR);
            }
            try
            {
                con.Open();
                sqlcmd = new SqlCommand("backup database testBackup to disk='" + backupDIR + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
                sqlcmd.ExecuteNonQuery();
                con.Close();
                return ("YES");
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }
        public string GetRestore(string filepath)
        {
            con.ConnectionString = @"Data Source=Nastya;Initial Catalog=VetApp; User = qwer; Password = qwerty123;";
            string backupDIR = "D:\\BackUp";
            try
            {
                con.Open();
                try
                {
                    sqlcmd = new SqlCommand("drop database testBackup", con);
                    sqlcmd.ExecuteNonQuery();
                }
                catch
                { }
                sqlcmd = new SqlCommand("restore database testBackup from disk='" + backupDIR + "\\" + filepath + ".Bak'", con);
                sqlcmd.ExecuteNonQuery();
                con.Close();
                return ("YES");
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }
    }
}
