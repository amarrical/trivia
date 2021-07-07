namespace Tests
{
    using System;
    using System.IO;
    using System.Linq;

    using FluentAssertions;

    using Trivia;

    using Xunit;
    using Xunit.Abstractions;

    public class GoldenMasterTest
    {
        private readonly ITestOutputHelper output;

        private Random random = new Random(4);

        private Game game;

        public GoldenMasterTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GoldenMaster()
        {
            GivenNewGame();
            GivenPlayers("Fred", "Steve", "Mike");

            using StringWriter sw = new StringWriter();
            Console.SetOut(sw);
                
            WhenTheGameIsRun();
            ThenTheOutputShouldBeAsExpected(sw);
        }

        private void ThenTheOutputShouldBeAsExpected(StringWriter sw)
        {
            sw.ToString().Should().Be(this.ExpectedOutput());
        }

        private void WhenTheGameIsRun()
        {
            bool _notAWinner;
            do
            {
                this.game.Roll(this.random.Next(5) + 1);

                _notAWinner = this.random.Next(9) == 7 ? this.game.WrongAnswer() : this.game.WasCorrectlyAnswered();
            }
            while (_notAWinner);
        }

        private void GivenPlayers(params string[] players)
        {
            players.ToList().ForEach(p => this.game.Add(p));
        }

        private void GivenNewGame()
        {
            this.game = new Game();
        }

        private string ExpectedOutput()
        {
            return 
@"Fred is the current player
They have rolled a 5
Fred's new location is 5
The category is Science
Science Question 0
Answer was corrent!!!!
Fred now has 1 Gold Coins.
Steve is the current player
They have rolled a 3
Steve's new location is 3
The category is Rock
Rock Question 0
Answer was corrent!!!!
Steve now has 1 Gold Coins.
Mike is the current player
They have rolled a 1
Mike's new location is 1
The category is Science
Science Question 1
Answer was corrent!!!!
Mike now has 1 Gold Coins.
Fred is the current player
They have rolled a 4
Fred's new location is 9
The category is Science
Science Question 2
Answer was corrent!!!!
Fred now has 2 Gold Coins.
Steve is the current player
They have rolled a 3
Steve's new location is 6
The category is Sports
Sports Question 0
Answer was corrent!!!!
Steve now has 2 Gold Coins.
Mike is the current player
They have rolled a 2
Mike's new location is 3
The category is Rock
Rock Question 1
Answer was corrent!!!!
Mike now has 2 Gold Coins.
Fred is the current player
They have rolled a 2
Fred's new location is 11
The category is Rock
Rock Question 2
Answer was corrent!!!!
Fred now has 3 Gold Coins.
Steve is the current player
They have rolled a 4
Steve's new location is 10
The category is Sports
Sports Question 1
Answer was corrent!!!!
Steve now has 3 Gold Coins.
Mike is the current player
They have rolled a 3
Mike's new location is 6
The category is Sports
Sports Question 2
Answer was corrent!!!!
Mike now has 3 Gold Coins.
Fred is the current player
They have rolled a 4
Fred's new location is 3
The category is Rock
Rock Question 3
Question was incorrectly answered
Fred was sent to the penalty box
Steve is the current player
They have rolled a 2
Steve's new location is 0
The category is Pop
Pop Question 0
Question was incorrectly answered
Steve was sent to the penalty box
Mike is the current player
They have rolled a 4
Mike's new location is 10
The category is Sports
Sports Question 3
Answer was corrent!!!!
Mike now has 4 Gold Coins.
Fred is the current player
They have rolled a 1
Fred is getting out of the penalty box
Fred's new location is 4
The category is Pop
Pop Question 1
Answer was correct!!!!
Fred now has 4 Gold Coins.
Steve is the current player
They have rolled a 2
Steve is not getting out of the penalty box
Mike is the current player
They have rolled a 4
Mike's new location is 2
The category is Sports
Sports Question 4
Answer was corrent!!!!
Mike now has 5 Gold Coins.
Fred is the current player
They have rolled a 4
Fred is not getting out of the penalty box
Steve is the current player
They have rolled a 5
Steve is getting out of the penalty box
Steve's new location is 5
The category is Science
Science Question 3
Answer was correct!!!!
Steve now has 4 Gold Coins.
Mike is the current player
They have rolled a 1
Mike's new location is 3
The category is Rock
Rock Question 4
Answer was corrent!!!!
Mike now has 6 Gold Coins.
";
        }
    }
}
