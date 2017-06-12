using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class Computer: AbsractResource
    {
        /// <summary>
        /// частота процессора в Герцах
        /// </summary>
        public double CpuFrequency { get; set; }


        /// <summary>
        /// цена в долларах
        /// </summary>
        public double Price
        {
            get
            {
                if (CpuFrequency > 3e6)
                    return ((CpuFrequency - 2e6)/1e6 * 225)+500;
                else if (CpuFrequency > 2.2e6)
                    return ((CpuFrequency - 1.2e6)/1e6 * 170)+450;
                else
                    return ((CpuFrequency - 0.5e6)/1e6 * 90)+300;
            }
        }
    }
}
