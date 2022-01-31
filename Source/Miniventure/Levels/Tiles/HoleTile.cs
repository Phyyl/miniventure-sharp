using Miniventure.Entities;
using Miniventure.Graphics;

namespace Miniventure.Levels.Tiles;


public class HoleTile : Tile
{
    public HoleTile(int id) : base(id)
    {
        connectsToSand = true;
        connectsToWater = true;
        connectsToLava = true;
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(111, 111, 110, 110);
        int transitionColor1 = Color.Get(3, 111, level.DirtColor - 111, level.DirtColor);
        int transitionColor2 = Color.Get(3, 111, level.SandColor - 110, level.SandColor);

        bool u = !level.GetTile(x, y - 1).ConnectsToLiquid();
        bool d = !level.GetTile(x, y + 1).ConnectsToLiquid();
        bool l = !level.GetTile(x - 1, y).ConnectsToLiquid();
        bool r = !level.GetTile(x + 1, y).ConnectsToLiquid();

        bool su = u && level.GetTile(x, y - 1).connectsToSand;
        bool sd = d && level.GetTile(x, y + 1).connectsToSand;
        bool sl = l && level.GetTile(x - 1, y).connectsToSand;
        bool sr = r && level.GetTile(x + 1, y).connectsToSand;

        if (!u && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 0, 0, col, 0);
        }
        else
        {

            screen.Render(x * 16 + 0, y * 16 + 0, (l ? 14 : 15) + (u ? 0 : 1) * 32, su || sl ? transitionColor2 : transitionColor1, 0);
        }

        if (!u && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 0, 1, col, 0);
        }
        else
        {

            screen.Render(x * 16 + 8, y * 16 + 0, (r ? 16 : 15) + (u ? 0 : 1) * 32, su || sr ? transitionColor2 : transitionColor1, 0);
        }

        if (!d && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 8, 2, col, 0);
        }
        else
        {

            screen.Render(x * 16 + 0, y * 16 + 8, (l ? 14 : 15) + (d ? 2 : 1) * 32, sd || sl ? transitionColor2 : transitionColor1, 0);
        }

        if (!d && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 8, 3, col, 0);
        }
        else
        {

            screen.Render(x * 16 + 8, y * 16 + 8, (r ? 16 : 15) + (d ? 2 : 1) * 32, sd || sr ? transitionColor2 : transitionColor1, 0);
        }
    }


    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return e.CanSwim();
    }

}
