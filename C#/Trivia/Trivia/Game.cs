namespace Trivia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private int _currentPlayer;
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
            _players.Add(playerName);
            _places[this.PlayerCount] = 0;
            _purses[this.PlayerCount] = 0;
            _inPenaltyBox[this.PlayerCount] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            switch (this._places[this._currentPlayer])
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
            var winner = false;

            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    this.AddCoin();

                    this.AdvanceToNextPlayer();
                    return DidPlayerWin();
                }

                this.AdvanceToNextPlayer();
                return true;
            }

            Console.WriteLine("Answer was corrent!!!!");
            this.AddCoin();
            this.AdvanceToNextPlayer();
            return this.DidPlayerWin();
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            this.AdvanceToNextPlayer();
            return true;
        }

        private void AddCoin()
        {
            this._purses[this._currentPlayer]++;
            Console.WriteLine(
                this._players[this._currentPlayer] + " now has " + this._purses[this._currentPlayer] + " Gold Coins.");
        }

        private bool DidPlayerWin()
        {
            return this._purses[this._currentPlayer] < 6;
        }

        private void AdvanceToNextPlayer()
        {
            this._currentPlayer++;
            if (this._currentPlayer == this._players.Count) 
                this._currentPlayer = 0;
        }
    }

}
