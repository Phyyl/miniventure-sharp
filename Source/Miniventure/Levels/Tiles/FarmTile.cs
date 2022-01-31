using Miniventure.Entities;
using Miniventure.Graphics;
using Miniventure.Items;

namespace Miniventure.Levels.Tiles;

public class FarmTile : Tile
{
    public FarmTile(int id) : base(id)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.DirtColor - 121, level.DirtColor - 11, level.DirtColor, level.DirtColor + 111);
        screen.Render((x * 16) + 0, (y * 16) + 0, 2 + 32, col, MirrorFlags.Horizontal);
        screen.Render((x * 16) + 8, (y * 16) + 0, 2 + 32, col, 0);
        screen.Render((x * 16) + 0, (y * 16) + 8, 2 + 32, col, 0);
        screen.Render((x * 16) + 8, (y * 16) + 8, 2 + 32, col, MirrorFlags.Horizontal);
    }


    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        {
            ToolItem tool = (ToolItem)item;
            if (tool.Type == ToolType.Shovel)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    level.SetTile(xt, yt, Tile.dirt, 0);
                    return true;
                }
            }
        }
        return false;
    }

    public override void Update(Level level, int xt, int yt)
    {
        byte age = level.GetData(xt, yt);

        if (age < 5)
        {
            level.SetData(xt, yt, (byte)(age + 1));
        }
    }


    public override void SteppedOn(Level level, int xt, int yt, Entity entity)
    {

        if (random.NextInt(60) != 0)
        {
            return;
        }

        if (level.GetData(xt, yt) < 5)
        {
            return;
        }

        level.SetTile(xt, yt, Tile.dirt, 0);

    }
}
