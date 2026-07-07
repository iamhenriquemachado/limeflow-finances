using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        string PasswordHasher(string password);
    }
}
