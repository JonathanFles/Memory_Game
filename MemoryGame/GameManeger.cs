using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace MemoryGame
{
    internal class GameManager
    {
        private Player m_player1;
        private Player m_player2;

        private Board m_gameBoard;
        private Cell[] m_currectGameCellArray;

        private bool m_player1Turn;
        private bool m_winner;
        private bool m_playAgain;
        private bool m_foundAMatch;

        private int m_totalBoardSize;
        private int m_gameCulomnSize;
        private int m_gameRowSize;

        private int m_currectTurnColumn;
        private int m_rcurrectTurnRow;

        private int m_counterOfMatchedCards;

        private String m_checkForCurrectInput;


        public GameManager()
        {
            nameAndOpponentMethod();

            m_player1Turn = false;
            m_winner = false;
            m_foundAMatch = false;
            m_playAgain = true;

            m_counterOfMatchedCards = 0;

            gameProcess();
        }


        private void gameProcess()
        {
            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                settingBoardSize();
                m_currectGameCellArray = m_gameBoard.getCellArray;

                if (startingPlayerTurn() < 0.5)
                {
                    m_player1Turn = true;
                }
                else
                {
                    m_player1Turn = false;
                }

                m_gameBoard.PrintBoard();

                while (m_winner == false)
                {

                    if (m_player1Turn == true)
                    {
                        do
                        {
                            cardsPick(m_player1, m_player2);

                            if (m_counterOfMatchedCards == m_totalBoardSize / 2)
                            {
                                m_winner = true;
                                break;
                            }
                        }
                        while (m_foundAMatch == true);


                        m_player1Turn = false;
                    }
                    else
                    {
                        do
                        {
                            cardsPick(m_player2, m_player1);

                            if (m_counterOfMatchedCards == m_totalBoardSize / 2)
                            {
                                m_winner = true;
                                break;
                            }
                        }
                        while (m_foundAMatch == true);


                        m_player1Turn = true;
                    }
                }

                if (m_player1.UserScore == m_player2.UserScore)
                {
                    System.Console.WriteLine("Tie!!");

                    System.Console.WriteLine("Do you want to play again? y/n or Y/N");
                    String againInput = System.Console.ReadLine();
                    m_playAgain = checkIfWantToPlayAgain(againInput);
                }
                else
                {
                    if (m_player1.UserScore > m_player2.UserScore)
                    {
                        System.Console.WriteLine(m_player1.PlayerName + " Won the game!!");

                        System.Console.WriteLine("Do you want to play again? y/n or Y/N");
                        String againInput = System.Console.ReadLine();
                        m_playAgain = checkIfWantToPlayAgain(againInput);
                    }
                    else
                    {
                        System.Console.WriteLine(m_player2.PlayerName + " Won the game!!");
                        System.Console.WriteLine("Do you want to play again? y/n or Y/N");
                        String againInput = System.Console.ReadLine();
                        m_playAgain = checkIfWantToPlayAgain(againInput);
                    }
                }
            }
            while (m_playAgain == true);



        }//gameProcess

        private void nameAndOpponentMethod()
        {
            String opponentChoice = "";
            String player1Name = "";
            String player2Name = "";

            System.Console.WriteLine("Please Enter player 1 name:");
            player1Name = System.Console.ReadLine();
            m_player1 = new Player(player1Name);

            System.Console.WriteLine("To play against PC choose 1, for PVP choose 2:");
            opponentChoice = System.Console.ReadLine();



            while (opponentChoice != "1" && opponentChoice != "2")
            {
                System.Console.WriteLine("Invalid input, try again.");
                System.Console.WriteLine("To play against PC choose 1, for PVP choose 2:");
                opponentChoice = System.Console.ReadLine();
            }

            if (opponentChoice == "1")
            {
                m_player2 = new Player("PC");
            }
            else
            {
                System.Console.WriteLine("Please Enter player 2 name:");
                player2Name = System.Console.ReadLine();
                m_player2 = new Player(player2Name);
            }
        }

        private void settingBoardSize()
        {
            String columnSize = "";
            String rowSize = "";


            System.Console.WriteLine("Please Enter the size between 4-6 of the row");
            rowSize = System.Console.ReadLine();

            while (rowSize != "4" && rowSize != "5" && rowSize != "6")
            {
                System.Console.WriteLine("invalid input, try again");
                System.Console.WriteLine("Please Enter the size between 4-6 of the row");
                rowSize = System.Console.ReadLine();
            }

            System.Console.WriteLine("Please Enter the size between 4-6 of the column");
            System.Console.WriteLine("Note! - if you choice 5 in the row, you cant choice it again!");
            columnSize = System.Console.ReadLine();

            if (rowSize == "5")
            {
                while (columnSize != "4" && columnSize != "6")
                {
                    System.Console.WriteLine("invalid input, try again");
                    System.Console.WriteLine("Please Enter the size between 4 or 6 of the column");
                    columnSize = System.Console.ReadLine();
                }
            }
            else
            {
                while (columnSize != "4" && columnSize != "5" && columnSize != "6")
                {
                    System.Console.WriteLine("invalid input, try again");
                    System.Console.WriteLine("Please Enter the size between 4 or 6 of the column");
                    columnSize = System.Console.ReadLine();
                }
            }

            m_gameRowSize = int.Parse(rowSize);
            m_gameCulomnSize = int.Parse(columnSize);
            m_totalBoardSize = int.Parse(rowSize) * int.Parse(columnSize);

            m_gameBoard = new Board(int.Parse(rowSize), int.Parse(columnSize));
        }

        private double startingPlayerTurn()
        {
            Random random = new Random();
            double pickedValue = random.NextDouble();
            return pickedValue;
        }

        private void checkIfCurrctCellInput()
        {
            bool canContinue = false;

            while (canContinue == false)
            {

                UserColumnChoice();
                UserRowChoice();

                int cellLocationOnTheArray = m_currectTurnColumn + (m_gameCulomnSize * (m_rcurrectTurnRow - 1));

                if (m_currectGameCellArray[cellLocationOnTheArray].IfFindMatch == false && m_currectGameCellArray[cellLocationOnTheArray].IfPickedByUser == false)
                {
                    canContinue = true;
                }
                else
                {
                    Console.WriteLine("you entered a picked card, pick a new card");
                    m_checkForCurrectInput = System.Console.ReadLine();
                }
            }
        }

        private void UserColumnChoice()
        {
            String checkMaxBoardSize = "ABCDEF";

            int columns = 0;
            bool flag = false;
            while (flag == false)
            {
                char ch = m_checkForCurrectInput[0];

                if ((ch >= 'A' && ch <= checkMaxBoardSize[m_gameCulomnSize - 1]) && checkValidInputLength(m_checkForCurrectInput))
                {

                    switch (ch)
                    {
                        case 'A':
                            flag = true;
                            columns = 0;
                            break;
                        case 'B':
                            flag = true;
                            columns = 1;
                            break;
                        case 'C':
                            flag = true;
                            columns = 2;
                            break;
                        case 'D':
                            flag = true;
                            columns = 3;
                            break;
                        case 'E':
                            flag = true;
                            columns = 4;
                            break;
                        case 'F':
                            flag = true;
                            columns = 5;
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
                else
                {
                    flag = false;

                    System.Console.WriteLine("invalide culomn, try again.");
                    System.Console.WriteLine("try enter the letter again");
                    m_checkForCurrectInput = System.Console.ReadLine();

                    UserRowChoice();

                }
            }

            m_currectTurnColumn = columns;
        }

        private void UserRowChoice()
        {

            bool flag = false;
            char inputOfTheNumberFromthrString = m_checkForCurrectInput[1];
            int theInputInIntFormat = 0;

            while (inputOfTheNumberFromthrString < '0' && inputOfTheNumberFromthrString > '9' || flag == false || checkValidInputLength(m_checkForCurrectInput))
            {
                inputOfTheNumberFromthrString = m_checkForCurrectInput[1];

                if ((int)Char.GetNumericValue(inputOfTheNumberFromthrString) <= m_gameRowSize && (int)Char.GetNumericValue(inputOfTheNumberFromthrString) != 0)
                {
                    theInputInIntFormat = (int)Char.GetNumericValue(inputOfTheNumberFromthrString);
                    flag = true;
                    break;
                }
                else
                {
                    System.Console.WriteLine("invalide Row, try again.");
                    System.Console.WriteLine("try enter the Row again");
                    m_checkForCurrectInput = System.Console.ReadLine();

                    UserColumnChoice();
                }
            }
            m_rcurrectTurnRow = theInputInIntFormat;
        }

        private bool checkValidInputLength(String str)
        {
            while (m_checkForCurrectInput.Length > 2)
            {
                System.Console.WriteLine("invalide input, try again.");
                System.Console.WriteLine("Enter column letter and row number like (A2):");
                m_checkForCurrectInput = System.Console.ReadLine();

            }

            return true;
        }

        private void cardsPick(Player i_CurrectPlayerTurn, Player i_otherPlayer)
        {
            int firstLocationPicked = 0;
            int secondLocationPicked = 0;

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(i_CurrectPlayerTurn.PlayerName + " score is:" + i_CurrectPlayerTurn.UserScore);
            Console.WriteLine(i_otherPlayer.PlayerName + " score is:" + i_otherPlayer.UserScore);

            m_gameBoard.PrintBoard();

            Console.WriteLine(i_CurrectPlayerTurn.PlayerName + " enter the first card you want to pick (like this: A2):");
            m_checkForCurrectInput = System.Console.ReadLine();
            checkIfPressQ(m_checkForCurrectInput);
            checkIfCurrctCellInput();

            firstLocationPicked = m_currectTurnColumn + (m_gameCulomnSize * (m_rcurrectTurnRow - 1));
            m_currectGameCellArray[firstLocationPicked].IfPickedByUser = true;

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(i_CurrectPlayerTurn.PlayerName + " score is:" + i_CurrectPlayerTurn.UserScore);
            Console.WriteLine(i_otherPlayer.PlayerName + " score is:" + i_otherPlayer.UserScore);
            m_gameBoard.PrintBoard();

            Console.WriteLine("enter the second card you want to pick (like this: A2):");
            m_checkForCurrectInput = System.Console.ReadLine();
            checkIfPressQ(m_checkForCurrectInput);
            checkIfCurrctCellInput();

            secondLocationPicked = m_currectTurnColumn + (m_gameCulomnSize * (m_rcurrectTurnRow - 1));
            m_currectGameCellArray[secondLocationPicked].IfPickedByUser = true;

            Ex02.ConsoleUtils.Screen.Clear();
            m_gameBoard.PrintBoard();

            if (m_currectGameCellArray[firstLocationPicked].charInCell == m_currectGameCellArray[secondLocationPicked].charInCell)
            {
                m_currectGameCellArray[firstLocationPicked].IfFindMatch = true;
                m_currectGameCellArray[secondLocationPicked].IfFindMatch = true;

                i_CurrectPlayerTurn.RaiseScore();
                m_counterOfMatchedCards++;
                m_foundAMatch = true;
            }
            else
            {
                Console.WriteLine("2 seconds delay");
                System.Threading.Thread.Sleep(2000);

                m_currectGameCellArray[firstLocationPicked].IfPickedByUser = false;
                m_currectGameCellArray[secondLocationPicked].IfPickedByUser = false;
                m_foundAMatch = false;
            }

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(i_CurrectPlayerTurn.PlayerName + " score is:" + i_CurrectPlayerTurn.UserScore);
            Console.WriteLine(i_otherPlayer.PlayerName + " score is:" + i_otherPlayer.UserScore);

            m_gameBoard.PrintBoard();

        }

        private bool checkIfWantToPlayAgain(String i_userInput)
        {
            bool flag = true;

            while (i_userInput.Length > 1 && (i_userInput[0] != 'y' || i_userInput[0] != 'Y' || i_userInput[0] != 'n' || i_userInput[0] != 'N'))
            {
                System.Console.WriteLine("wrong input, try again");
                System.Console.WriteLine("Do you want to play again? y/n or Y/N");
                i_userInput = System.Console.ReadLine();
            }

            if (i_userInput[0] == 'n' || i_userInput[0] == 'N')
            {
                flag = false;
            }
            else
            {
                flag = true;
                gameDataReset();
            }

            return flag;
        }

        private void gameDataReset()
        {
            m_player1.UserScore = 0;
            m_player2.UserScore = 0;

            m_counterOfMatchedCards = 0;

            m_winner = false;
        }

        private void checkIfPressQ(String i_UserInput)
        {
            if (i_UserInput[0] == 'Q' && i_UserInput.Length == 1)
            {
                Environment.Exit(0);
            }
        }

    }//class
}
