using Miniventure.Entities;
using Miniventure.Graphics;

namespace Miniventure.Levels.Tiles;

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
