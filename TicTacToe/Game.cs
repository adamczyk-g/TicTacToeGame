using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum GameResult { InProgress, CrossWin, NoughtWin, Draw };
    public enum BoardFieldState { Empty, Cross, Nought };

    public class Game
    {
        private int movesCounter;
        private readonly GameResult result;
        private readonly BoardFieldState[] board = new BoardFieldState[9];

        public Game()
        {
            movesCounter = 0;
            result = GameResult.InProgress;
            for (int i = 0; i < 9; i++) board[i] = BoardFieldState.Empty;
        }

        public BoardFieldState FieldState(int fieldNumber)
        {
            return board[fieldNumber];
        }

        public void Move(int fieldNumber)
        {
            if (CheckResult() != GameResult.InProgress)
                throw new InvalidOperationException("No moves allowed when game is over");

            if (fieldNumber < 0 || fieldNumber > 8)
                throw new ArgumentOutOfRangeException("Wrong input: field number must be between 0 and 8");

            if (board[fieldNumber] != BoardFieldState.Empty)
                throw new InvalidOperationException("Field is not empty");

            board[fieldNumber] = movesCounter++ % 2 == 0 ? BoardFieldState.Cross : BoardFieldState.Nought;
        }

        public void Moves(params int[] filedsNumbers)
        {
            foreach (int i in filedsNumbers) Move(i);
        }

        public GameResult CheckResult()
        {
            List<int[]> winningFields = new List<int[]>()
                {
                    new int[]{0,1,2},
                    new int[]{3,4,5},
                    new int[]{6,7,8},
                    new int[]{0,3,6},
                    new int[]{1,4,7},
                    new int[]{2,5,8},
                    new int[]{0,4,8},
                    new int[]{2,4,6},
                };

            foreach (int[] i in winningFields)
            {
                if (IsSameState(i[0], i[1], i[2]))
                {
                    if (IsCrossInField(i[0])) return GameResult.CrossWin;
                    if (IsNoughtInField(i[0])) return GameResult.NoughtWin;
                }
            }

            if (movesCounter == 9)
                return GameResult.Draw;
            else
                return GameResult.InProgress;
        }

        private bool IsCrossInField(int fieldNumber)
        {
            return FieldState(fieldNumber) == BoardFieldState.Cross;
        }

        private bool IsNoughtInField(int fieldNumber)
        {
            return FieldState(fieldNumber) == BoardFieldState.Nought;
        }

        private bool IsSameState(int a, int b, int c)
        {
            return board[a] == board[b] && board[a] == board[c];
        }
    }
}
