using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Developing_System_Prototype
{
    /// <summary>
    /// Обработка экспертных оценок методом непосредственного оценивания
    /// </summary>
    public class DirectEstimationMethod
    {
        /// <summary>
        /// матрица оценок экспертов
        /// </summary>
        private double[,,] assessment;

        /// <summary>
        /// Количество экспертов, объектов, показатедей
        /// </summary>
        private int m, n, l;

        /// <summary>
        /// Коэффициенты компетентности эксперта
        /// </summary>
        private double[] k;

        /// <summary>
        /// Коэффициенты весов показателей сравнения
        /// </summary>
        private double[,] q;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Input">Матрица оценок</param>
        /// <param name="m">Количество экспертов</param>
        /// <param name="n">Количество объектов</param>
        /// <param name="l">Количество показателей</param>
        public DirectEstimationMethod (double[,,] Input, int n, int m, int l)
        {
            this.n = n; this.m = m; this.l = l;
            q = new double[n, l];
            k = new double[n];
            assessment = new double[n, m, l];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    for (int h = 0; h < l; h++)
                        assessment[i, j, h] = Input[i, j, h];
        }

        /// <summary>
        /// Поиск максимального элемента в массиве
        /// </summary>
        /// <param name="Array">Массив</param>
        /// <returns>Возвращает максимальный элемент массива</returns>
        private double max(double[] Array)
        {
            double Max = Array[0];
            for (int i=1; i<Array.Length;i++)
            {
                if (Array[i] > Max)
                {
                    Max = Array[i];
                }
            }

            return Max;
        }

        /// <summary>
        /// Поиск номера максимального элемента массива
        /// </summary>
        /// <param name="Array">Массив</param>
        /// <returns>Возвращает номер максимального элемента массива</returns>
        private int maxNum(double[] Array)
        {
            double Max = Array[0];
            int k = 0;
            for (int i = 1; i < Array.Length; i++)
            {
                if (Array[i] > Max)
                {
                    Max = Array[i];
                    k = i;
                }
            }

            return k;
        }

        /// <summary>
        /// Поиск модуля числа
        /// </summary>
        /// <param name="a"></param>
        /// <returns>Возвращает модуль числа</returns>
        private double abs (double a)
        {
            if (a < 0)
                return -a;
            else
                return a;
        }

        /// <summary>
        /// Вычисление коэффициентов компетенции
        /// </summary>
        /// <param name="input">Марица оценок</param>
        /// <returns>Возвращает массив коэффициентов компетенции экспертов</returns>
        private double[,] Competence(double[,,] input)
        {
            double[,] k = new double[m, l];
            double[] xt1 = new double[n];
            double[] xt2 = new double[n];
            double[] xt = new double[n];

            for (int h = 0; h < l; h++)
            {
                int t = 1;

                for (int i = 0; i < n; i++)
                {
                    xt[i] = 0;
                    xt1[i] = 0;
                    xt2[i] = 0;
                }

                //нормировочный коэффициент
                double lambda = 0;

                //Начальные значения коэффициентов компетентности
                for (int i = 0; i < m; i++)
                {
                    k[i,h] = 1.0 / m;
                }

                //Поиск коэффициентов компетентности
                do
                {
                    //Вычисление средних оценок объектов первого приближения
                    for (int i = 0; i < n; i++)
                    {
                        xt1[i] = 0;

                        for (int j = 0; j < m; j++)
                        {
                            xt1[i] += input[i, j, 0] * k[j,h];
                        }
                    }

                    //вычисление нормировочного коэффициента
                    lambda = 0;
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            lambda += input[i, j, h] * xt1[i];
                        }
                    }

                    //Вычисление значений коэффициентов компетентности первого приближения
                    for (int j = 0; j < m; j++)
                    {
                        k[j,h] = 0;

                        for (int i = 0; i < n; i++)
                        {
                            k[j,h] += input[i, j, h] * xt1[i];
                        }

                        k[j,h] = k[j,h] / lambda;
                    }

                    //Вычисление групповых оценок второго приближения
                    for (int i = 0; i < n; i++)
                    {
                        xt2[i] = 0;

                        for (int j = 0; j < m; j++)
                        {
                            xt2[i] += input[i, j, h] * k[j,h];
                        }
                    }

                    //Вычисление разности коэффициентов первого и второго порядка
                    for (int i = 0; i < n; i++)
                    {
                        xt[i] = xt1[i] - xt2[i];
                    }

                    t++;


                } while ((abs(max(xt)) > 10e-5) && (t < 100));
            }
            return k;
        }

        /// <summary>
        /// Вычисление групповой оценки 
        /// </summary>
        /// <param name="Input">Таблица оценок</param>
        /// <returns>Среднее взвешенное значение оценки объекта</returns>
        private double[] GroupAssessment()
        {
            double[] x = new double[n];
            double[,] k = this.Competence(assessment);

            //подсчет весов
            for(int i=0; i<n; i++)
            {
                /*for(int h=0; h<l; h++)
                {
                    for(int j=0; j<m; j++)
                    {
                        q[i, h] += assessment[i, j, h] * k[j, h];
                    }
                }*/

                ///////////////////////////////////
                // !!!ЭТИМ ОТЛИЧАЮТСЯ КУРСАЧИ!!! //
                ///////////////////////////////////
                q[i, 0] = 0.4; //СТОИМОСТЬ
                q[i, 1] = 0.2; //ПРОИЗВОДИТЕЛЬНОСТЬ
                q[i, 1] = 0.3; //НАДЕЖНОСТЬ
                q[i, 1] = 0.1; //КОМФОРТНОСТЬ
            }

            //Подсчет Групповой Оценки
            for(int i=0; i<n; i++)
            {
                for(int h=0; h<l; h++)
                {
                    for(int j=0; j<m; j++)
                    {
                        x[i] += q[i, h] * assessment[i, j, h] * k[j, h];
                    }
                }
            }

            return x;
        }

        /// <summary>
        /// Выявление лучшего метода на основании экспертной оценки
        /// </summary>
        /// <returns>Номер лучшего метода</returns>
        public int CalculateBestMethod()
        {
            double[] x = GroupAssessment();
            return maxNum(x);
        }

    }
}
