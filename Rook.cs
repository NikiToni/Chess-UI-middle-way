namespace chess_drag_test
{
    internal class Rook : FigureRokade
    {
        internal Rook(ColorsOfFigures color) : 
            base(TypeOfFigures.Rook, color, new int[] { 1, 0, -1, 0, 0, -1, 0, 1 }, 8) { }
    }
}