namespace chess_drag_test
{
    internal class Queen : MovingFigure
    {
        internal Queen(ColorsOfFigures color) : 
            base(TypeOfFigures.Queen, color, new int[] { 1, 0, -1, 0,  0, -1,  0, 1, //move horizontal vertical
                                                         1, 1, -1, -1, 1, -1, -1, 1  // move by diagonal
                                                                                }, 8) { }
    }
}
