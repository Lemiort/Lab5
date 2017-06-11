using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class Logger : IObserver
    {
        string log = "";

        public void Update(string state)
        {
            log += state;
        }

        public void Clear()
        {
            log = "";
        }

        public string Log { get { return log; } }
    }
}
