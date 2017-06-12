using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class TightPlacingAlgorithm : PlacingAlgorithm
    {
        Random rand = new Random();

        public override TimeSpan PerformPlacing(Board board)
        {
            // индексы элементов которые ещё не размещены
            List<int> elemIndexes = new List<int>();
            for (int i = 0; i < board.ElemCount; i++)
            {
                elemIndexes.Add(i);
            }

            int sideCount = 0, iter = 1, time = 0;

            // цикл алгоритма
            while (elemIndexes.Count > 0)
            {
                // выбор элемента

                // поиск элемента с макс смежностью
                int maxAdjacency = 0;
                int maxAdjacencyElemIndex = 0;
                foreach (int elemIndex in elemIndexes)
                {
                    time++;
                    int adj = Adjacency(elemIndex, board);
                    if (adj > maxAdjacency)
                    {
                        maxAdjacency = adj;
                        maxAdjacencyElemIndex = elemIndex;
                    }
                }

                // выбор элемента и удаление его из списка
                // элементов которые ещё не размещены

                int currentElemIndex = maxAdjacencyElemIndex;
                elemIndexes.Remove(currentElemIndex);


                // сам алгоритм
                if (iter == 1)
                {
                    time++;
                    sideCount += 4;
                }

                else
                {
                    time += 2 * sideCount;
                    if (rand.NextDouble() > 0.5)
                        sideCount += 2;
                    if ((rand.NextDouble() > 0.5) && (sideCount > 4))
                        sideCount -= 2;
                    time++;
                }

                iter++;
            }



            board.quality = (Board.Quality)rand.Next(0, 3);

            return TimeSpan.FromMinutes(((int)(((new Random()).NextDouble()) * 3) + 4) * board.ElemCount * board.ElemCount);
        }

        /// <summary>
        /// поиск смежности элемента
        /// </summary>
        /// <param name="elemIndex">номер элемента</param>
        /// <param name="board">плата</param>
        /// <returns>Возвращает смежность элемента</returns>
        private int Adjacency(int elemIndex, Board board)
        {
            int result = 0;
            for (int i = 0; i < board.ElemCount; i++)
            {
                result += board.WeightedMatrix1[i, elemIndex];
            }
            return result;
        }
    }
}
