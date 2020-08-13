// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace UnitTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Before_first_move_game_result_is_in_progress()
        {
            Assert.AreEqual(GameResult.InProgress, new Game().Result);
        }

        [Test]
        public void After_the_first_move_the_selected_field_contains_a_cross()
        {
            Game game = new Game();
            game.Move(0);
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(0));
        }

        [Test]
        public void Move_with_field_number_below_zero_trow_exception()
        {
            Assert.Catch<System.ArgumentOutOfRangeException>(() => new Game().Move(-1));
        }

        [Test]
        public void Move_with_field_number_over_eight_trow_exception()
        {
            Assert.Catch<System.ArgumentOutOfRangeException>(() => new Game().Move(9));
        }

        [Test]
        public void Move_on_not_empty_field_trow_exception()
        {
            Game game = new Game();
            game.Move(0);
            Assert.Catch<InvalidOperationException>(() => game.Move(0));
        }

        [Test]
        public void Even_move_is_cross_odds_move_is_nought()
        {
            Game game = new Game();
            game.Moves(0, 1, 2, 3, 4, 5, 6, 7, 8);

            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(0));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(1));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(2));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(3));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(4));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(5));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(6));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(7));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(8));
        }

        [TestCase(0, 3, 1, 5, 2)]
        [TestCase(3, 8, 4, 1, 5)]
        [TestCase(6, 4, 7, 2, 8)]
        [TestCase(0, 1, 3, 2, 6)]
        [TestCase(1, 2, 4, 8, 7)]
        [TestCase(2, 3, 5, 6, 8)]
        [TestCase(0, 6, 4, 7, 8)]
        [TestCase(2, 5, 4, 1, 6)]
        public void All_these_games_win_cross(int c1, int c2, int c3, int c4, int c5)
        {
            Game game = new Game();
            game.Moves(c1, c2, c3, c4, c5);

            Assert.AreEqual(GameResult.CrossWin, game.CheckResult());
        }

        [TestCase(3, 0, 5, 1, 7, 2)]
        [TestCase(0, 3, 8, 4, 1, 5)]
        [TestCase(0, 6, 4, 7, 2, 8)]
        [TestCase(4, 0, 1, 3, 2, 6)]
        [TestCase(5, 1, 2, 4, 6, 7)]
        [TestCase(0, 2, 3, 5, 1, 8)]
        [TestCase(1, 0, 6, 4, 7, 8)]
        [TestCase(7, 2, 5, 4, 1, 6)]
        public void All_these_games_win_naught(int c1, int c2, int c3, int c4, int c5, int c6)
        {
            Game game = new Game();
            game.Moves(c1, c2, c3, c4, c5, c6);

            Assert.AreEqual(GameResult.NoughtWin, game.CheckResult());
        }

        [TestCase(0, 1, 3, 8, 4, 5, 7, 6, 2)]
        [TestCase(6, 4, 1, 0, 5, 7, 8, 2, 3)]
        public void All_these_games_is_draw(int c1, int c2, int c3, int c4, int c5, int c6, int c7, int c8, int c9)
        {
            Game game = new Game();
            game.Moves(c1, c2, c3, c4, c5, c6, c7, c8, c9);

            Assert.AreEqual(GameResult.Draw, game.CheckResult());
        }

        [Test]
        public void Move_after_the_game_over_throw_exception()
        {
            Game game = new Game();
            game.Moves(0, 3, 1, 5, 2);
            Assert.Catch<System.InvalidOperationException>(() => game.Move(8));
        }
    }

    public enum GameResult { InProgress, CrossWin, NoughtWin, Draw };
    public enum BoardFieldState { Empty, Cross, Nought };

    public class Game
    {
        private int movesCounter;
        private GameResult result;
        private readonly BoardFieldState[] board = new BoardFieldState[9];

        public Game()
        {
            movesCounter = 0;
            result = GameResult.InProgress;
            for (int i = 0; i < 9; i++) board[i] = BoardFieldState.Empty;
        }

        public GameResult Result { get { return result; } }

        public BoardFieldState FieldState(int fieldNumber)
        {
            return board[fieldNumber];
        }

        public void Move(int fieldNumber)
        {
            if (fieldNumber < 0 || fieldNumber > 8)
                throw new ArgumentOutOfRangeException("Wrong input: field number must be between 0 and 8");

            if (board[fieldNumber] != BoardFieldState.Empty)
                throw new InvalidOperationException();

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
