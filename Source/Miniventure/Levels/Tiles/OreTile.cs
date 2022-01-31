namespace Miniventure.Levels.Tiles;


public record class OreTile : Tile
{
    private readonly Resource toDrop;
    private int color;

    public OreTile(byte id, Resource toDrop)
        : base(id)
    {
        this.toDrop = toDrop;
        color = toDrop.Color & 0xffff00;
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        color = (toDrop.Color & unchecked((int)0xffffff00)) + Color.Get(level.DirtColor);

        screen.Render(x * 16 + 0, y * 16 + 0, 17 + 1 * 32, color, 0);
        screen.Render(x * 16 + 8, y * 16 + 0, 18 + 1 * 32, color, 0);
        screen.Render(x * 16 + 0, y * 16 + 8, 17 + 2 * 32, color, 0);
        screen.Render(x * 16 + 8, y * 16 + 8, 18 + 2 * 32, color, 0);
    }

    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false;
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Hurt(level, x, y, 0);
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Pickaxe)
            {
                if (player.PayStamina(6 - (int)tool.Level))
                {
                    Hurt(level, xt, yt, 1);
                    return true;
                }
            }
        }

        return false;
    }

    public virtual void Hurt(Level level, int x, int y, int dmg)
    {
        byte damage = (byte)(level.GetData(x, y) + 1);

        level.Add(new SmashParticle(x * 16 + 8, y * 16 + 8));
        level.Add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, Color.Get(-1, 500, 500, 500)));

        if (dmg > 0)
        {
            int count = random.NextInt(2);

            if (damage >= random.NextInt(10) + 3)
            {
                level.SetTile(x, y, Dirt, 0);
                count += 2;
            }
            else
            {
                level.SetData(x, y, damage);
            }

            for (int i = 0; i < count; i++)
            {
                level.Add(new ItemEntity(new ResourceItem(toDrop), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
            }
        }
    }

    public override void BumpedInto(Level level, int x, int y, Entity entity)
    {
        entity.Hurt(this, x, y, 3);
    }
}