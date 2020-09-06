using NUnit.Framework;
using System;
using TicTacToe;

namespace UnitTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Before_the_first_move_the_game_returns_InProgress_state()
        {
            Assert.AreEqual(GameResult.InProgress, new Game().CheckResult());
        }

        [Test]
        public void After_the_first_move_the_field_where_the_move_was_made_contains_a_cross()
        {
            Game game = new Game();
            game.Move(0);
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(0));
        }

        [Test]
        public void After_the_second_move_the_field_where_the_move_was_made_contains_a_circle()
        {
            Game game = new Game();
            game.Move(0);
            game.Move(1);
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(1));
        }

        [Test]
        public void Perform_an_even_move_places_a_cross_in_the_field_perform_an_odd_move_places_a_circle_in_the_field()
        {
            Game game = new Game();

            game.Moves(0, 1, 3, 8, 4, 5, 7, 6, 2);

            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(0));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(1));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(3));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(8));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(4));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(5));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(7));
            Assert.AreEqual(BoardFieldState.Nought, game.FieldState(6));
            Assert.AreEqual(BoardFieldState.Cross, game.FieldState(2));
        }

        [Test]
        public void Move_to_a_field_with_a_number_below_zero_raises_an_exception()
        {
            Assert.Catch<System.ArgumentOutOfRangeException>(() => new Game().Move(-1));
        }

        [Test]
        public void Move_to_a_field_with_a_number_greater_than_eight_raises_an_exception()
        {
            Assert.Catch<System.ArgumentOutOfRangeException>(() => new Game().Move(9));
        }

        [Test]
        public void Move_to_a_non_empty_field_raises_an_exception()
        {
            Game game = new Game();
            game.Move(0);
            Assert.Catch<InvalidOperationException>(() => game.Move(0));
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
        public void Read_the_state_of_a_field_with_a_number_below_zero_raises_an_exception()
        {
            Assert.Catch<System.ArgumentOutOfRangeException>(() => new Game().FieldState(-1));
        }

        [Test]
        public void Read_the_state_of_a_field_with_a_number_above_eight_raises_an_exception()
        {
            Assert.Catch<System.ArgumentOutOfRangeException>(() => new Game().FieldState(9));
        }

        [Test]
        public void Make_a_move_when_the_game_is_over_raises_an_exception()
        {
            Game game = new Game();
            game.Moves(0, 3, 1, 5, 2);
            Assert.Catch<System.InvalidOperationException>(() => game.Move(8));
        }
    }    
}
