// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Before_first_move_game_result_is_in_progress()
        {
            Game game = new Game();

            Assert.AreEqual(GameResult.InProgress, game.Result);
        }

        [Test]
        public void After_the_first_move_the_selected_field_contains_a_cross()
        {
            Game game = new Game();
            game.Move(0);
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(0));
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
            board[fieldNumber] = movesCounter++ % 2 == 0? BoardFieldState.Cross: BoardFieldState.Nought;
        }
    }


}
