namespace chess_drag_test
{
    internal class Pawn : Figure
    {
        internal Pawn(ColorsOfFigures color) : base(TypeOfFigures.Pawn, color) { }

        public override void getPossibleCells(bool[,] outArr)
        {
            if (positionY < 7 && positionY > 0)
            {
                testIfNextCellIsEmptyAndSetRegard(positionX, positionY - 1, ChessBoard.PossMoves, figureColor);
                if (positionY == 6)
                {
                    if (ChessBoard.PossMoves[positionX, positionY - 1]) {
                        testIfNextCellIsEmptyAndSetRegard(positionX, positionY - 2, ChessBoard.PossMoves, figureColor);
                    }
                }
            }
        }
    }
}
