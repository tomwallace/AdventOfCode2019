namespace AdventOfCode2019.Thirteen
{
    public enum GamePixel
    {
        /*
         0 is an empty tile. No game object appears in this tile
         1 is a wall tile. Walls are indestructible barriers.
         2 is a block tile. Blocks can be broken by the ball.
         3 is a horizontal paddle tile. The paddle is indestructible.
         4 is a ball tile. The ball moves diagonally and bounces off objects.
         */
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4
    }
}