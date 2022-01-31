using Vildmark.Serialization;

namespace Miniventure.Entities;

public abstract class Entity : ISerializable
{
    protected static Random Random { get; } = new Random();

    public int X { get; set; }
    public int Y { get; set; }
    public int HorizontalRadius { get; private set; }
    public int VerticalRadius { get; private set; }
    public bool Removed { get; set; }
    public Level Level { get; set; }

    public Entity(int x, int y, int horizontalRadius = 6, int verticalRadius = 6)
    {
        X = x;
        Y = y;
        HorizontalRadius = horizontalRadius;
        VerticalRadius = verticalRadius;
    }

    public virtual void Render(Screen screen)
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Remove()
    {
        Removed = true;
    }

    public virtual bool Intersects(int x0, int y0, int x1, int y1)
    {
        return !(X + HorizontalRadius < x0 || Y + VerticalRadius < y0 || X - HorizontalRadius > x1 || Y - VerticalRadius > y1);
    }

    public virtual bool Blocks(Entity e)
    {
        return false;
    }

    public virtual void Hurt(int dmg, Direction attackDir)
    {
    }

    public virtual void Hurt(Tile tile, int x, int y, int damage)
    {
    }

    public virtual bool Move(int xa, int ya)
    {
        if (xa != 0 || ya != 0)
        {
            bool stopped = true;

            if (xa != 0 && Move2(xa, 0))
            {
                stopped = false;
            }

            if (ya != 0 && Move2(0, ya))
            {
                stopped = false;
            }

            if (!stopped)
            {
                int xt = X >> 4;
                int yt = Y >> 4;

                Level.GetTile(xt, yt).SteppedOn(Level, xt, yt, this);
            }

            return !stopped;
        }

        return true;
    }

    public virtual bool Move2(int xa, int ya)
    {
        if (xa != 0 && ya != 0)
        {
            throw new ArgumentException("Move2 can only move along one axis at a time!");
        }

        int xto0 = X - HorizontalRadius >> 4;
        int yto0 = Y - VerticalRadius >> 4;
        int xto1 = X + HorizontalRadius >> 4;
        int yto1 = Y + VerticalRadius >> 4;

        int xt0 = X + xa - HorizontalRadius >> 4;
        int yt0 = Y + ya - VerticalRadius >> 4;
        int xt1 = X + xa + HorizontalRadius >> 4;
        int yt1 = Y + ya + VerticalRadius >> 4;

        bool blocked = false;

        for (int yt = yt0; yt <= yt1; yt++)
        {
            for (int xt = xt0; xt <= xt1; xt++)
            {
                if (xt >= xto0 && xt <= xto1 && yt >= yto0 && yt <= yto1)
                {
                    continue;
                }

                Level.GetTile(xt, yt).BumpedInto(Level, xt, yt, this);

                if (!Level.GetTile(xt, yt).MayPass(Level, xt, yt, this))
                {
                    return false;
                }
            }
        }

        if (blocked)
        {
            return false;
        }

        IEnumerable<Entity> wasInside = Level.GetEntities(X - HorizontalRadius, Y - VerticalRadius, X + HorizontalRadius, Y + VerticalRadius);
        IEnumerable<Entity> isInside = Level.GetEntities(X + xa - HorizontalRadius, Y + ya - VerticalRadius, X + xa + HorizontalRadius, Y + ya + VerticalRadius);

        foreach (var entity in isInside)
        {
            if (entity == this)
            {
                continue;
            }

            entity.TouchedBy(this);
        }

        foreach (var entity in isInside.Except(wasInside))
        {
            if (entity == this)
            {
                continue;
            }

            if (entity.Blocks(this))
            {
                return false;
            }
        }

        X += xa;
        Y += ya;

        return true;
    }

    public virtual void TouchedBy(Entity entity)
    {
    }

    public virtual bool IsBlockableBy(Mob mob)
    {
        return true;
    }

    public virtual void TouchItem(ItemEntity itemEntity)
    {
    }

    public virtual bool CanSwim()
    {
        return false;
    }

    public virtual bool Interact(Player player, Item item, Direction attackDir)
    {
        return item.Interact(player, this, attackDir);
    }

    public virtual bool Use(Player player, Direction attackDir)
    {
        return false;
    }

    public virtual int GetLightRadius()
    {
        return 0;
    }

    public virtual void Serialize(IWriter writer)
    {
        writer.WriteValue(X);
        writer.WriteValue(Y);
        writer.WriteValue(HorizontalRadius);
        writer.WriteValue(VerticalRadius);
        writer.WriteValue(Removed);
    }

    public virtual void Deserialize(IReader reader)
    {
        X = reader.ReadValue<int>();
        Y = reader.ReadValue<int>();
        HorizontalRadius = reader.ReadValue<int>();
        VerticalRadius = reader.ReadValue<int>();
        Removed = reader.ReadValue<bool>();
    }
}