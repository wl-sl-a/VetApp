using System;
using VetApp.Core.Services;
using VetApp.Core;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace VetApp.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;
        public AdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public string GetBackup()
        {
            return unitOfWork.Admin.GetBackup();
        }
        public string GetRestore(string filepath)
        {
            return unitOfWork.Admin.GetRestore(filepath);
        }
        public DateTime GetExpirationSsl()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:44379");
            DateTime expirationDate = default;
            request.ServerCertificateValidationCallback += (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
            {
                expirationDate = DateTime.Parse(certificate.GetExpirationDateString());
                return true;
            };
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                return expirationDate;
            }
        }
    }
}
