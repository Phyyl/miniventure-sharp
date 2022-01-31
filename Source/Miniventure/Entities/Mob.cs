using Vildmark.Serialization;

namespace Miniventure.Entities;

public abstract class Mob : Entity
{
    private int swimDist;

    protected int TickTime { get; private set; }

    protected int XKnockback { get; set; }
    protected int YKnockback { get; set; }
    protected int ImmuneTime { get; set; }
    protected int WalkDist { get; set; }

    public int Health { get; set; }
    public int MaxHealth { get; private set; }

    public Direction Direction { get; protected set; } = Direction.Down;

    protected Mob(int maxHealth, int x = 8, int y = 8, int horizontalRadius = 4, int verticalRadius = 3)
        : base(x, y, horizontalRadius, verticalRadius)
    {
        Health = MaxHealth = maxHealth;
    }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteValue(swimDist);
        writer.WriteValue(TickTime);

        writer.WriteValue(XKnockback);
        writer.WriteValue(YKnockback);
        writer.WriteValue(ImmuneTime);
        writer.WriteValue(WalkDist);
        writer.WriteValue(MaxHealth);
        writer.WriteValue(Health);
        writer.WriteValue(Direction);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        swimDist = reader.ReadValue<int>();
        TickTime = reader.ReadValue<int>();

        XKnockback = reader.ReadValue<int>();
        YKnockback = reader.ReadValue<int>();
        ImmuneTime = reader.ReadValue<int>();
        WalkDist = reader.ReadValue<int>();
        MaxHealth = reader.ReadValue<int>();
        Health = reader.ReadValue<int>();
        Direction = reader.ReadValue<Direction>();
    }

    public override void Update()
    {
        base.Update();

        TickTime++;

        if (Level.GetTile(X >> 4, Y >> 4) == Tile.Lava)
        {

            Hurt(4, Direction.GetOpposite());
        }

        if (Health <= 0)
        {
            Die();
        }

        if (ImmuneTime > 0)
        {
            ImmuneTime--;
        }
    }

    public virtual void Die()
    {
        Remove();
    }

    public override bool Move(int xa, int ya)
    {
        if (IsSwimming())
        {
            if (swimDist++ % 2 == 0)
            {
                return true;
            }
        }

        if (XKnockback < 0)
        {
            Move2(-1, 0);
            XKnockback++;
        }

        if (XKnockback > 0)
        {
            Move2(1, 0);
            XKnockback--;
        }

        if (YKnockback < 0)
        {
            Move2(0, -1);
            YKnockback++;
        }

        if (YKnockback > 0)
        {
            Move2(0, 1);
            YKnockback--;
        }

        if (ImmuneTime > 0)
        {
            return true;
        }

        if (xa != 0 || ya != 0)
        {
            WalkDist++;

            if (xa < 0)
            {
                Direction = Direction.Left;
            }

            if (xa > 0)
            {
                Direction = Direction.Right;
            }

            if (ya < 0)
            {
                Direction = Direction.Up;
            }

            if (ya > 0)
            {
                Direction = Direction.Down;
            }
        }

        return base.Move(xa, ya);
    }

    public virtual bool IsSwimming()
    {
        Tile tile = Level.GetTile(X >> 4, Y >> 4);

        return tile == Tile.Water || tile == Tile.Lava;
    }

    public override bool Blocks(Entity e)
    {
        return e.IsBlockableBy(this);
    }

    public override void Hurt(Tile tile, int x, int y, int damage)
    {
        DoHurt(damage, Direction.GetOpposite());
    }

    public override void Hurt(int damage, Direction attackDir)
    {
        DoHurt(damage, attackDir);
    }

    public virtual void Heal(int heal)
    {
        if (ImmuneTime > 0)
        {
            return;
        }

        Level.Add(new TextParticle("" + heal, X, Y, Color.Get(-1, 50, 50, 50)));

        Health += heal;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public virtual void DoHurt(int damage, Direction attackDir)
    {
        if (ImmuneTime > 0)
        {
            return;
        }

        if (Level.Player != null)
        {
            int xd = Level.Player.X - X;
            int yd = Level.Player.Y - Y;

            if (xd * xd + yd * yd < 80 * 80)
            {
                AudioTracks.MonsterHurt.Play();
            }
        }

        Level.Add(new TextParticle("" + damage, X, Y, Color.Get(-1, 500, 500, 500)));
        Health -= damage;

        if (attackDir == Direction.Down)
        {
            YKnockback = +6;
        }

        if (attackDir == Direction.Up)
        {
            YKnockback = -6;
        }

        if (attackDir == Direction.Left)
        {
            XKnockback = -6;
        }

        if (attackDir == Direction.Right)
        {
            XKnockback = +6;
        }

        ImmuneTime = 10;
    }

    public virtual bool TrySpawn(Level level)
    {
        int x = Random.NextInt(level.Width);
        int y = Random.NextInt(level.Height);

        int xx = x * 16 + 8;
        int yy = y * 16 + 8;

        if (level.Player != null)
        {
            int xd = level.Player.X - xx;
            int yd = level.Player.Y - yy;

            if (xd * xd + yd * yd < 80 * 80)
            {
                return false;
            }
        }

        int r = level.MonsterDensity * 16;

        if (level.GetEntities(xx - r, yy - r, xx + r, yy + r).Any())
        {
            return false;
        }

        if (level.GetTile(x, y).MayPass(level, x, y, this))
        {
            X = xx;
            Y = yy;
            return true;
        }

        return false;
    }
}
