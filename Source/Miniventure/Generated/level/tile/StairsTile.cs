namespace com.mojang.ld22.level.tile;


public class StairsTile : Tile
{
    private readonly bool leadsUp; // value to tell if the stairs lead up or not

    public StairsTile(int id, bool leadsUp) : base(id)
    { // assigns the id
        this.leadsUp = leadsUp; // assigns the leadsUp value
    }

    /** Render method, draws sprites on the screen */
    public override void Render(Screen screen, Level level, int x, int y)
    {
        int color = Color.Get(level.dirtColor, 000, 333, 444); // the color of the stairs
        int xt = 0; // the x tile position on the tile sheet
        if (leadsUp)
        {
            xt = 2; // if the stairs lead up, then move the x tile position over 2 times
        }

        screen.Render((x * 16) + 0, (y * 16) + 0, xt + (2 * 32), color, 0); // renders the top left part of the sprite
        screen.Render((x * 16) + 8, (y * 16) + 0, xt + 1 + (2 * 32), color, 0); // renders the top right part of the sprite
        screen.Render((x * 16) + 0, (y * 16) + 8, xt + (3 * 32), color, 0); // renders the bottom left part of the sprite
        screen.Render((x * 16) + 8, (y * 16) + 8, xt + 1 + (3 * 32), color, 0); // renders the bottoms right part of the sprite
    }
}
