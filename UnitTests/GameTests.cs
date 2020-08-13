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
    }

    public enum GameResult { InProgress};

    public class Game
    {
        private GameResult result;

        public Game()
        {
            result = GameResult.InProgress;
        }

        public GameResult Result { get { return result; } }
    }


}
