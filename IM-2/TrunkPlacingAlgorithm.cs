﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_2
{
    class TrunkPlacingAlgorithm : PlacingAlgorithm
    {
        public override TimeSpan PerformPlacing(Board board)
        {
            Random rand = new Random();
            board.quality = (Board.Quality)rand.Next(0, 3);
            return TimeSpan.FromMinutes(((int)(((new Random()).Next(1, 100)) * 3) + 14) * board.ElemCount * board.ElemCount * 5 - (new Random()).Next(board.ElemCount * board.ElemCount));
        }
    }
}