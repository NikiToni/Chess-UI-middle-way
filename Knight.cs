namespace chess_drag_test
{
    internal class Knight : MovingFigure
    {
        internal Knight(ColorsOfFigures color) : 
            base(TypeOfFigures.Knight, color, new int[] { 2, 1,  2, -1,
                                                         -2, 1, -2, -1,
                                                          1, 2,  1, -2,
                                                         -1, 2, -1, -2 }, 1) { }
    }
}
