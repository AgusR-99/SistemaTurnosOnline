using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTurnosOnline.Shared.Validators.Contracts
{
    public interface ICarreraValidator
    {
        Task<bool> NameIsUnique(string name);
    }
}
