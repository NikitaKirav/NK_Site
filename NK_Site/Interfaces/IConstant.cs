using NK_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NK_Site.Interfaces
{
    public interface IConstant
    {
        IEnumerable<Constant> AllConstants { get; }
        Constant GetObjectConstant(int constantId);
        int? GetValueInt(string name);
        bool UpdateConstant(Constant constant);
        bool CreateConstant(Constant constant);
    }
}
