using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetApp.Core.Services
{
    public interface IPasswordService
    {
        string GeneratePassword(int minLength, int maxLength);
    }
}
