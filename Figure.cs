namespace chess_drag_test
{
    internal class Figure : IFigure
    {
        protected TypeOfFigures figureType;
        protected ColorsOfFigures figureColor;
        protected bool onTable;  //alife?
        protected bool imBlocked;   //when the figure can not move in no position/cell
        protected int positionX = 0;
        protected int positionY = 0;
        
        public virtual void getPossibleCells(bool [,] outArr)
        {
            //this one is virtual, because it does the same as abstract but doesn't force me to make
            //this class abstract. I don't want to make it abstract becasue I want to be able to reference/address it
        }

        protected Figure(TypeOfFigures type, ColorsOfFigures color)
        {
            figureType = type;
            figureColor = color;
            onTable = true;
            imBlocked = false; //initialize this to avoid errors due the weird case of somebody reading this property unitialized O.o
        }

        protected static bool testIfNextCellIsEmptyAndSetRegard(int i, int j, bool[,] arr, ColorsOfFigures color) //note to not use this for pawn. pawh is different because it can't beat a black figure that is next on front of it
        {
            if (i >= 0 && i < 8 && j >= 0 && j < 8) {
                if (ChessBoard.Cells[i, j].Empty)
                {
                    arr[i, j] = true; //it is possible
                    return true; //don't interrupt the iterator inside the caller
                }
                else
                {
                    if (ChessBoard.Cells[i, j].MyFigure.Color != color)
                    {
                        arr[i, j] = true; // it is possible
                    }
                    return false; // cell was occupied by other color figure and caller needs to stop iteration in this path/direction regardles of color, because previous cell is always possible so it shouldn't give a problem
                }
            }
            return false; //position is not inside the board
        }

        public TypeOfFigures Type
        {
            get { return figureType; }
        }

        public ColorsOfFigures Color
        {
            get { return figureColor; }
        }

        public bool OnTable
        {
            get { return onTable; }
            set { onTable = value; }
        }

        public bool Blocked
        {
            get { return imBlocked; }
        }

        public int PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }

        public int PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }
    }
}
