using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    internal static class ChessLogic
    {
        internal static void setPossibleCells(int i, int j)
        {
            ChessBoard.resetAllPoss();
            ChessBoard.Cells[i, j].MyFigure.getPossibleCells(ChessBoard.PossMoves);
        }

        /*internal bool checkForEndOfGame()
        {

        }*/

        internal static void promoteTo(int x, int y, TypeOfFigures destType)
        {
            switch (destType)
            {
                case TypeOfFigures.Queen:
                    ChessBoard.Cells[x, y].MyFigure = new Queen(ChessBoard.Cells[x, y].MyFigure.Color);
                    break;
                case TypeOfFigures.Rook:
                    ChessBoard.Cells[x, y].MyFigure = new Rook(ChessBoard.Cells[x, y].MyFigure.Color);
                    break;
                case TypeOfFigures.Knight:
                    ChessBoard.Cells[x, y].MyFigure = new Knight(ChessBoard.Cells[x, y].MyFigure.Color);
                    break;
                case TypeOfFigures.Bishop:
                    ChessBoard.Cells[x, y].MyFigure = new Bishop(ChessBoard.Cells[x, y].MyFigure.Color);
                    break;
            }
            
            ChessBoard.Cells[x, y].MyFigure.PositionX = x;
            ChessBoard.Cells[x, y].MyFigure.PositionY = y;
            ChessBoard.resetAllPoss();
            ChessBoard.Cells[x, y].MyFigure.getPossibleCells(ChessBoard.PossMoves);
        }

        internal static void MoveFigureFromTo(int fromX, int fromY, int toX, int toY)
        {
            ChessBoard.Cells[fromX, fromY].Empty = true;
            ChessBoard.Cells[toX, toY].MyFigure = ChessBoard.Cells[fromX, fromY].MyFigure;
            ChessBoard.Cells[toX, toY].MyFigure.PositionX = toX;
            ChessBoard.Cells[toX, toY].MyFigure.PositionY = toY;

        }
    }
}
