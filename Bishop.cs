namespace chess_drag_test
{
    internal class Bishop : MovingFigure
    {
        internal Bishop(ColorsOfFigures color) : 
            base(TypeOfFigures.Bishop, color, new int[] { 1, 1, -1, -1, 1, -1, -1, 1 }, 8) { }
    }
} 