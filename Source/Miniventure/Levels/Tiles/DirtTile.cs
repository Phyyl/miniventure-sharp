using Miniventure.Audio;
using Miniventure.Entities;
using Miniventure.Graphics;
using Miniventure.Items;

namespace Miniventure.Levels.Tiles;


public class DirtTile : Tile
{
    public DirtTile(int id) : base(id)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.DirtColor, level.DirtColor, level.DirtColor - 111, level.DirtColor - 111);
        screen.Render(x * 16 + 0, y * 16 + 0, 0, col, 0);
        screen.Render(x * 16 + 8, y * 16 + 0, 1, col, 0);
        screen.Render(x * 16 + 0, y * 16 + 8, 2, col, 0);
        screen.Render(x * 16 + 8, y * 16 + 8, 3, col, 0);
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
                    level.SetTile(xt, yt, hole, 0);
                    level.Add(new ItemEntity(new ResourceItem(Resource.dirt), xt * 16 + random.NextInt(10) + 3, yt * 16 + random.NextInt(10) + 3));
                    Sound.monsterHurt.Play();
                    return true;
                }
            }
            if (tool.Type == ToolType.Hoe)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    level.SetTile(xt, yt, farmland, 0);
                    Sound.monsterHurt.Play();
                    return true;
                }
            }
        }
        return false;
    }
}
