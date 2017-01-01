using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    internal static class ChessBoard
    {
        private static readonly int widthTable;
        private static int cellWidth;
        private static Cell[,] cells;
        private static bool[,] possMoves;

        static ChessBoard()
        {
            widthTable = 600;  //NOTE: USe 600, because this way the cell size is 75 and this is the size of the Images used to draw the figures
            cellWidth = widthTable / 8;

            
            cells = new Cell[8, 8];
            possMoves = new bool[8, 8];
            setAllCellsAsFree();
        }

        internal static void setAllCellsAsFree()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    cells[x, y] = new Cell(true); //empty cell
                }
            }
        }
        
        internal static void resetAllPoss()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    possMoves[x, y] = false;
                }
            }
        }

        internal static void repartFigures(ColorsOfFigures firstPlayerColor)
        {
            if (firstPlayerColor == ColorsOfFigures.black) {
                for (int x = 0; x < 8; x++)
                {
                    cells[x, 0].MyFigure = FiguresBox.getWhiteFigureAtIndex(x);
                    cells[x, 1].MyFigure = FiguresBox.getWhiteFigureAtIndex(x + 8);
                    cells[x, 6].MyFigure = FiguresBox.getBlackFiguresAtIndex(x + 8);
                    cells[x, 7].MyFigure = FiguresBox.getBlackFiguresAtIndex(x);
                }
            } else {
                for (int x = 0; x < 8; x++)
                {
                    cells[x, 0].MyFigure = FiguresBox.getBlackFiguresAtIndex(x);
                    cells[x, 1].MyFigure = FiguresBox.getBlackFiguresAtIndex(x + 8);
                    cells[x, 6].MyFigure = FiguresBox.getWhiteFigureAtIndex(x + 8);
                    cells[x, 7].MyFigure = FiguresBox.getWhiteFigureAtIndex(x);
                }
            }
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    cells[x, y].MyFigure.PositionX = x;
                    cells[x, y].MyFigure.PositionY = y;
                }
                for (int y = 6; y < 8; y++)
                {
                    cells[x, y].MyFigure.PositionX = x;
                    cells[x, y].MyFigure.PositionY = y;
                }
            }
        }

        internal static Cell[,] Cells
        {
            get { return cells; }
        }

        internal static bool[,] PossMoves
        {
            get { return possMoves; }
        }

        internal static int BoardWidth
        {
            get { return widthTable; }
        }

        internal static int CellWidth
        {
            get { return cellWidth; }
        }
    }


}
