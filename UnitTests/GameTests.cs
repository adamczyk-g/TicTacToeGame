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
            game.Move(0);
            game.Move(1);
            game.Move(2);
            game.Move(3);
            game.Move(4);
            game.Move(5);
            game.Move(6);
            game.Move(7);
            game.Move(8);

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

    }

    public enum GameResult { InProgress};
    public enum BoardFieldState { Empty, Cross, Nought};

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

            board[fieldNumber] = movesCounter++ % 2 == 0? BoardFieldState.Cross: BoardFieldState.Nought;
        }
    }


}
