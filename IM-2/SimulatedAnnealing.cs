using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    struct PlacingStepResources
    {
        public Designer designer;
        public Computer workstation;
        public Computer server;
        public PlacingAlgorithm algorithm;
    }

    /// <summary>
    /// испытание несколько раз с конфигурацией
    /// </summary>
    /// <param name="">борда для тестов</param>
    /// <param name="">уонфигурация</param>
    /// <param name="count">количество повторений</param>
    /// <returns>среднее время</returns>
    delegate TimeSpan AverageValueDelegate(Board board, PlacingStepResources resources, int count);


    class SimulatedAnnealing
    {
        /// <summary>
        /// лучший вариант
        /// </summary>
        PlacingStepResources bestConfiguration = new PlacingStepResources();

        /// <summary>
        /// текущий вараинт
        /// </summary>
        PlacingStepResources currentConfiguration = new PlacingStepResources();

        

        /// <summary>
        /// текущая итерация
        /// </summary>
        int currentIteration = 0;

        /// <summary>
        /// текущая температура
        /// </summary>
        double temperature;

        /// <summary>
        /// начальная температура
        /// </summary>
        public double StartTemperature
        {
            get;
            set;
        }

        /// <summary>
        /// конечная температура
        /// </summary>
        public double EndTemperature
        {
            get;
            set;
        }

        /// <summary>
        /// изменение температуры
        /// </summary>
        public double DeltaTemperature
        {
            get;
            set;
        }


        /// <summary>
        /// считаем стоимость целевой функции
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        private double CalcFunctionWeight(Board board, PlacingStepResources res, AverageValueDelegate experimentResult)
        {
            double timePrice = 6.25;//6.25$ в час
            double moneyPart = res.server.Price;
            moneyPart += res.workstation.Price;
            TimeSpan time = experimentResult(board, res, 1);
            moneyPart += (time.TotalHours + 1) * timePrice;
            moneyPart = (int)moneyPart;

            return moneyPart;
        }



        
        public PlacingStepResources Optimize(Board board, AverageValueDelegate experimentResult, out double cost)
        {
            Random rand = new Random();
            currentConfiguration = new PlacingStepResources();
            currentConfiguration.algorithm = new MatrixPlacingAlgorithm();
            currentConfiguration.designer = new Designer();
            currentConfiguration.server = new Computer() { CpuFrequency = 3.5e6 };
            currentConfiguration.workstation = new Computer() { CpuFrequency = 2.5e6 };
            bestConfiguration = currentConfiguration;
            temperature = StartTemperature;
            currentIteration = 0;


            while (temperature > EndTemperature)
            {
                PlacingStepResources prevConfig;
                prevConfig = currentConfiguration;

                currentConfiguration = new PlacingStepResources();
                currentConfiguration.algorithm = prevConfig.algorithm;
                currentConfiguration.designer = prevConfig.designer;
                currentConfiguration.server = prevConfig.server;
                currentConfiguration.workstation = prevConfig.workstation;
                

                ///изменение одного параметра
                switch(rand.Next(0,4))
                {
                    case 0:
                        if (currentConfiguration.algorithm is MatrixPlacingAlgorithm)
                            currentConfiguration.algorithm = new TightPlacingAlgorithm();
                        else
                            currentConfiguration.algorithm = new MatrixPlacingAlgorithm();
                        break;
                    case 1:
                        //nothing
                        break;
                    case 2:
                        currentConfiguration.server.CpuFrequency = rand.Next(2, 9) * 0.5e6;
                        break;
                    case 3:
                        currentConfiguration.workstation.CpuFrequency = rand.Next(2, 9) * 0.5e6;
                        break;
                }

                //выбираем лучший
                if (CalcFunctionWeight(board, bestConfiguration, experimentResult) >= CalcFunctionWeight(board, currentConfiguration, experimentResult))
                {
                    bestConfiguration = currentConfiguration;
                }
                //или рандомно
                else
                {
                    double P = CalculateProbaility(CalcFunctionWeight(board,currentConfiguration, experimentResult) - CalcFunctionWeight(board, bestConfiguration, experimentResult), temperature);
                    if (rand.NextDouble() <= P)
                    {
                        bestConfiguration = currentConfiguration;
                    }
                }
                //запоминаем лучший результат в любом случае
                currentConfiguration = bestConfiguration;

                //Охлаждаем
                temperature -= DeltaTemperature;
                currentIteration++;
            }
            cost = CalcFunctionWeight(board, bestConfiguration, experimentResult);
            return bestConfiguration;
        }

        private double CalculateProbaility(double dE, double T)
        {
            return Math.Exp(-dE / T);
        }

    }
}
