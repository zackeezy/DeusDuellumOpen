﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakthrough
{
    enum PlayerTypes { Human=0,AI=1,Network=2 }
   public class Move : GameCoreToken
    {
        public int whiteFromX;
        public int whiteFromY;
        public int whiteToX;
        public int whiteToY;

        public int blackFromX;
        public int blackFromY;
        public int blackToX;
        public int blackToY;

        public bool Exit { set; get; }
        public bool isWhiteTurn;
        public bool isWhite;

        public Move()
        {
            whiteFromX=0;
            whiteFromY=0;
            whiteToX=0;
            whiteToY=0;

            blackFromX=0;
            blackFromY=0;
            blackToX=0;
            blackToY=0;

            isWhiteTurn = true;
            isWhite = true;
            Exit = false;
        }

        public void GetMove()
        {
            if (isWhiteTurn)
            {
                WhiteGetInput();
            }
            else
            {
                BlackGetInput();
            }
            

            if (!Exit)
            {
                RearrangeTokens();
            }
        }
        private void BlackGetInput()
        {
            //get input and validate it
            Console.WriteLine("Enter Black Token's from coordinate X");
            Exit = BlackValidateInput(int.TryParse(Console.ReadLine(), out blackFromY));

            if (!Exit)//if previous validation passed, move to the next coordinates
            {
                Console.WriteLine("Enter Black Token's from coordinate Y");
                Exit = BlackValidateInput(int.TryParse(Console.ReadLine(), out blackFromX));
            }
            if (!Exit)//if previous validation passed, move to the next coordinates
            {
                Console.WriteLine("Enter Black Token's to coordinate X");
                Exit = BlackValidateInput(int.TryParse(Console.ReadLine(), out blackToY));
            }
            if (!Exit)//if previous validation passed, move to the next coordinates
            {
                Console.WriteLine("Enter Black Token's to coordinate Y");
                Exit = BlackValidateInput(int.TryParse(Console.ReadLine(), out blackToX));
            }
        }
        private void WhiteGetInput()
        {
                //get input and validate it
                Console.WriteLine("Enter White Token's from coordinate X");
                Exit = whiteValidateInput(int.TryParse(Console.ReadLine(), out whiteFromY));

                if (!Exit)//if previous validation passed, move to the next coordinates
                {
                    Console.WriteLine("Enter White Token's from coordinate Y");
                    Exit = whiteValidateInput(int.TryParse(Console.ReadLine(), out whiteFromX));
                }
                if (!Exit)//if previous validation passed, move to the next coordinates
                {
                    Console.WriteLine("Enter White Token's to coordinate X");
                    Exit = whiteValidateInput(int.TryParse(Console.ReadLine(), out whiteToY));
                }
                if (!Exit)//if previous validation passed, move to the next coordinates
                {
                    Console.WriteLine("Enter White Token's to coordinate Y");
                    Exit = whiteValidateInput(int.TryParse(Console.ReadLine(), out whiteToX));
                }
             
            //if (isWhiteTurn && toY == 7)
            //{
            //    Console.WriteLine("White wins");
            //}
            //if (!isWhiteTurn && toY == 0)
            //{
            //    Console.WriteLine("Black Wins");
            //}
        }
        private bool BlackValidateInput(bool parsed)
        {
            bool error = false;

            if (!parsed)
            {
                error = true;
            }
            else if (blackFromX < 0 || blackFromY < 0 || blackToX < 0 || blackToY < 0)
            {
                error = true;
            }
            else if (blackFromX > BreakthroughBoard.boardSize - 1 || blackFromY > BreakthroughBoard.boardSize - 1
                || blackToX > BreakthroughBoard.boardSize - 1 || blackToY > BreakthroughBoard.boardSize - 1)
            {
                error = true;
            }
            else if (blackToX == blackFromX + 1 && blackToY == blackFromY)
            {
                error = true;
            }
            if (error)
            {
                Console.WriteLine("Invalid input. Exiting program now.");
            }

            return error;
        }
        private bool whiteValidateInput(bool parsed)
        {
            bool error = false;

            if (!parsed)
            {
                error = true;
            }
            else if (whiteFromX < 0 || whiteFromY < 0 || whiteToX < 0 || whiteToY < 0)
            {
                error = true;
            }
            else if (whiteFromX > BreakthroughBoard.boardSize - 1 || whiteFromY > BreakthroughBoard.boardSize - 1
                || whiteToX > BreakthroughBoard.boardSize - 1 || whiteToY > BreakthroughBoard.boardSize - 1)
            {
                error = true;
            }
            if (error)
            {
                Console.WriteLine("Invalid input. Exiting program now.");
            }

            return error;
        }

        public bool allowMove()
        {
            bool isAllowed = false;
            if (isWhite && tokens[whiteFromX, whiteFromY] == 'W')
            {
                //diagonal left
                if (whiteFromX != 0 && whiteFromY != 7)
                {
                    if (whiteToX == whiteFromX - 1 && whiteToY == whiteFromY + 1)
                    {
                        isAllowed = true;
                    }
                }
                //diagonal right
                if (whiteFromX != 7 && whiteFromY != 7)
                {
                    if (whiteToX == whiteFromX + 1 && whiteToY == whiteFromY + 1)
                    {
                        isAllowed = true;
                    }
                }
                //middle
                if (whiteFromX != 7)
                {
                    if (whiteToX == whiteFromX  && whiteToY == whiteFromY + 1
                        && tokens[whiteToX, whiteToY] != 'B' 
                        && tokens[whiteToX, whiteToY] != 'W')
                    {
                        isAllowed = true;
                    }
                }
       
            }
            else if (!isWhite && tokens[blackFromX, blackFromY] == 'B')
            {
                //diagonal left
                if (blackFromX != 0 && blackFromY != 0)
                {
                    if (blackToX == blackFromX + 1 && blackToY == blackFromY - 1)
                    {
                        isAllowed = true;
                    }
                }
                //diagonal right
                if(blackFromX != 0 && blackFromY != 7)
                {
                    if (blackToX == blackFromX - 1 && blackToY == blackFromY - 1)
                    {
                        isAllowed = true;
                    }
                }
                //middle
                if(blackFromX != 0)
                {
                    if (blackToX == blackFromX && blackToY == blackFromY - 1
                        && tokens[blackToX, blackToY] != 'B'
                        && tokens[blackToX, blackToY] != 'W')
                    {
                        isAllowed = true;
                    }
                }
            }

            return isAllowed;
        }

        public void RearrangeTokens()
        {
            if (isWhite && allowMove())
            {
                tokens[whiteToX, whiteToY] = tokens[whiteFromX, whiteFromY];
                tokens[whiteFromX, whiteFromY] = DASH;//once moved, place a dash in its place
                isWhite = false;
                isWhiteTurn = false;
            }
            else if(!isWhite && allowMove())
            {
                tokens[blackToX, blackToY] = tokens[blackFromX, blackFromY];
                tokens[blackFromX, blackFromY] = DASH;//once moved, place a dash in its place
                isWhite = true;
                isWhiteTurn = true;
            }
        }
    }
}