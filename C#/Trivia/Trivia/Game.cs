namespace Trivia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private readonly List<Player> _players = new List<Player>();
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private Player _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                this._popQuestions.AddLast($"Pop Question {i}");
                this._scienceQuestions.AddLast($"Science Question {i}");
                this._sportsQuestions.AddLast($"Sports Question {i}");
                this._rockQuestions.AddLast($"Rock Question {i}");
            }
        }

        public bool IsPlayable => this.PlayerCount >= 2;

        public int PlayerCount => this._players.Count;

        public bool Add(string playerName)
        {
            var player = new Player(playerName);
            this._players.Add(player);
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + this.PlayerCount);
            this._currentPlayer ??= player;
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(this._currentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (this._currentPlayer.InPenalityBox)
            {
                if (roll % 2 != 0)
                {
                    this._currentPlayer.IsGettingOutOfPenalityBox();
                    this._isGettingOutOfPenaltyBox = true;

                    Console.WriteLine($"{this._currentPlayer.Name} is getting out of the penalty box");
                    this.MovePlayerAhead(roll);
                    Console.WriteLine("The category is " + this.CurrentCategory());
                    this.AskQuestion();
                }
                else
                {
                    Console.WriteLine($"{this._currentPlayer.Name} is not getting out of the penalty box");
                    this._isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                this.MovePlayerAhead(roll);
                Console.WriteLine($"The category is {this.CurrentCategory()}");
                this.AskQuestion();
            }
        }

        private string PlayerName(int position)
        {
            return this._players[position].Name;
        }

        private void AskQuestion()
        {
            var category = this.CurrentCategory();

            switch (category)
            {
                case "Pop":
                    Console.WriteLine(this._popQuestions.First());
                    this._popQuestions.RemoveFirst();
                    break;
                case "Science":
                    Console.WriteLine(this._scienceQuestions.First());
                    this._scienceQuestions.RemoveFirst();
                    break;
                case "Sports":
                    Console.WriteLine(this._sportsQuestions.First());
                    this._sportsQuestions.RemoveFirst();
                    break;
                case "Rock":
                    Console.WriteLine(this._rockQuestions.First());
                    this._rockQuestions.RemoveFirst();
                    break;
            }
        }

        private string CurrentCategory()
        {
            switch (this._currentPlayer.Place)
            {
                case 0:
                case 4:
                case 8:
                    return "Pop";
                case 1:
                case 5:
                case 9:
                    return "Science";
                case 2:
                case 6:
                case 10:
                    return "Sports";
                default:
                    return "Rock";
            }
        }

        public bool WasCorrectlyAnswered()
        {
            bool winner;
            if (this._currentPlayer.InPenalityBox)
            {
                if (this._isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    this.AddCoin();

                    winner = this.DidPlayerWin();
                    this.AdvanceToNextPlayer();
                    return winner;
                }

                this.AdvanceToNextPlayer();
                return true;
            }

            Console.WriteLine("Answer was corrent!!!!");
            this.AddCoin();

            winner = this.DidPlayerWin();
            this.AdvanceToNextPlayer();
            return winner;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(this._currentPlayer.SendToPenalityBox());
            this.AdvanceToNextPlayer();
            return true;
        }

        private void AddCoin()
        {
            Console.WriteLine(this._currentPlayer.AddCoin());
        }

        private bool DidPlayerWin()
        {
            return this._currentPlayer.Purse < 6;
        }

        private void AdvanceToNextPlayer()
        {
            var position = this._players.IndexOf(this._currentPlayer);
            position++;
            if (position == this._players.Count)
                position = 0;

            this._currentPlayer = this._players[position];
        }

        private void MovePlayerAhead(int roll)
        {
            Console.WriteLine(this._currentPlayer.MoveAhead(roll));
        }
    }
}
