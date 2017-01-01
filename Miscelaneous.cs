namespace chess_drag_test
{
    public enum ColorsOfFigures
    {
        black,
        white
    }

    public enum PosOfMyFigures
    {
        top,
        down
    }

    public enum TypeOfFigures
    {
        Pawn,
        Bishop,
        Rook,
        Knight,
        Queen,
        King
    }
    
    public enum TypeOfEndGame
    {
        Math,
        StaleMate,
        DrawByAgreement,
        Threefold,
        FiveFold,
        FiftyMove,
        SeventyFiveMove,
        InsufficientMaterial,
        EndByTime
    }

    internal struct EndOfGameReport
    {
        internal bool gameEnded;
        internal TypeOfEndGame type;
        internal Player winner;     //return only the reference plz
        internal Player Loser;
    }
}
