using Miniventure.Items.Tools;

namespace Miniventure.Levels.Tiles;

public record class FlowerTile : GrassTile
{
    public FlowerTile(byte id)
        : base(id)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        base.Render(screen, level, x, y);

        int data = level.GetData(x, y);
        int shape = data / 16 % 2;
        int flowerCol = Color.Get(10, level.GrassColor, 555, 440);

        if (shape == 0)
        {
            screen.Render(x * 16 + 0, y * 16 + 0, 1 + 1 * 32, flowerCol, 0);
        }

        if (shape == 1)
        {
            screen.Render(x * 16 + 8, y * 16 + 0, 1 + 1 * 32, flowerCol, 0);
        }

        if (shape == 1)
        {
            screen.Render(x * 16 + 0, y * 16 + 8, 1 + 1 * 32, flowerCol, 0);
        }

        if (shape == 0)
        {
            screen.Render(x * 16 + 8, y * 16 + 8, 1 + 1 * 32, flowerCol, 0);
        }
    }

    public override bool Interact(Level level, int x, int y, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Shovel)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    level.Add(new ItemEntity(new ResourceItem(Resource.Flower), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
                    level.Add(new ItemEntity(new ResourceItem(Resource.Flower), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
                    level.SetTile(x, y, Grass, 0);
                    return true;
                }
            }
        }
        return false;
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        int count = random.NextInt(2) + 1;
        for (int i = 0; i < count; i++)
        {
            level.Add(new ItemEntity(new ResourceItem(Resource.Flower), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
        }
        level.SetTile(x, y, Grass, 0);
    }
}