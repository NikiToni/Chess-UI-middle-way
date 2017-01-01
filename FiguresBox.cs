namespace chess_drag_test
{
    internal static class FiguresBox
    {
        private static Figure[] figuresArray;
        private static Figure[] blackFiguresArray;
        private static Figure[] whiteFiguresArray;

        static FiguresBox()
        {
            figuresArray = new Figure[] {
                                new Rook(ColorsOfFigures.white),
                                new Knight(ColorsOfFigures.white),
                                new Bishop(ColorsOfFigures.white),
                                new Queen(ColorsOfFigures.white),
                                new King(ColorsOfFigures.white),
                                new Bishop(ColorsOfFigures.white),
                                new Knight(ColorsOfFigures.white),
                                new Rook(ColorsOfFigures.white),

                                new Pawn(ColorsOfFigures.white),
                                new Pawn(ColorsOfFigures.white),
                                new Pawn(ColorsOfFigures.white),
                                new Pawn(ColorsOfFigures.white),
                                new Pawn(ColorsOfFigures.white),
                                new Pawn(ColorsOfFigures.white),
                                new Pawn(ColorsOfFigures.white),
                                new Pawn(ColorsOfFigures.white),

                                new Rook(ColorsOfFigures.black),
                                new Knight(ColorsOfFigures.black),
                                new Bishop(ColorsOfFigures.black),
                                new Queen(ColorsOfFigures.black),
                                new King(ColorsOfFigures.black),
                                new Bishop(ColorsOfFigures.black),
                                new Knight(ColorsOfFigures.black),
                                new Rook(ColorsOfFigures.black),

                                new Pawn(ColorsOfFigures.black),
                                new Pawn(ColorsOfFigures.black),
                                new Pawn(ColorsOfFigures.black),
                                new Pawn(ColorsOfFigures.black),
                                new Pawn(ColorsOfFigures.black),
                                new Pawn(ColorsOfFigures.black),
                                new Pawn(ColorsOfFigures.black),
                                new Pawn(ColorsOfFigures.black)
                        };

            whiteFiguresArray = new Figure[16];
            blackFiguresArray = new Figure[16];

            for (int i = 0; i < 16; i++)
            {
                whiteFiguresArray[i] = figuresArray[i];
                blackFiguresArray[i] = figuresArray[i + 16];
            }
        }

        internal static Figure[] Figures
        {
            get { return figuresArray; }
        }

        internal static Figure getWhiteFigureAtIndex(int i)
        {
            return whiteFiguresArray[i];
        }

        internal static Figure getBlackFiguresAtIndex(int i)
        {
            return blackFiguresArray[i];
        }
    }
}
