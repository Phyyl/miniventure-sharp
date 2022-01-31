namespace Miniventure.Levels.Tiles;

public class CactusTile : Tile
{
    public CactusTile(int id) : base(id)
    {
        connectsToSand = true;
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(20, 40, 50, level.SandColor);
        screen.Render((x * 16) + 0, (y * 16) + 0, 8 + (2 * 32), col, 0);
        screen.Render((x * 16) + 8, (y * 16) + 0, 9 + (2 * 32), col, 0);
        screen.Render((x * 16) + 0, (y * 16) + 8, 8 + (3 * 32), col, 0);
        screen.Render((x * 16) + 8, (y * 16) + 8, 9 + (3 * 32), col, 0);
    }


    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false;
    }


    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        byte damage = (byte)(level.GetData(x, y) + dmg);
        level.Add(new SmashParticle((x * 16) + 8, (y * 16) + 8));
        level.Add(new TextParticle("" + dmg, (x * 16) + 8, (y * 16) + 8, Color.Get(-1, 500, 500, 500)));
        if (damage >= 10)
        {
            int count = random.NextInt(2) + 1;
            for (int i = 0; i < count; i++)
            {
                level.Add(new ItemEntity(new ResourceItem(Resource.cactusFlower), (x * 16) + random.NextInt(10) + 3, (y * 16) + random.NextInt(10) + 3));
            }
            level.SetTile(x, y, Tile.sand, 0);
        }
        else
        {
            level.SetData(x, y, damage);
        }
    }

    public override void BumpedInto(Level level, int x, int y, Entity entity)
    {
        entity.Hurt(this, x, y, 1);
    }

    public override void Update(Level level, int xt, int yt)
    {
        byte damage = level.GetData(xt, yt);
        if (damage > 0)
        {
            level.SetData(xt, yt, (byte)(damage - 1));
        }

    }
}