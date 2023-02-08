using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTurnosOnline.Shared.Validators.Contracts
{
    public interface IPasswordValidator
    {
        public Task<bool> IsPasswordValid(string userId, string password);
    }
}
