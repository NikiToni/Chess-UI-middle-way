namespace chess_drag_test
{
    internal class King : FigureRokade
    {
        internal King(ColorsOfFigures color) : 
            base(TypeOfFigures.King, color, new int[] { 1, 0, -1, 0,  0, -1,  0, 1,
                                                         1, 1, -1, -1, 1, -1, -1, 1
                                                                                }, 1) { }
    }
}
