using Miniventure.Audio;
using Miniventure.Entities;
using Miniventure.Graphics;
using Miniventure.Items;

namespace Miniventure.Levels.Tiles;


public class GrassTile : Tile
{
    public GrassTile(int id) : base(id)
    {
        connectsToGrass = true;
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.GrassColor, level.GrassColor, level.GrassColor + 111, level.GrassColor + 111);
        int transitionColor = Color.Get(level.GrassColor - 111, level.GrassColor, level.GrassColor + 111, level.DirtColor);

        bool u = !level.GetTile(x, y - 1).connectsToGrass;
        bool d = !level.GetTile(x, y + 1).connectsToGrass;
        bool l = !level.GetTile(x - 1, y).connectsToGrass;
        bool r = !level.GetTile(x + 1, y).connectsToGrass;

        if (!u && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 0, 0, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 0, (l ? 11 : 12) + (u ? 0 : 1) * 32, transitionColor, 0);
        }

        if (!u && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 0, 1, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 0, (r ? 13 : 12) + (u ? 0 : 1) * 32, transitionColor, 0);
        }

        if (!d && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 8, 2, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 8, (l ? 11 : 12) + (d ? 2 : 1) * 32, transitionColor, 0);
        }

        if (!d && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 8, 3, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 8, (r ? 13 : 12) + (d ? 2 : 1) * 32, transitionColor, 0);
        }
    }


    public override void Update(Level level, int xt, int yt)
    {
        int xn = xt;
        int yn = yt;

        if (random.NextBoolean())
        {
            xn += random.NextInt(2) * 2 - 1;
        }
        else
        {
            yn += random.NextInt(2) * 2 - 1;
        }

        if (level.GetTile(xn, yn) == dirt)
        {
            level.SetTile(xn, yn, this, 0);
        }
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
                    level.SetTile(xt, yt, dirt, 0);
                    Sound.monsterHurt.Play();
                    if (random.NextInt(5) == 0)
                    {

                        level.Add(new ItemEntity(new ResourceItem(Resource.seeds), xt * 16 + random.NextInt(10) + 3, yt * 16 + random.NextInt(10) + 3));
                        return true;
                    }
                }
            }

            if (tool.Type == ToolType.Hoe)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    Sound.monsterHurt.Play();
                    if (random.NextInt(5) == 0)
                    {

                        level.Add(new ItemEntity(new ResourceItem(Resource.seeds), xt * 16 + random.NextInt(10) + 3, yt * 16 + random.NextInt(10) + 3));
                        return true;
                    }
                    level.SetTile(xt, yt, farmland, 0);
                    return true;
                }
            }
        }
        return false;

    }
}
