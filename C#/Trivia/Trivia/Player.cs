namespace Trivia
{
    public class Player
    {
        private int _purse = 0;

        private int _place = 0;

        private bool _isGettingOutOfPenalityBox;

        public Player(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public int Purse => this._purse;

        public int Place => this._place;

        public bool InPenalityBox { get; set; } = false;

        public string AddCoin()
        {
            this._purse++;
            return $"{this.Name} now has {this.Purse} Gold Coins.";
        }

        public string MoveAhead(int roll)
        {
            var place = this.Place + roll;
            this._place = (place > 11) ? place - 12 : place;
            return $"{this.Name}'s new location is {this.Place}";
        }

        public string SendToPenalityBox()
        {
            this.InPenalityBox = true;
            return $"{this.Name} was sent to the penalty box";
        }

        public void IsGettingOutOfPenalityBox()
        {
            this._isGettingOutOfPenalityBox = true;
        }
    }
}