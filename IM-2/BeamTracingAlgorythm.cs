using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class BeamTracingAlgorythm: TracingAlgorythm
    {
        public override TimeSpan PerformTracing(Board board)
        {
            Random rand = new Random();
            board.quality = (Board.Quality)rand.Next(0, 3);
            return TimeSpan.FromMilliseconds(((int)(((new Random()).NextDouble()) * 6) + 10) * board.ElemCount * board.ElemCount * 5 - (new Random()).Next(board.ElemCount * board.ElemCount));
        }
    }
}
