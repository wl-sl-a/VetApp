using System;

namespace VetApp.Core.Repositories
{
    public interface IAdminRepository
    {
        public string GetBackup();
        public string GetRestore(string filepath);
    }
}
