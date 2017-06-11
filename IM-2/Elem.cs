using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class Elem
    {
        /// <summary>
        /// Длина элемента
        /// </summary>
        private double heigh;

        /// <summary>
        /// координата
        /// </summary>
        Point coordinate;

        /// <summary>
        /// Ширина элемента
        /// </summary>
        private double width;

        /// <summary>
        /// Длина элемента
        /// </summary>
        public double Heigh
        {
            get
            {
                return heigh;
            }

            private set
            {
                heigh = value;
            }
        }

        /// <summary>
        /// Ширина элемента
        /// </summary>
        public double Width
        {
            get
            {
                return width;
            }

            private set
            {
                width = value;
            }
        }

        /// <summary>
        /// Площать элемента
        /// </summary>
        public double Sqare
        {
            get
            {
                return width * heigh;
            }
        }

        public Elem(double heigh, double width)
        {
            this.heigh = heigh;
            this.width = width;
            coordinate = new Point();
        }

        // TODO: положение эл-в не плате

    }
}
