using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    interface IProcedure
    {
        AbsrtactTask PerformProcedure(AbsrtactTask arg);
        void AddResource(AbsractResource res);
        void RemoveResource(AbsractResource res);
    }
}
