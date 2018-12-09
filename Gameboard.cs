namespace Snake
{
    public class Gameboard
    {
        public bool IsStarted { get; set; }
        public bool IsPaused { get; set; }
        public int RoundNumber { get; set; }
        public int TurnNumber { get; set; }
        public int TurnTimeMilliseconds { get; set; }
        public int TimeUntilNextTurnMilliseconds { get; set; }
        public Gameboardsize GameBoardSize { get; set; }
        public int MaxFood { get; set; }
        public Player[] Players { get; set; }
        public Food[] Food { get; set; }
        public Wall[] Walls { get; set; }
    }

    public class Gameboardsize
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Player
    {
        public string Name { get; set; }
        public bool IsSpawnProtected { get; set; }
        public Snake[] Snake { get; set; }
    }

    public class Snake
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Food
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Wall
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

