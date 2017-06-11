using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class Board : AbsrtactTask
    {
        public enum Quality
        {
            Good,
            Bad,
            NotMeasured
        }
        public Quality quality = Quality.NotMeasured;

        /// <summary>
        /// высота
        /// </summary>
        private double heigh;

        /// <summary>
        /// ширина
        /// </summary>
        private double width;

        /// <summary>
        /// Площадь
        /// </summary>
        public double Area
        {
            get
            {
                return width * heigh;
            }
        }

        public TimeSpan DevelopTime { get; set; }

        /// <summary>
        /// Длина
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
        /// Ширина
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
        /// Количество элементов
        /// </summary>
        public int ElemCount
        {
            get
            {
                return elemCount;
            }
        }

        internal List<Elem> Elems
        {
            get
            {
                return elems;
            }
        }

        public int[,] WeightedMatrix1
        {
            get
            {
                return WeightedMatrix;
            }
        }

        /// <summary>
        /// Элементы на борде
        /// </summary>
        private List<Elem> elems;

        /// <summary>
        /// Количество элементов
        /// </summary>
        private int elemCount;

        /// <summary>
        /// Матрица весов
        /// </summary>
        private int[,] WeightedMatrix;

        /// <summary>
        /// все для рандома
        /// </summary>
        private const double LInkP = 0.5;
        private static Random random = new Random();

        public Board(double width, double heigh, int elemCount)
        {
            this.elemCount = elemCount;

            this.elems = new List<Elem>();
            // TODO: инициализировать элементы

            this.width = width;
            this.heigh = heigh;

            WeightedMatrix = new int[this.ElemCount, this.ElemCount];

            //Генерация матрицы весов
            for (int i = 0; i < this.elemCount - 1; i++)
            {
                for (int j = i + 1; j < this.elemCount; j++)
                {
                    if (Board.random.NextDouble() < LInkP)
                    {
                        WeightedMatrix[i, j] = Board.random.Next(1, 6);
                        WeightedMatrix[j, i] = WeightedMatrix[i, j];
                    }
                }
            }

        }
    }
}
