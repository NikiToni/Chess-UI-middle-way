using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    interface ICell
    {
        Figure MyFigure { get; set; }

        bool Empty { get; set; }
    }
}
