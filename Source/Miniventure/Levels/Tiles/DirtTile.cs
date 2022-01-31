namespace Miniventure.Levels.Tiles;


public record class DirtTile : Tile
{
    public DirtTile(byte id)
        : base(id)
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
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Shovel)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    level.SetTile(xt, yt, Hole, 0);
                    level.Add(new ItemEntity(new ResourceItem(Resource.Dirt), xt * 16 + random.NextInt(10) + 3, yt * 16 + random.NextInt(10) + 3));
                    AudioTracks.MonsterHurt.Play();
                    return true;
                }
            }
            if (tool.Type == ToolType.Hoe)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    level.SetTile(xt, yt, Farmland, 0);
                    AudioTracks.MonsterHurt.Play();
                    return true;
                }
            }
        }
        return false;
    }
}
