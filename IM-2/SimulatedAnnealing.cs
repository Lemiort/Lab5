using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    struct PlacingAndTracingStepResources
    {
        public Designer designer;
        public Designer designer2;
        public Computer workstation;
        public Computer workstation2;
        public Computer server;
        public PlacingAlgorithm algorithm;
        public TracingAlgorythm algorithm2;
    }

    /// <summary>
    /// испытание несколько раз с конфигурацией
    /// </summary>
    /// <param name="">борда для тестов</param>
    /// <param name="">уонфигурация</param>
    /// <param name="count">количество повторений</param>
    /// <returns>среднее время</returns>
    delegate TimeSpan AverageValueDelegate(Board board, PlacingAndTracingStepResources resources, int count);


    class SimulatedAnnealing
    {
        /// <summary>
        /// лучший вариант
        /// </summary>
        PlacingAndTracingStepResources bestConfiguration = new PlacingAndTracingStepResources();

        /// <summary>
        /// текущий вараинт
        /// </summary>
        PlacingAndTracingStepResources currentConfiguration = new PlacingAndTracingStepResources();

        

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
        private double CalcFunctionWeight(Board board, PlacingAndTracingStepResources res, AverageValueDelegate experimentResult)
        {
            double timePrice = 6.25;//6.25$ в час
            double moneyPart = res.server.Price;
            moneyPart += res.workstation.Price;
            TimeSpan time = experimentResult(board, res, 1);
            moneyPart += (time.TotalHours + 1) * timePrice;
            moneyPart = (int)moneyPart;

            return moneyPart;
        }



        
        public PlacingAndTracingStepResources Optimize(Board board, AverageValueDelegate experimentResult, out double cost)
        {
            Random rand = new Random();
            currentConfiguration = new PlacingAndTracingStepResources();
            currentConfiguration.algorithm = new MatrixPlacingAlgorithm();
            currentConfiguration.algorithm2 = new WaveTracingAlgorythm();
            currentConfiguration.designer = new Designer();
            currentConfiguration.designer2 = new Designer();
            currentConfiguration.server = new Computer() { CpuFrequency = 3.5e6 };
            currentConfiguration.workstation = new Computer() { CpuFrequency = 2.5e6 };
            currentConfiguration.workstation2 = new Computer() { CpuFrequency = 2.5e6 };
            bestConfiguration = currentConfiguration;
            temperature = StartTemperature;
            currentIteration = 0;


            while (temperature > EndTemperature)
            {
                PlacingAndTracingStepResources prevConfig;
                prevConfig = currentConfiguration;

                currentConfiguration = new PlacingAndTracingStepResources();
                currentConfiguration.algorithm = prevConfig.algorithm;
                currentConfiguration.algorithm2 = prevConfig.algorithm2;
                currentConfiguration.designer = prevConfig.designer;
                currentConfiguration.designer2 = prevConfig.designer;
                currentConfiguration.server = prevConfig.server;
                currentConfiguration.workstation = prevConfig.workstation;
                currentConfiguration.workstation2 = prevConfig.workstation2;


                ///изменение одного параметра
                switch (rand.Next(0,5))
                {
                    case 0:
                        if (currentConfiguration.algorithm is MatrixPlacingAlgorithm)
                            currentConfiguration.algorithm = new TightPlacingAlgorithm();
                        else
                            currentConfiguration.algorithm = new MatrixPlacingAlgorithm();
                        break;
                    case 1:
                        if (currentConfiguration.algorithm2 is WaveTracingAlgorythm)
                            currentConfiguration.algorithm2 = new TrunkTracingAlgorythm();
                        else if (currentConfiguration.algorithm2 is TrunkTracingAlgorythm)
                            currentConfiguration.algorithm2 = new BeamTracingAlgorythm();
                        else
                            currentConfiguration.algorithm2 = new WaveTracingAlgorythm();
                        break;
                    case 2:
                        currentConfiguration.server.CpuFrequency = rand.Next(2, 9) * 0.5e6;
                        break;
                    case 3:
                        currentConfiguration.workstation.CpuFrequency = rand.Next(2, 9) * 0.5e6;
                        break;
                    case 4:
                        currentConfiguration.workstation2.CpuFrequency = rand.Next(2, 9) * 0.5e6;
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
