namespace com.mojang.ld22.entity;

public abstract class Mob : Entity
{
    private int swimDist;
    protected int XKnockback { get; set; }
    protected int YKnockback { get; set; }

    protected int ImmuneTime { get; set; }
    protected int TickTime { get; private set; }
    protected int WalkDist { get; set; }

    public int MaxHealth { get; }
    public Direction Direction { get; protected set; } = Direction.Down;
    public int Health { get; set; }

    protected Mob(int maxHealth = 10, int x = 8, int y = 8, int horizontalRadius = 4, int verticalRadius = 3)
        : base(x, y, horizontalRadius, verticalRadius)
    {
        MaxHealth = Health = maxHealth;
    }

    public override void Update()
    {
        base.Update();

        TickTime++;

        if (Level.GetTile(X >> 4, Y >> 4) == Tile.Lava)
        {
            //TODO: Make this a responsibility of the lava tile
            Hurt(this, 4, Direction.GetOpposite());
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

    public override void Hurt(Mob mob, int damage, Direction attackDir)
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

        if (Level.player != null)
        {
            int xd = Level.player.X - X;
            int yd = Level.player.Y - Y;

            if ((xd * xd) + (yd * yd) < 80 * 80)
            {
                Sound.monsterHurt.Play();
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

        int xx = (x * 16) + 8;
        int yy = (y * 16) + 8;

        if (level.player != null)
        {
            int xd = level.player.X - xx; 
            int yd = level.player.Y - yy;

            if ((xd * xd) + (yd * yd) < 80 * 80)
            {
                return false;
            }
        }

        int r = level.monsterDensity * 16;

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
