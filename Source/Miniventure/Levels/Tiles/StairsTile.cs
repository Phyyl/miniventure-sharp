namespace Miniventure.Levels.Tiles;


public class StairsTile : Tile
{
    private readonly bool leadsUp;

    public StairsTile(int id, bool leadsUp) : base(id)
    {
        this.leadsUp = leadsUp;
    }


    public override void Render(Screen screen, Level level, int x, int y)
    {
        int color = Color.Get(level.DirtColor, 000, 333, 444);
        int xt = 0;
        if (leadsUp)
        {
            xt = 2;
        }

        screen.Render(x * 16 + 0, y * 16 + 0, xt + 2 * 32, color, 0);
        screen.Render(x * 16 + 8, y * 16 + 0, xt + 1 + 2 * 32, color, 0);
        screen.Render(x * 16 + 0, y * 16 + 8, xt + 3 * 32, color, 0);
        screen.Render(x * 16 + 8, y * 16 + 8, xt + 1 + 3 * 32, color, 0);
    }
}
