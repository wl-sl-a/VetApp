using System;

namespace VetApp.Core.Services
{
    public interface IAdminService
    {
        public string GetBackup();
        public string GetRestore(string filepath);
        public DateTime GetExpirationSsl();
    }
}
