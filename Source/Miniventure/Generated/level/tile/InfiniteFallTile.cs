namespace com.mojang.ld22.level.tile;


public class InfiniteFallTile : Tile
{



    public InfiniteFallTile(int id) : base(id)
    {
    }


    public override void Render(Screen screen, Level level, int x, int y)
    {
    }


    public override void Update(Level level, int xt, int yt)
    {
    }


    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        if (e is AirWizard)
        {
            return true;
        }

        return false;
    }
}
