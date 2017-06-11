using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    interface IObserver
    {
        void Update(string state);
    }
}
