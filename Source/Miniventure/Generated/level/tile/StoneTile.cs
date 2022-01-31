namespace com.mojang.ld22.level.tile;


public class StoneTile : Tile
{
    public StoneTile(int id) : base(id)
    {
    }


    public override void Render(Screen screen, Level level, int x, int y)
    {
        int rc1 = 111;
        int rc2 = 333;
        int rc3 = 555;
        screen.Render((x * 16) + 0, (y * 16) + 0, 32, Color.Get(rc1, level.DirtColor, rc2, rc3), 0);
        screen.Render((x * 16) + 8, (y * 16) + 0, 32, Color.Get(rc1, level.DirtColor, rc2, rc3), 0);
        screen.Render((x * 16) + 0, (y * 16) + 8, 32, Color.Get(rc1, level.DirtColor, rc2, rc3), 0);
        screen.Render((x * 16) + 8, (y * 16) + 8, 32, Color.Get(rc1, level.DirtColor, rc2, rc3), 0);
    }


    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false;
    }

}
