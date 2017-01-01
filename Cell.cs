using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    class Cell : ICell
    {
        private bool empty;
        private Figure figureOnMe; //tells if in that cell there is a figure

        public Cell(bool empty)
        {
            this.empty = empty;
        }

        public Figure MyFigure
        {
            get { return figureOnMe; } //make sure you dont access the figure of a empty cell
            set
            {
                empty = false;
                figureOnMe = value;
            }
        }

        public bool Empty
        {
            get { return empty; }
            set { empty = value; }
        }
    }
}
