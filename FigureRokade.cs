namespace chess_drag_test
{
    internal abstract class FigureRokade : MovingFigure
    {
        internal FigureRokade(TypeOfFigures type, ColorsOfFigures color, int[] dirArr, int range) : 
            base(type, color, dirArr, range) { }

        protected bool moved = false;

        protected override void optionalRokade(bool[,] arr)
        {
            if (moved)
            {
                //code for rokade
                if (figureType == TypeOfFigures.King)
                {

                }
                else
                {

                }
            }
            moved = true;
        }
    }
}
