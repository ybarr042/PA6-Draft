﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace PA6_Draft
{
    public partial class Chessboard : Form
    {
        private Brush LightColor;
        private Brush DarkColor;
        private Brush Highlighted;
        private ChessGame Game;
        private Square Picked;
        private Square Dropped;
        private Point PickedLocation;
        private bool time_control = false;
        private bool timer1_less10s = true;
        private bool timer2_less10s = true;
        SoundPlayer player_check;
        SoundPlayer player_checkmate;
        SoundPlayer player_stalemate;
        SoundPlayer player_time_left;
        SoundPlayer player_move;
        SoundPlayer player_capture;
        private Dictionary<Piece, Bitmap> PieceImages;//BlackPawn,WhitePawn,BlackRook,WhiteRook,BlackKnight,WhiteKnight,BlackBishop,WhiteBishop
                                                      //,BlackKing, WhiteKing, BlackQueen, WhiteQueen;


        internal Chessboard(Color Light, Color Dark, ChessGame Game)
        {
            InitializeComponent();

            PieceImages = new Dictionary<Piece, Bitmap>();
            PieceImages.Add(Piece.BPAWN, new Bitmap(new Bitmap(@"bp.png"), new Size(64, 64)));
            PieceImages.Add(Piece.WPAWN, new Bitmap(new Bitmap(@"wp.png"), new Size(64, 64)));
            PieceImages.Add(Piece.BROOK, new Bitmap(new Bitmap(@"br.png"), new Size(64, 64)));
            PieceImages.Add(Piece.WROOK, new Bitmap(new Bitmap(@"wr.png"), new Size(64, 64)));
            PieceImages.Add(Piece.BKNIGHT, new Bitmap(new Bitmap(@"bkn.png"), new Size(64, 64)));
            PieceImages.Add(Piece.WKNIGHT, new Bitmap(new Bitmap(@"wkn.png"), new Size(64, 64)));
            PieceImages.Add(Piece.BBISHOP, new Bitmap(new Bitmap(@"bb.png"), new Size(64, 64)));
            PieceImages.Add(Piece.WBISHOP, new Bitmap(new Bitmap(@"wb.png"), new Size(64, 64)));
            PieceImages.Add(Piece.BKING, new Bitmap(new Bitmap(@"bk.png"), new Size(64, 64)));
            PieceImages.Add(Piece.WKING, new Bitmap(new Bitmap(@"wk.png"), new Size(64, 64)));
            PieceImages.Add(Piece.BQUEEN, new Bitmap(new Bitmap(@"bq.png"), new Size(64, 64)));
            PieceImages.Add(Piece.WQUEEN, new Bitmap(new Bitmap(@"wq.png"), new Size(64, 64)));
            LightColor = new SolidBrush(Light);
            DarkColor = new SolidBrush(Dark);
            Highlighted = new SolidBrush(Color.FromArgb(100, Color.FromName("yellow")));
            this.Game = Game;
            Player1.Text = Game.Player1Name;
            Player2.Text = Game.Player2Name;
            Game.Promote += Game_Promote;
            Game.Check_in_Game += Game_Check;
            Game.Move_Pieces += Game_Moves;
            Game.Checkmate += Game_Checkmate;
            Game.StalemateGame += Game_Stalemate;
            Game.Capture += Game_Capture;
            player_check = new SoundPlayer(@"Resources\check.wav");
            player_checkmate = new SoundPlayer(@"Resources\checkmate.wav");
            player_stalemate = new SoundPlayer(@"Resources\checkmate.wav");
            player_time_left = new SoundPlayer(@"Resources\time_left_clock.wav");
            player_move = new SoundPlayer(@"Resources\chess_move.wav");
            player_capture = new SoundPlayer(@"Resources\captures.wav");
            Picked = new Square(0, 'z');
            Dropped = new Square(0, 'z');
            Board.Image = new Bitmap(512, 512);
            Board_Paint(null, null);
            this.listBox1.DataSource = Game.Moves;
            Player1Time.Text = Game.WhiteTimeLimit.ToString();
            Player2Time.Text = Game.BlackTimeLimit.ToString();
        }
        private object Game_Promote(Move move)
        {
            return ((int)move.MovedPiece % 2 == 0) ? Promotion.BQUEEN : Promotion.WQUEEN;
        }

        private object Game_Check(Move move)
        {
            player_check.Play();
            return move;
        }

        private object Game_Checkmate(Move move)
        {
            player_checkmate.Play();
            return move;
        }

        private object Game_Stalemate(Move move)
        {
            player_stalemate.Play();

            if (Game.IsGameStalemate())
            {
                if (Game.WhiteTurn)
                {
                    MainTimer.Stop();
                    MessageBox.Show(Game.Player1Name + " lost by Stalemate");
                }
                else
                {
                    MainTimer.Stop();
                    MessageBox.Show(Game.Player2Name + " lost by Stalemate");
                }
            }

            return move;
        }

        private void Game_TimeLeft()
        {
            //player = new SoundPlayer(@"Resources\time_left_clock.wav");
            player_time_left.Play();
        }

        private object Game_Moves(Move move)
        {
            //player = new SoundPlayer(@"Resources\chess_move.wav");
            player_move.Play();
            return move;
        }

        private object Game_Capture(Move move)
        {
            //player = new SoundPlayer(@"Resources\captures.wav");
            player_capture.Play();
            return move;
        }

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            int sizeUnit = (int)Math.Round(Board.Image.Width / 16.0);
            int X = e.X / (2 * sizeUnit);
            int Y = e.Y / (2 * sizeUnit);
            if (Game.Board[X][Y].Occupant == Piece.NONE)
                return;
            Picked = new Square(Game.Board[X][Y].Rank,
                                Game.Board[X][Y].File,
                                Game.Board[X][Y].Occupant);
            PickedLocation = new Point(e.Location.X - sizeUnit, e.Location.Y - sizeUnit);
            Board.Refresh();
            time_control = true;
        }

        private void Board_MouseUp(object sender, MouseEventArgs e)
        {
            int sizeUnit = (int)Math.Round(Board.Image.Width / 16.0);
            if (Picked.Occupant == Piece.NONE)
                return;
            if (e.X >= Board.Width || e.Y >= Board.Height || e.X < 0 || e.Y < 0)
            {
                Picked = new Square(0, 'z');
                Board.Invalidate();
                return;
            }
            int X = e.X / (2 * sizeUnit);
            int Y = e.Y / (2 * sizeUnit);
            bool Success = Game.Move(new Move(Picked.File - 'a', 8 - Picked.Rank, X, Y));
            if (Success)
                Dropped = new Square(Game.Board[X][Y].Rank,
                                    Game.Board[X][Y].File,
                                    Game.Board[X][Y].Occupant);
            Picked.Occupant = Piece.NONE;
            Board.Invalidate();
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            int sizeUnit = (int)Math.Round(Board.Image.Width / 16.0);
            if (Picked.Occupant != Piece.NONE)
            {
                PickedLocation = new Point(e.Location.X - sizeUnit, e.Location.Y - sizeUnit);
                if (e.X >= Board.Width)
                    PickedLocation.X = Board.Width - sizeUnit;
                if (e.X < 0)
                    PickedLocation.X = -sizeUnit;
                if (e.Y >= Board.Height)
                    PickedLocation.Y = Board.Height - sizeUnit;
                if (e.Y < 0)
                    PickedLocation.Y = -sizeUnit;
            }
            Board.Invalidate();

        }
        private void Board_Paint(object sender, PaintEventArgs e)
        {
            int squareWidth = (int)Math.Round(Board.Image.Width / 8.0);
            using (Graphics g = Graphics.FromImage(Board.Image))
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if ((i + j) % 2 == 0)
                            g.FillRectangle(LightColor, new Rectangle(squareWidth * i, squareWidth * j, squareWidth, squareWidth));
                        else
                            g.FillRectangle(DarkColor, new Rectangle(squareWidth * i, squareWidth * j, squareWidth, squareWidth));
                for (int i = 0; i < 8; i++)
                {
                    g.DrawString("" + (8 - i), new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold),
                        (i % 2 == 0) ? DarkColor : LightColor, new Point(0, 3 * squareWidth / 64 + squareWidth * i));
                    g.DrawString("" + (char)('a' + i), new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold),
                        (i % 2 == 1) ? DarkColor : LightColor, new Point(54 * squareWidth / 64 + squareWidth * i, 498));
                }
                if (Dropped.Occupant != Piece.NONE)
                    g.FillRectangle(Highlighted, new Rectangle(squareWidth * (Dropped.File - 'a'), squareWidth * (8 - Dropped.Rank), squareWidth, squareWidth));
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (Game.Board[i][j].Occupant == Piece.NONE)//empty square
                            continue;
                        if (Picked.Occupant != Piece.NONE)
                            if (Game.Board[i][j].Rank == Picked.Rank && Game.Board[i][j].File == Picked.File)
                                continue;
                        g.DrawImage(PieceImages[Game.Board[i][j].Occupant], new Point(squareWidth * i, squareWidth * j));
                    }
                if (Picked.Occupant == Piece.NONE)
                    return;
                g.FillRectangle(Highlighted,
                    new Rectangle(squareWidth * (Picked.File - 'a'), squareWidth * (8 - Picked.Rank), squareWidth, squareWidth));
                g.DrawImage(PieceImages[Picked.Occupant], PickedLocation);
            }
        }

        private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            Board.Invalidate();
        }

        private void Board_MouseLeave(object sender, EventArgs e)
        {
            Board.Invalidate();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (!Game.IsGameFinished())
            {
                if (Game.WhiteTurn)
                {
                    if (time_control)
                    {
                        if (Game.WLimit < 10000 && timer1_less10s)
                        {
                            Game_TimeLeft();
                            timer1_less10s = false;
                        }
                        if (Game.WLimit <= MainTimer.Interval)
                        {
                            Game.WhiteTimeLimit = "0.00";
                            Game.WLimit = 0;
                            MainTimer.Stop();
                            MessageBox.Show(Game.Player1Name + " lost by timeout");
                        }

                        else
                        {
                            Game.WhiteTimeLimit = Game.TimeToString(Game.WLimit -= MainTimer.Interval);
                            Player1Time.Text = Game.WhiteTimeLimit;
                        }
                    }
                }
                else
                {
                    if (time_control)
                    {
                        if (Game.BLimit < 10000 && timer2_less10s)
                        {
                            Game_TimeLeft();
                            timer2_less10s = false;
                        }
                        if (Game.BLimit <= MainTimer.Interval)
                        {
                            Game.BlackTimeLimit = "0.00";
                            Game.BLimit = 0;
                            MainTimer.Stop();
                            MessageBox.Show(Game.Player2Name + " lost by timeout");
                        }
                        else
                        {
                            Game.BlackTimeLimit = Game.TimeToString(Game.BLimit -= MainTimer.Interval);
                            Player2Time.Text = Game.BlackTimeLimit;
                        }
                    }
                }
            }
        }

        private void Chessboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainTimer.Stop();
        }
    }
}
