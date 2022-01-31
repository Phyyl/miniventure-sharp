using Miniventure.Entities;
using Miniventure.Entities.Particles;
using Miniventure.Graphics;
using Miniventure.Items;

namespace Miniventure.Levels.Tiles;


public class CloudCactusTile : Tile
{
    public CloudCactusTile(int id) : base(id)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int color = Color.Get(444, 111, 333, 555);
        screen.Render(x * 16 + 0, y * 16 + 0, 17 + 1 * 32, color, 0);
        screen.Render(x * 16 + 8, y * 16 + 0, 18 + 1 * 32, color, 0);
        screen.Render(x * 16 + 0, y * 16 + 8, 17 + 2 * 32, color, 0);
        screen.Render(x * 16 + 8, y * 16 + 8, 18 + 2 * 32, color, 0);
    }


    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        if (e is AirWizard)
        {
            return true;
        }

        return false;
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        hurt(level, x, y, 0);
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        {
            ToolItem tool = (ToolItem)item;
            if (tool.Type == ToolType.Pickaxe)
            {
                if (player.PayStamina(6 - (int)tool.Level))
                {
                    hurt(level, xt, yt, 1);
                    return true;
                }
            }
        }
        return false;
    }


    public virtual void hurt(Level level, int x, int y, int dmg)
    {
        byte damage = (byte)(level.GetData(x, y) + 1);
        level.Add(new SmashParticle(x * 16 + 8, y * 16 + 8));
        level.Add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, Color.Get(-1, 500, 500, 500)));
        if (dmg > 0)
        {
            if (damage >= 10)
            {
                level.SetTile(x, y, cloud, 0);
            }
            else
            {
                level.SetData(x, y, damage);
            }
        }
    }

    public override void BumpedInto(Level level, int x, int y, Entity entity)
    {
        if (entity is AirWizard)
        {
            return;
        }

        entity.Hurt(this, x, y, 3);
    }
}