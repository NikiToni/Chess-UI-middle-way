using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace chess_drag_test
{
    public partial class Form1 : Form
    {
        private const int boardWidth = 600; //the width of the board
        private const int cellWidth = boardWidth / 8;

        // properties for the select oponent screen
        private static Color selOpBckGroundColor = Color.Teal;
        Random randomColorGen = new Random();

        // select users btn properties
        private const int selectUsrBtnBottomMargin = 40;
        private const int selectUsrBtnButtonWidth = 220;
        private const int selectUsrBtnButtonHeigth = 60;

        private const int figureChoiseMargin = 20;
        private const int figureChoiseBtnHeight = 40;
        private const int figureChoiseBtnWidth = 127;
        private const int figureChoiseMenuTop = 50;
        private const int figureChoiseMenuLeft = (boardWidth - figureChoiseMenuWidth) / 2;
        private const int figureChoiseMenuWidth = cellWidth * 4 + figureChoiseMargin * 5;
        private const int figureChoiceMenuHeight = cellWidth + figureChoiseMargin * 3 + selectUsrBtnButtonHeigth - 10;
        private const int figureChoiseBtnTop = figureChoiseMenuTop + figureChoiseMargin * 2 + cellWidth;
        private const int figureChoiseBtnLeft = (boardWidth - figureChoiseBtnWidth) / 2;

        // properties for the scroll box of the users
        private const int userBoxWidth = 440;
        private const int userBoxHeight = 400;
        private const int userBoxTop = 60;
        private const int userBoxLeft = (boardWidth - userBoxWidth) / 2;
        private const int userBoxScrollBarWidth = 20;
        private const int userBoxScrollBarLeft = userBoxLeft + (userBoxWidth - userBoxScrollBarWidth);
        private double scrollDelta = 0;
        private const int userItemVertMargin = 20;
        private const int userItemHorizMargin = 20;
        private const int userItemHeight = 120;
        private int userItemWidth;
        private bool scrollNeeded;
        private const int optInsideUsrItemMargin = 7;

        private Pen userBoxBorderPen = new Pen(Brushes.DeepSkyBlue, 8);
        private Pen photoBorder = new Pen(Brushes.DeepSkyBlue, 2);
        private Pen InvLineColor = new Pen(Color.FromArgb(255, 20, 80, 80));
        private Pen userMarkedPen = new Pen(Color.Yellow , 4);           // linked together
        private Pen userNotMarkedPen = new Pen(selOpBckGroundColor, 4);  // ////// ////////
        private Pen nearesrSquareBorder = new Pen(Brushes.Black, 4);
        private Pen figureChoiceBorder = new Pen(Color.FromArgb(255, 20, 80, 80), 4);
        private SolidBrush outernOrnamentColor = new SolidBrush(Color.DeepSkyBlue);

        // select users scroll button properties
        private double scrollBtnHeight;
        private const int scrollBtnMargin = 4;
        private readonly int scrollBtnWidth;
        private const int scrollBtnLeft = userBoxScrollBarLeft + scrollBtnMargin;
        private double scrollBtnTop;

        // user items properties
        private const int photoMargin = 10;
        private const int photoWidth = userItemHeight - photoMargin * 2;
        private const int userItemLeft = userItemHorizMargin + userBoxLeft;
        private const int photoLeft = userItemLeft + photoMargin;
        private const int ornament2Width = 2;

        private const int iconMargin = 4;
        private int iconLeft;
        private int iconTop;
        private const int iconWidth = 30;
        private int tempLeft;
        private int tempRight;

        private Point userItemOrnamentPoint1 = new Point(userItemLeft,                            0);
        private Point userItemOrnamentPoint2 = new Point(photoLeft + photoWidth + photoMargin,    0);
        private Point userItemOrnamentPoint3 = new Point(photoLeft + photoWidth,                  0);
        private Point userItemOrnamentPoint4 = new Point(photoLeft,                               0);
        private Point userItemOrnamentPoint5 = new Point(photoLeft,                               0);
        private Point userItemOrnamentPoint6 = new Point(userItemLeft,                            0);

        private Point userItemOrnament2Point1 = new Point(photoLeft + photoWidth - ornament2Width,  0);
        private Point userItemOrnament2Point2 = new Point(photoLeft + photoWidth + ornament2Width,  0);
        private Point userItemOrnament2Point3 = new Point(photoLeft + photoWidth + ornament2Width,  0);
        private Point userItemOrnament2Point4 = new Point(photoLeft - ornament2Width,               0);
        private Point userItemOrnament2Point5 = new Point(photoLeft + ornament2Width,               0);
        private Point userItemOrnament2Point6 = new Point(photoLeft + photoWidth - ornament2Width,  0);
        
        private Point userItemOrnament3Point1;
        private Point userItemOrnament3Point2;
        private Point userItemOrnament3Point3;
        private Point userItemOrnament3Point4;
        private Point userItemOrnament3Point5;
        private Point userItemOrnament3Point6;

        private Point userItemOrnament4Point1;
        private Point userItemOrnament4Point2;
        private Point userItemOrnament4Point3;
        private Point userItemOrnament4Point4;
        private Point userItemOrnament4Point5;
        private Point userItemOrnament4Point6;

        private Point figureChoise1Point1;
        private Point figureChoise1Point2;
        private Point figureChoise1Point3;
        private Point figureChoise1Point4;
        private Point figureChoise1Point5;
        private Point figureChoise1Point6;

        private bool[] defaultInvOptions = new bool[] { false, false, true, false, false, false, false, false};
        
        private ColorsOfFigures myColor;
        
        private SolidBrush userScrollColor = new SolidBrush(Color.LightSkyBlue);
        private SolidBrush userScrollClickedColor = new SolidBrush(Color.FromArgb(255, 0, 150, 150));
        private SolidBrush userItemOrnamentColor = new SolidBrush(Color.FromArgb(255, 0, 60, 60));
        private SolidBrush userBoxBckGroundColor = new SolidBrush(Color.AntiqueWhite);
        private SolidBrush userItemBckgroundColor = new SolidBrush(Color.FromArgb(255, 53, 227, 217));
        private SolidBrush userScrollBckgroundColor = new SolidBrush(Color.Azure);
        private SolidBrush userBoxFakeMaskColor = new SolidBrush(selOpBckGroundColor);
        private SolidBrush figureHoverColor = new SolidBrush(Color.FromArgb(255, 55, 159, 177));

        private visualButton sendInvitationBtn = new visualButton("selctOpntBtn", imagesPathPrefix, (boardWidth - selectUsrBtnButtonWidth) / 2 + 1, boardWidth - (selectUsrBtnButtonHeigth + selectUsrBtnBottomMargin), selectUsrBtnButtonWidth, selectUsrBtnButtonHeigth);
        private visualButton figureChoiceBtn = new visualButton("promoteBtn", imagesPathPrefix, figureChoiseBtnLeft, figureChoiseBtnTop, figureChoiseBtnWidth , figureChoiseBtnHeight);

        private bool mouseDowned = false;
        private int mouseX;
        private int mouseY;
        private int wholeUserAreaHeight;
        private double calculableScrollArea;
        private readonly double maxScrollTop;
        private double minScrollTop;
        private double maxDelta;
        private const int invHeight = 50;
        private const int invLeft = photoLeft + photoWidth + ornament2Width + invMarginLeft;
        private const int invMarginLeft = 20;
        private const int arrowsHeight = 40;
        private const int leftArrowWidth = 35;
        private const int cancelAcceptWidth = 40;
        private const int rightArrowWidth = 36;
        private const int invMargin = (userItemHeight - (arrowsHeight * 2)) / 3;
        private int cancelAcceptLeft;
        private const int optionsIconWidth = 24;
        private const int optionsIconsLeft = (boardWidth - optionsIconWidth * 8) / 2;
        private readonly int optionsIconsTop;
        private double centerOfCancelX;
                private const double centerOfCancelY = (cancelAcceptWidth / 2) + invMargin;
                private const int cancelAcceptRadius = 18;
        private bool[] tempInvOpt;

        private const int invLeftStartX = invLeft + 18;
        private const int invRightStartX = invLeft + rightArrowWidth - 2;
        private const int invLineWidth = 6;
        private readonly int maxUserClickableArea;
        private readonly int minUserClickableArea;

        private int topDrawingOffset;
        private int clickableAreasIndex;

        private static string imagesPathPrefix = "../../chess figures/";
        private Image leftArrowImg = Image.FromFile(imagesPathPrefix + "leftArrow.png");
        private Image rightArrowImg = Image.FromFile(imagesPathPrefix + "rigthArrow.png");
        private Image invCancelImg = Image.FromFile(imagesPathPrefix + "invCancel.png");
        private Image invAcceptImg = Image.FromFile(imagesPathPrefix + "invAccept.png");

        private int[,] clickableAreas = new int[4, 2];   //in no moment more than 4 users will be simultaneously visible in the scroll user area
        private User[] clicableUsers = new User[4];
        private int selectedFigureInd = 1;

        private Image[,] invOptionsImgs = new Image[8,4] {
            { Image.FromFile(imagesPathPrefix + "invWhiteKing.png"), Image.FromFile(imagesPathPrefix + "invWhiteKingLight.png"), null, Image.FromFile(imagesPathPrefix + "invWhiteKingGreen.png")},
            { Image.FromFile(imagesPathPrefix + "invBlackKing.png"), Image.FromFile(imagesPathPrefix + "invBlackKingLight.png"), null, Image.FromFile(imagesPathPrefix + "invBlackKingGreen.png")},
            { Image.FromFile(imagesPathPrefix + "invAnyKing.png"), Image.FromFile(imagesPathPrefix + "invAnyKingLight.png"), null, Image.FromFile(imagesPathPrefix + "invAnyKingGreen.png")},
            { Image.FromFile(imagesPathPrefix + "15min.png"), Image.FromFile(imagesPathPrefix + "15minLight.png"), Image.FromFile(imagesPathPrefix + "15minOrange.png"), Image.FromFile(imagesPathPrefix + "15minGreen.png")},
            { Image.FromFile(imagesPathPrefix + "30min.png"), Image.FromFile(imagesPathPrefix + "30minLight.png"), Image.FromFile(imagesPathPrefix + "15minOrange.png"), Image.FromFile(imagesPathPrefix + "15minGreen.png")},
            { Image.FromFile(imagesPathPrefix + "45min.png"), Image.FromFile(imagesPathPrefix + "45minLight.png"), Image.FromFile(imagesPathPrefix + "15minOrange.png"), Image.FromFile(imagesPathPrefix + "15minGreen.png")},
            { Image.FromFile(imagesPathPrefix + "60min.png"), Image.FromFile(imagesPathPrefix + "60minLight.png"), Image.FromFile(imagesPathPrefix + "15minOrange.png"), Image.FromFile(imagesPathPrefix + "15minGreen.png")},
            { Image.FromFile(imagesPathPrefix + "noClock.png"), Image.FromFile(imagesPathPrefix + "noClockLight.png"), Image.FromFile(imagesPathPrefix + "15minOrange.png"), Image.FromFile(imagesPathPrefix + "15minGreen.png")}
        }; 

        private SolidBrush rectangleFillDark = new SolidBrush(Color.BlueViolet);
        private SolidBrush rectangleFillLight = new SolidBrush(Color.LightCyan);
        private SolidBrush rectangleFillPossLight = new SolidBrush(Color.FromArgb(255, 119, 239, 108));
        private SolidBrush rectangleFillPossDark = new SolidBrush(Color.FromArgb(255, 42, 196, 64));

        //Note the orden of this array should correspond to the indexes in the enums Types and Colors
        private visualFigure[,] possibleFigures;
        private Image[] socialNets;
        

        private int figureOnFocusIndexX = -1;
        private int figureOnFocusIndexY = -1;

        private int squareOnFocusIndexX;
        private int squareOnFocusIndexY;

        private int[] squareOnFocusIndex = new int[2];

        public Form1()
        {
            possibleFigures = new visualFigure[,] {
                              { new visualFigure((TypeOfFigures) 0, (ColorsOfFigures) 0, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 1, (ColorsOfFigures) 0, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 2, (ColorsOfFigures) 0, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 3, (ColorsOfFigures) 0, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 4, (ColorsOfFigures) 0, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 5, (ColorsOfFigures) 0, imagesPathPrefix) },

                              { new visualFigure((TypeOfFigures) 0, (ColorsOfFigures) 1, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 1, (ColorsOfFigures) 1, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 2, (ColorsOfFigures) 1, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 3, (ColorsOfFigures) 1, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 4, (ColorsOfFigures) 1, imagesPathPrefix),
                                new visualFigure((TypeOfFigures) 5, (ColorsOfFigures) 1, imagesPathPrefix) }
            };

            socialNets = new Image[] { Image.FromFile(imagesPathPrefix + "facebook2.png"),
                                       Image.FromFile(imagesPathPrefix + "SkypeAvailable.png"),
                                       Image.FromFile(imagesPathPrefix + "SkypeAway.png") };

            
            //selectFigureBtn = new

            scrollBtnWidth = userBoxScrollBarWidth - scrollBtnMargin * 2 - (int)userBoxBorderPen.Width / 2;
            optionsIconsTop = sendInvitationBtn.top - optionsIconWidth;

            scrollBtnTop = userBoxTop + scrollBtnMargin + userBoxBorderPen.Width / 2;
            maxScrollTop = scrollBtnTop;
            
            InvLineColor.Width = invLineWidth;
            
            maxUserClickableArea = userBoxTop + userBoxHeight - (int)userBoxBorderPen.Width / 2;
            minUserClickableArea = userBoxTop + (int)userBoxBorderPen.Width / 2;
            
            userItemOrnament3Point1 = new Point(userItemLeft - (int)userMarkedPen.Width, 0);
            userItemOrnament3Point2 = new Point(photoLeft + photoWidth + photoMargin + (int)userMarkedPen.Width, 0);
            userItemOrnament3Point3 = new Point(photoLeft + photoWidth + photoMargin, 0);
            userItemOrnament3Point4 = new Point(userItemLeft, 0);
            userItemOrnament3Point5 = new Point(userItemLeft, 0);
            userItemOrnament3Point6 = new Point(userItemLeft - (int)userMarkedPen.Width, 0);
            
            figureChoise1Point1 = new Point(0, figureChoiseMenuTop + figureChoiseMargin - (int)userNotMarkedPen.Width / 2);
            figureChoise1Point2 = new Point(0, figureChoiseMenuTop + figureChoiseMargin - (int)userNotMarkedPen.Width / 2);
            figureChoise1Point3 = new Point(0, figureChoiseMenuTop + figureChoiseMargin + (int)userNotMarkedPen.Width / 2);
            figureChoise1Point4 = new Point(0, figureChoiseMenuTop + figureChoiseMargin + (int)userNotMarkedPen.Width / 2);
            figureChoise1Point5 = new Point(0, figureChoiseMenuTop + figureChoiseMargin + cellWidth - (int)userNotMarkedPen.Width / 2);
            figureChoise1Point6 = new Point(0, figureChoiseMenuTop + figureChoiseMargin + cellWidth + (int)userNotMarkedPen.Width / 2);
            
            InitializeComponent(boardWidth, 12);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //calculate the hight of the scroll button

            wholeUserAreaHeight = UsersHandler.UsersNumber * (userItemHeight + userItemVertMargin) + userItemVertMargin;
            maxDelta = wholeUserAreaHeight - (userBoxHeight - userBoxBorderPen.Width);

            if (wholeUserAreaHeight > userBoxHeight) { //not much friends online
                calculableScrollArea = userBoxHeight - scrollBtnMargin * 2 - (int)userBoxBorderPen.Width;
                scrollBtnHeight = calculableScrollArea / ((double)wholeUserAreaHeight / ((double)userBoxHeight));
                userItemWidth = (userBoxWidth - (userItemHorizMargin * 2)) - userBoxScrollBarWidth;
                minScrollTop = userBoxHeight + userBoxTop - scrollBtnMargin - userBoxBorderPen.Width / 2 - scrollBtnHeight;
                scrollNeeded = true;
            } else {
                userItemWidth = (userBoxWidth - (userItemHorizMargin * 2));
                scrollNeeded = false;
            }

            iconLeft = photoLeft + photoWidth - iconMargin - iconWidth;
            iconTop = photoMargin + photoWidth - iconMargin - iconWidth;
            cancelAcceptLeft = userItemLeft + userItemWidth - invMargin - cancelAcceptWidth;
            centerOfCancelX = cancelAcceptLeft + cancelAcceptWidth / 2;

            userItemOrnament4Point1 = new Point(userItemLeft + userItemWidth, 0);
            userItemOrnament4Point2 = new Point(userItemLeft + userItemWidth + (int)userMarkedPen.Width, 0);
            userItemOrnament4Point3 = new Point(userItemLeft + userItemWidth + (int)userMarkedPen.Width, 0);
            userItemOrnament4Point4 = new Point(userItemLeft + userItemWidth - photoWidth - iconMargin * 2, 0);
            userItemOrnament4Point5 = new Point(userItemLeft + userItemWidth - photoWidth - iconMargin * 2 + (int)userMarkedPen.Width, 0);
            userItemOrnament4Point6 = new Point(userItemLeft + userItemWidth, 0);

            canvas.Invalidate();
        }

        //================================================================================================
        // methods for select oponent from list menu
        //================================================================================================

        private void canvas_Paint_SendInv(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(selOpBckGroundColor);

            if (!scrollNeeded)
            {
                //draw the user box background
                e.Graphics.FillRectangle(userBoxBckGroundColor, userBoxLeft, userBoxTop, userBoxWidth, userBoxHeight);
            }
            else
            {
                //draw the user box background
                e.Graphics.FillRectangle(userBoxBckGroundColor, userBoxLeft, userBoxTop, userBoxWidth, userBoxHeight);

                //draw the scroll bar
                e.Graphics.FillRectangle(userScrollBckgroundColor, userBoxScrollBarLeft, userBoxTop, userBoxScrollBarWidth, userBoxHeight);

                //draw the scroll bar button 
                e.Graphics.FillRectangle(mouseDowned ? userScrollClickedColor : userScrollColor, scrollBtnLeft, (int)scrollBtnTop, scrollBtnWidth, (int)scrollBtnHeight);
            }

            //draw the user items
            topDrawingOffset = userItemVertMargin - (int)scrollDelta + (int)userBoxBorderPen.Width / 2;
            clickableAreasIndex = 0;
            for (int i = 0; i < 4; i ++)
            {
                clickableAreas[i, 1] = -100;    //only this will invalidate all click tests
            }

            for (int i = 0; i < UsersHandler.UsersNumber; i++)
            {
                if ((topDrawingOffset < userBoxHeight && topDrawingOffset + userItemHeight + userBoxTop > userBoxTop))
                {
                    drawUserItem(topDrawingOffset + userBoxTop, UsersHandler.getUserAtIndex(i), e);
                    clickableAreasIndex++;
                }
                topDrawingOffset = topDrawingOffset + userItemHeight + userItemVertMargin;
            }
            // draw the masks that ocult the hiden part of the user box
            e.Graphics.FillRectangle(userBoxFakeMaskColor, userBoxLeft, 0, userBoxWidth, userBoxTop);
            e.Graphics.FillRectangle(userBoxFakeMaskColor, userBoxLeft, userBoxTop + userBoxHeight, userBoxWidth, boardWidth - (userBoxTop + userBoxHeight));

            //draw the border of the user box
            e.Graphics.DrawRectangle(userBoxBorderPen, userBoxLeft, userBoxTop, userBoxWidth, userBoxHeight);
            
            //draw the button
            e.Graphics.DrawImage(sendInvitationBtn.clicked ? sendInvitationBtn.imgShadow : sendInvitationBtn.image,
                                                         sendInvitationBtn.left, sendInvitationBtn.top,
                                                         sendInvitationBtn.width, sendInvitationBtn.height);

            //draw the default options
            for (int i = 0; i < 8; i++)
            {
                e.Graphics.DrawImage(defaultInvOptions[i] ? invOptionsImgs[i, 1] : invOptionsImgs[i, 0], optionsIconsLeft + optionsIconWidth * i, optionsIconsTop, optionsIconWidth, optionsIconWidth);
            }
        }

        void canvas_MouseDown_SendInv(object sender, MouseEventArgs e)
        {
            if (testButtonCollision(sendInvitationBtn, e.X, e.Y))
            {
                sendInvitationBtn.clicked = true;
                for (int i = 0; i < UsersHandler.UsersNumber; i++)
                {
                    if (UsersHandler.getUserAtIndex(i).markedInForm && sendInvitationBtn.clicked)
                    {
                        UsersHandler.getUserAtIndex(i).invitationSent = true;
                        for (int j = 0; j < 8; j++)
                        {
                            UsersHandler.getUserAtIndex(i).invitationISentToUserOptions[j] = defaultInvOptions[j];
                        }
                        UsersHandler.getUserAtIndex(i).markedInForm = false;
                    }
                }
            }
            else if (e.X > scrollBtnLeft && e.X < scrollBtnLeft + scrollBtnWidth && e.Y > scrollBtnTop && e.Y < scrollBtnTop + scrollBtnHeight)
            {
                {
                    mouseDowned = true;
                    mouseY = e.Y - (int)scrollBtnTop;
                }
            }
            else if (e.Y > minUserClickableArea && e.Y < maxUserClickableArea && e.X > userItemLeft && e.X < userItemLeft + userItemWidth)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (e.Y > clickableAreas[i, 0] && e.Y < clickableAreas[i, 1])
                    {
                        double baseTop = (centerOfCancelY + clickableAreas[i, 0]);
                        if (clicableUsers[i].invitationSent) {
                            if (Math.Sqrt((e.X - centerOfCancelX) * (e.X - centerOfCancelX) +
                                          (e.Y - baseTop) * (e.Y - baseTop)) < cancelAcceptRadius)
                            {
                                clicableUsers[i].invitationSent = false;
                            }
                            baseTop += cancelAcceptWidth + invMargin;
                        }
                        if (clicableUsers[i].invitationReceived)
                        {
                            if (Math.Sqrt((e.X - centerOfCancelX) * (e.X - centerOfCancelX) +
                                          (e.Y - baseTop) * (e.Y - baseTop)) < cancelAcceptRadius)
                            {
                                if (clicableUsers[i].invitationISentToUserOptions[2])
                                {
                                    if (clicableUsers[i].invitationUserSentToMeOptions[2])
                                    {
                                        myColor = (ColorsOfFigures)randomColorGen.Next(0,2);
                                        //send to oponent the other color in order his table to know what figures to play
                                    } else
                                    { //user selected color
                                        if (clicableUsers[i].invitationUserSentToMeOptions[0])
                                        {
                                            myColor = ColorsOfFigures.black;
                                        } else
                                        {
                                            myColor = ColorsOfFigures.white;
                                        }
                                    }
                                } else
                                { // I chosed the color of the figures
                                    if (clicableUsers[i].invitationISentToUserOptions[0])
                                    {
                                        myColor = ColorsOfFigures.white;
                                    }
                                    else
                                    {
                                        myColor = ColorsOfFigures.black;
                                    }
                                }

                                ChessBoard.repartFigures(myColor);

                                // connect to user
                                // reset states 
                                clicableUsers[i].invitationReceived = false;
                                clicableUsers[i].invitationSent = false;

                                //load the chess table regard the invitation options negotiated by users
                                canvas.Paint -= canvas_Paint_SendInv;
                                canvas.Paint += canvas_Paint_ChessTbl_BckGrndFigs;
                                canvas.Paint += canvas_Paint_ChessTable;
                                canvas.MouseDown -= canvas_MouseDown_SendInv;
                                canvas.MouseDown += canvas_MouseDown_ChessTable;
                                canvas.MouseUp -= canvas_MouseUp_SendInv;
                                canvas.MouseUp += canvas_MouseUp_ChessTable;
                                canvas.MouseMove -= canvas_MouseMove_SendInv;
                                canvas.MouseMove += canvas_MouseMove_ChessTable;
                                canvas.MouseWheel -= canvas_MouseWheel_SendInv;
                            }
                        }
                        clicableUsers[i].markedInForm = !clicableUsers[i].markedInForm;
                        break;
                    }
                }
            }
            else if (e.Y > optionsIconsTop && e.Y < optionsIconsTop + optionsIconWidth)
            {
                // click detect on kings
                if (e.X > optionsIconsLeft && e.X < optionsIconsLeft + optionsIconWidth)
                {
                    defaultInvOptions[0] = !defaultInvOptions[0];
                    defaultInvOptions[1] = false;
                    defaultInvOptions[2] = false;
                }
                else if (e.X > optionsIconsLeft + optionsIconWidth && e.X < optionsIconsLeft + optionsIconWidth * 2)
                {
                    defaultInvOptions[0] = false;
                    defaultInvOptions[1] = !defaultInvOptions[1];
                    defaultInvOptions[2] = false;
                }
                else if (e.X > optionsIconsLeft + optionsIconWidth * 2 && e.X < optionsIconsLeft + optionsIconWidth * 3)
                {
                    if (!defaultInvOptions[2])
                    {
                        defaultInvOptions[0] = false;
                        defaultInvOptions[1] = false;
                        defaultInvOptions[2] = true;
                    }
                }
                else if (e.X > optionsIconsLeft + optionsIconWidth * 3 && e.X < optionsIconsLeft + optionsIconWidth * 7)
                {
                    for (int i = 3; i < 7; i++)
                    {
                        if (e.X > optionsIconsLeft + optionsIconWidth * i && e.X < optionsIconsLeft + optionsIconWidth * (i + 1))
                        {
                            defaultInvOptions[i] = !defaultInvOptions[i];
                        }
                    }
                    defaultInvOptions[7] = false;
                }
                else if (e.X > optionsIconsLeft + optionsIconWidth * 7 && e.X < optionsIconsLeft + optionsIconWidth * 8)
                {
                    defaultInvOptions[3] = false;
                    defaultInvOptions[4] = false;
                    defaultInvOptions[5] = false;
                    defaultInvOptions[6] = false;
                    defaultInvOptions[7] = !defaultInvOptions[7];
                }

                if (!defaultInvOptions[0] && !defaultInvOptions[1])
                {
                    defaultInvOptions[2] = true;
                }
            }
            canvas.Invalidate();
        }

        void canvas_MouseUp_SendInv(object sender, MouseEventArgs e)
        {
            sendInvitationBtn.clicked = false;
            mouseDowned = false;
            canvas.Invalidate();
        }

        void canvas_MouseMove_SendInv(object sender, MouseEventArgs e)
        {
            if (mouseDowned)
            {
                if (e.Y - mouseY <= maxScrollTop)
                {
                    scrollBtnTop = maxScrollTop;
                    scrollDelta = 0;
                }
                else if (e.Y - mouseY >= minScrollTop)
                {
                    scrollBtnTop = minScrollTop;
                    scrollDelta = wholeUserAreaHeight - (userBoxHeight - userBoxBorderPen.Width);
                } else {
                    scrollBtnTop = e.Y - mouseY;
                    scrollDelta = (double)wholeUserAreaHeight / (calculableScrollArea / (scrollBtnTop - maxScrollTop));
                }
            }
            canvas.Invalidate();
        }

        void canvas_MouseWheel_SendInv(object sender, MouseEventArgs e)
        {
            if (scrollNeeded) {
                if (e.X > userBoxLeft && e.X < userBoxLeft + userBoxWidth && e.Y > userBoxTop && e.Y < userBoxTop + userBoxHeight)
                {
                    scrollDelta -= e.Delta / 2;

                    if (scrollDelta < 0)
                    {
                        scrollDelta = 0;
                    } else if (scrollDelta > maxDelta) {   //i think delta cant be zero here?
                        scrollDelta = maxDelta;
                    }
                    updateScrollBtnPosition();
                }
            }
            canvas.Invalidate();
        }

        //==================================================================

        private void updateScrollBtnPosition()
        {
            scrollBtnTop = (calculableScrollArea / (wholeUserAreaHeight / scrollDelta)) + maxScrollTop;
            if (scrollBtnTop < maxScrollTop)
            {
                scrollBtnTop = maxScrollTop;
            } else if (scrollBtnTop > minScrollTop) {
                scrollBtnTop = minScrollTop;
            }
        }
        
        private bool testButtonCollision(visualButton button, int mouseX, int mouseY)
        {
            if (mouseX > button.left && mouseX < button.width + button.left && mouseY > button.top && mouseY < button.top + button.height) {

                //handle transparency of the image for the figure
                int relativeX = mouseX - button.left;
                int relativeY = mouseY - button.top;

                if (button.bitmap.GetPixel(relativeX, relativeY).A >= 8)
                {
                    return true;
                }
            }
            return false;
        }

        private void drawUserItem(int top, User user, PaintEventArgs e)
        {
            clickableAreas[clickableAreasIndex, 0] = top;
            clickableAreas[clickableAreasIndex, 1] = top + userItemHeight;
            clicableUsers[clickableAreasIndex] = user;

            e.Graphics.FillRectangle(userItemBckgroundColor, userItemLeft, top, userItemWidth, userItemHeight);
            e.Graphics.DrawRectangle(user.markedInForm ? userMarkedPen : userNotMarkedPen,
                                                        userItemLeft - userMarkedPen.Width / 2, top - userMarkedPen.Width / 2,
                                                        userItemWidth + userMarkedPen.Width, userItemHeight + userMarkedPen.Width);

            //draw ornaments on user items boxes
            userItemOrnamentPoint1.Y = top;
            userItemOrnamentPoint2.Y = top;
            userItemOrnamentPoint3.Y = top + photoMargin;
            userItemOrnamentPoint4.Y = top + photoMargin;
            userItemOrnamentPoint5.Y = top + photoWidth + photoMargin;
            userItemOrnamentPoint6.Y = top + userItemHeight;

            e.Graphics.FillPolygon(userItemOrnamentColor, new Point[]{ userItemOrnamentPoint1,
                                                                       userItemOrnamentPoint2,
                                                                       userItemOrnamentPoint3,
                                                                       userItemOrnamentPoint4,
                                                                       userItemOrnamentPoint5,
                                                                       userItemOrnamentPoint6});
            
            e.Graphics.DrawImage(user.photo, photoLeft, top + photoMargin, photoWidth, photoWidth);

            e.Graphics.DrawRectangle(photoBorder, photoLeft, top + photoMargin, photoWidth, photoWidth);
            
            userItemOrnament2Point1.Y = top + photoMargin + ornament2Width;
            userItemOrnament2Point2.Y = top + photoMargin - ornament2Width;
            userItemOrnament2Point3.Y = top + photoMargin + photoWidth + ornament2Width;
            userItemOrnament2Point4.Y = top + photoMargin + photoWidth + ornament2Width;
            userItemOrnament2Point5.Y = top + photoMargin + photoWidth - ornament2Width;
            userItemOrnament2Point6.Y = top + photoMargin + photoWidth - ornament2Width;

            e.Graphics.FillPolygon(userItemOrnamentColor, new Point[]{ userItemOrnament2Point1,
                                                                       userItemOrnament2Point2,
                                                                       userItemOrnament2Point3,
                                                                       userItemOrnament2Point4,
                                                                       userItemOrnament2Point5,
                                                                       userItemOrnament2Point6});

            userItemOrnament3Point1.Y = top - (int)userMarkedPen.Width;
            userItemOrnament3Point2.Y = top - (int)userMarkedPen.Width;
            userItemOrnament3Point3.Y = top;
            userItemOrnament3Point4.Y = top;
            userItemOrnament3Point5.Y = top + userItemHeight;
            userItemOrnament3Point6.Y = top + userItemHeight + (int)userMarkedPen.Width;

            e.Graphics.FillPolygon(outernOrnamentColor, new Point[]{ userItemOrnament3Point1,
                                                                       userItemOrnament3Point2,
                                                                       userItemOrnament3Point3,
                                                                       userItemOrnament3Point4,
                                                                       userItemOrnament3Point5,
                                                                       userItemOrnament3Point6});
            
            userItemOrnament4Point1.Y = top;
            userItemOrnament4Point2.Y = top - (int)userMarkedPen.Width;
            userItemOrnament4Point3.Y = top + userItemHeight + (int)userMarkedPen.Width;
            userItemOrnament4Point4.Y = top + userItemHeight + (int)userMarkedPen.Width;
            userItemOrnament4Point5.Y = top + userItemHeight;
            userItemOrnament4Point6.Y = top + userItemHeight;

            e.Graphics.FillPolygon(userBoxFakeMaskColor, new Point[]{ userItemOrnament4Point1,
                                                                       userItemOrnament4Point2,
                                                                       userItemOrnament4Point3,
                                                                       userItemOrnament4Point4,
                                                                       userItemOrnament4Point5,
                                                                       userItemOrnament4Point6});
            
            e.Graphics.DrawImage(socialNets[(int)user.type], iconLeft, top + iconTop, iconWidth, iconWidth);

            int topInv = top + invMargin;
            if (user.invitationSent)
            {
                drawItemOptions(topInv, 0, invCancelImg, e, user);
                topInv += arrowsHeight + invMargin;
            }
            if (user.invitationReceived)
            {
                drawItemOptions(topInv, 1, invAcceptImg, e, user);
            }
            e.Graphics.DrawImage(socialNets[(int)user.type], iconLeft, top + iconTop, iconWidth, iconWidth);
        }

        private void drawItemOptions(int top, int type, Image actionImg, PaintEventArgs e, User user)
        {
            e.Graphics.DrawImage((type == 0 ? leftArrowImg : rightArrowImg), invLeft, top, (type == 0 ? leftArrowWidth : rightArrowWidth), arrowsHeight);
            e.Graphics.DrawLine(InvLineColor, (type == 0 ? invRightStartX : invLeftStartX), top + invLineWidth / 2, cancelAcceptLeft + 10, top + invLineWidth / 2);
            e.Graphics.DrawImage(actionImg, cancelAcceptLeft, top, cancelAcceptWidth, cancelAcceptWidth);
            e.Graphics.DrawLine(InvLineColor, (type == 0 ? invRightStartX : invLeftStartX), top + cancelAcceptWidth - invLineWidth / 2, cancelAcceptLeft + 10, top + cancelAcceptWidth - invLineWidth / 2);

            if (type == 0) {
                tempInvOpt = user.invitationISentToUserOptions;
            }  else
            {
                tempInvOpt = user.invitationUserSentToMeOptions;
            }

            for (int i = 0; i < 3; i++)
            {
                if (tempInvOpt[i])
                {
                    e.Graphics.DrawImage(invOptionsImgs[i, 3], invLeft + leftArrowWidth + optInsideUsrItemMargin, top + 8, 24, 24);
                    break;
                }
            }

            for (int i = 3; i < 8; i++)
            {
                if (tempInvOpt[i])
                    e.Graphics.DrawImage(invOptionsImgs[i, 3], invLeft + leftArrowWidth + optInsideUsrItemMargin + optionsIconWidth * (i - 2), top + 8, 24, 24);
                else
                    e.Graphics.DrawImage(invOptionsImgs[i, 2], invLeft + leftArrowWidth + optInsideUsrItemMargin + optionsIconWidth * (i - 2), top + 8, 24, 24);
            }
        }

        //==================================================================
        //==================================================================
        //==================================================================
        //
        //   the actual chess playing code goes here
        //
        //==================================================================
        //==================================================================
        //==================================================================

        private bool lightSquare = false;
        private double tempDist;
        private double nearDistance;
        private int localTopLeftX;
        private int localTopLeftY;
        private int globalTopLeftX;
        private int globalTopLeftY;

        private void canvas_Paint_ChessTbl_BckGrndFigs(object sender, PaintEventArgs e)
        {
            if (myColor == ColorsOfFigures.white)
            {
                lightSquare = true;
            }
            else
            {
                lightSquare = false;
            }

            //draw the backgrond
            e.Graphics.Clear(Color.Teal); //clears the background 
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (lightSquare)
                    {
                        if (ChessBoard.PossMoves[i, j] && mouseDowned)
                        {
                            e.Graphics.FillRectangle(rectangleFillPossLight, i * cellWidth, j * cellWidth, cellWidth, cellWidth);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(rectangleFillLight, i * cellWidth, j * cellWidth, cellWidth, cellWidth);
                        }
                    }
                    else
                    {
                        if (ChessBoard.PossMoves[i, j] && mouseDowned)
                        {
                            e.Graphics.FillRectangle(rectangleFillPossDark, i * cellWidth, j * cellWidth, cellWidth, cellWidth);
                        }
                    }
                    lightSquare = !lightSquare;
                }
                lightSquare = !lightSquare;
            }
            //draw the figures

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!ChessBoard.Cells[i, j].Empty)
                    {
                        if (i != figureOnFocusIndexX || j != figureOnFocusIndexY)
                        {
                            e.Graphics.DrawImage(possibleFigures[(int)ChessBoard.Cells[i, j].MyFigure.Color,
                                                                 (int)ChessBoard.Cells[i, j].MyFigure.Type].image,
                                                                 i * cellWidth,
                                                                 j * cellWidth,
                                                                 cellWidth,
                                                                 cellWidth);
                        }
                    }
                }
            }
        }

        private void canvas_Paint_ChessTable(object sender, PaintEventArgs e)
        {
            if (mouseDowned)
            {
                nearDistance = 1000;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        tempDist = Math.Sqrt((i * cellWidth - globalTopLeftX) * (i * cellWidth - globalTopLeftX) +
                                             (j * cellWidth - globalTopLeftY) * (j * cellWidth - globalTopLeftY));
                        if (tempDist < nearDistance)
                        {
                            nearDistance = tempDist;
                            squareOnFocusIndexX = i;
                            squareOnFocusIndexY = j;
                        }
                    }
                }

                e.Graphics.DrawRectangle(nearesrSquareBorder, squareOnFocusIndexX * cellWidth,
                                                              squareOnFocusIndexY * cellWidth,
                                                              cellWidth, cellWidth);
                
                e.Graphics.DrawImage(possibleFigures[(int)ChessBoard.Cells[figureOnFocusIndexX, figureOnFocusIndexY].MyFigure.Color,
                                                     (int)ChessBoard.Cells[figureOnFocusIndexX, figureOnFocusIndexY].MyFigure.Type].imgShadow,
                                                     globalTopLeftX,
                                                     globalTopLeftY,
                                                     cellWidth,
                                                     cellWidth);
            }
        }

        void canvas_MouseDown_ChessTable(object sender, MouseEventArgs e)
        {
            figureOnFocusIndexX = -1;
            figureOnFocusIndexY = -1;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!ChessBoard.Cells[i, j].Empty)
                    {
                        if (ChessBoard.Cells[i, j].MyFigure.Color == myColor)
                        {
                            if (testFigureCollision(ChessBoard.Cells[i, j].MyFigure, j, i, e.X, e.Y))
                            {
                                figureOnFocusIndexX = i;
                                figureOnFocusIndexY = j;
                                mouseDowned = true;

                                localTopLeftX = e.X % cellWidth;
                                localTopLeftY = e.Y % cellWidth;

                                globalTopLeftX = e.X - localTopLeftX;
                                globalTopLeftY = e.Y - localTopLeftY;

                                mouseX = e.X;
                                mouseY = e.Y;

                                ChessLogic.setPossibleCells(i, j);  // mark the possible Cells

                                break;
                            }
                        }
                    }
                }
            }
            canvas.Invalidate();
        }

        void canvas_MouseUp_ChessTable(object sender, MouseEventArgs e)
        {
            mouseDowned = false;
            if (figureOnFocusIndexX >= 0 && ChessBoard.PossMoves[squareOnFocusIndexX, squareOnFocusIndexY])
            {
                ChessLogic.MoveFigureFromTo(figureOnFocusIndexX, figureOnFocusIndexY, squareOnFocusIndexX, squareOnFocusIndexY);
                if (ChessBoard.Cells[figureOnFocusIndexX, figureOnFocusIndexY].MyFigure.Type == TypeOfFigures.Pawn &&
                    squareOnFocusIndexY == 0)
                {
                    //show dialog to change which figure to promote the pawn to
                    canvas.Paint -= canvas_Paint_ChessTable;
                    canvas.Paint += canvas_Paint_FigureChoice;
                    canvas.MouseDown -= canvas_MouseDown_ChessTable;
                    canvas.MouseDown += canvas_MouseDown_FigureChoice;
                    canvas.MouseUp -= canvas_MouseUp_ChessTable;
                    canvas.MouseMove -= canvas_MouseMove_SendInv;
                    //canvas.Invalidate();
                }
                else
                {
                    canvas.Invalidate();
                    figureOnFocusIndexX = -1;
                    figureOnFocusIndexY = -1;
                }
            }
            else
            {
                figureOnFocusIndexX = -1;
                figureOnFocusIndexY = -1;
            }
        }

        void canvas_MouseMove_ChessTable(object sender, MouseEventArgs e)
        {
            if (mouseDowned)
            {
                globalTopLeftX = e.X - localTopLeftX;
                globalTopLeftY = e.Y - localTopLeftY;

                canvas.Invalidate(); //ask for draw
            }
            canvas.Invalidate();
        }

        private bool testFigureCollision(Figure figure, int top, int left, int mouseX, int mouseY)
        {
            int relativeX = mouseX - left * cellWidth;
            int relativeY = mouseY - top * cellWidth;

            if (relativeX >= 0 && relativeY >= 0 &&
                relativeX < possibleFigures[(int)figure.Color, (int)figure.Type].width &&
                relativeY < possibleFigures[(int)figure.Color, (int)figure.Type].height)
            {
                //handle transparency of the image for the figure
                if (possibleFigures[(int)figure.Color, (int)figure.Type].bitmap.GetPixel(relativeX, relativeY).A >= 8)
                {
                    return true;
                }
            }
            return false;
        }

        //=============================================
        //=============================================
        // 
        // promote pawn to something vode and variables
        // 
        //=============================================
        //=============================================

        private void canvas_Paint_FigureChoice(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(userScrollColor, figureChoiseMenuLeft, figureChoiseMenuTop, figureChoiseMenuWidth, figureChoiceMenuHeight);

            e.Graphics.DrawRectangle(figureChoiceBorder, figureChoiseMenuLeft, figureChoiseMenuTop, figureChoiseMenuWidth, figureChoiceMenuHeight);

            if (selectedFigureInd >= 0) {
                e.Graphics.FillRectangle(figureHoverColor, figureChoiseMenuLeft + figureChoiseMargin + selectedFigureInd * (cellWidth + figureChoiseMargin), figureChoiseMenuTop + figureChoiseMargin,
                                     cellWidth, cellWidth);
            }

            for (int i = 0; i < 4; i++) {

                tempLeft = figureChoiseMenuLeft + figureChoiseMargin + i * (cellWidth + figureChoiseMargin);

                e.Graphics.DrawImage(possibleFigures[(int)myColor, i + 1].image, tempLeft, figureChoiseMenuTop + figureChoiseMargin,
                                 cellWidth, cellWidth);

                e.Graphics.DrawRectangle(userNotMarkedPen,
                           figureChoiseMenuLeft + figureChoiseMargin + i * (cellWidth + figureChoiseMargin), figureChoiseMenuTop + figureChoiseMargin,
                                 cellWidth, cellWidth);

                figureChoise1Point1.X = tempLeft - (int)userNotMarkedPen.Width / 2;
                figureChoise1Point2.X = tempLeft + cellWidth + (int)userNotMarkedPen.Width / 2;
                figureChoise1Point3.X = tempLeft + cellWidth - (int)userNotMarkedPen.Width / 2;
                figureChoise1Point4.X = tempLeft + (int)userNotMarkedPen.Width / 2;
                figureChoise1Point5.X = tempLeft + (int)userNotMarkedPen.Width / 2;
                figureChoise1Point6.X = tempLeft - (int)userNotMarkedPen.Width / 2;

                e.Graphics.FillPolygon(userItemOrnamentColor, new Point[]{ figureChoise1Point1,
                                                                           figureChoise1Point2,
                                                                           figureChoise1Point3,
                                                                           figureChoise1Point4,
                                                                           figureChoise1Point5,
                                                                           figureChoise1Point6,});
            }

            e.Graphics.DrawImage(figureChoiceBtn.image, figureChoiceBtn.left, figureChoiceBtn.top, figureChoiceBtn.width, figureChoiceBtn.height);
        }

        void canvas_MouseDown_FigureChoice(object sender, MouseEventArgs e)
        {
            if (e.Y > figureChoiseMenuTop + figureChoiseMargin && e.Y < figureChoiseMenuTop + figureChoiseMargin + cellWidth) {
                for (int i = 0; i < 4; i++)
                {
                    if (e.X > figureChoiseMenuLeft + figureChoiseMargin + i * (cellWidth + figureChoiseMargin) &&
                        e.X < figureChoiseMenuLeft + figureChoiseMargin + i * (cellWidth + figureChoiseMargin) + cellWidth)
                    {
                        selectedFigureInd = i;
                        break;
                    }
                }
            }

            if (testButtonCollision(figureChoiceBtn, e.X, e.Y))
            {
                ChessLogic.promoteTo(squareOnFocusIndexX, squareOnFocusIndexY, (TypeOfFigures)(selectedFigureInd + 1));

                //show dialog to change which figure to promote the pawn to
                canvas.Paint -= canvas_Paint_FigureChoice;
                canvas.Paint += canvas_Paint_ChessTable;
                canvas.MouseDown -= canvas_MouseDown_FigureChoice;
                canvas.MouseDown += canvas_MouseDown_ChessTable;
                canvas.MouseUp += canvas_MouseUp_ChessTable;
                canvas.MouseMove += canvas_MouseMove_SendInv;

                figureOnFocusIndexX = -1;
                figureOnFocusIndexY = -1;
            }

            canvas.Invalidate();
        }
    }

    internal class visualFigure : visualElement
    {
        internal visualFigure(TypeOfFigures type, ColorsOfFigures color, string prefix) :
                        base(color.ToString() + type.ToString(), prefix, ChessBoard.CellWidth, ChessBoard.CellWidth) { }
    }

    internal class visualButton : visualElement
    {
        internal int top;
        internal int left;
        //internal Image imgOver;
        internal visualButton(string name, string prefix, int left, int top, int width, int height) : base(name, prefix, width, height)
        {
            this.left = left;
            this.top = top;
            //imgShadow = Image.FromFile(prefix + name + "Over.png");
        }
    }

    internal class visualElement
    {
        internal Image image;
        internal Bitmap bitmap;
        internal Image imgShadow;
        internal int width;
        internal int height;
        internal bool clicked = false;
        internal visualElement(string name, string prefix, int width, int height)
        {
            image = Image.FromFile(prefix + name + ".png");
            this.width = width;
            this.height = height;
            bitmap = new Bitmap(image, width, height);
            imgShadow = Image.FromFile(prefix + name + "Shaded.png");
        }
    }
}
