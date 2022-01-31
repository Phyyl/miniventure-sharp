using Miniventure.Generated.gfx;

namespace com.mojang.ld22.entity;

public class Zombie : Mob
{
    private int xa, ya;
    private int lvl;
    private int randomWalkTime = 0;

    public Zombie(int lvl)
        : base(lvl * lvl * 10, Random.NextInt(64 * 16), Random.NextInt(64 * 16))
    {
        this.lvl = lvl;
    }

    public override void Update()
    {
        base.Update();

        if (Level.Player != null && randomWalkTime == 0)
        {
            int xd = Level.Player.X - X;
            int yd = Level.Player.Y - Y;
            if ((xd * xd) + (yd * yd) < 50 * 50)
            {
                xa = 0;
                ya = 0;
                if (xd < 0)
                {
                    xa = -1;
                }

                if (xd > 0)
                {
                    xa = +1;
                }

                if (yd < 0)
                {
                    ya = -1;
                }

                if (yd > 0)
                {
                    ya = +1;
                }
            }
        }



        int speed = TickTime & 1;
        if (!Move(xa * speed, ya * speed) || Random.NextInt(200) == 0)
        {
            randomWalkTime = 60;
            xa = (Random.NextInt(3) - 1) * Random.NextInt(2);
            ya = (Random.NextInt(3) - 1) * Random.NextInt(2);
        }
        if (randomWalkTime > 0)
        {
            randomWalkTime--;
        }
    }

    public override void Render(Screen screen)
    {

        int xt = 0;
        int yt = 14;


        int flip1 = (WalkDist >> 3) & 1;
        int flip2 = (WalkDist >> 3) & 1;

        if (Direction == Direction.Up)
        {
            xt += 2;
        }

        if (Direction == Direction.Left || Direction == Direction.Right)
        {

            flip1 = 0;
            flip2 = (WalkDist >> 4) & 1;

            if (Direction == Direction.Left)
            {
                flip1 = 1;
            }

            xt += 4 + (((WalkDist >> 3) & 1) * 2);
        }

        int xo = X - 8;
        int yo = Y - 11;

        int col = Color.Get(-1, 10, 252, 050);
        if (lvl == 2)
        {
            col = Color.Get(-1, 100, 522, 050);
        }

        if (lvl == 3)
        {
            col = Color.Get(-1, 111, 444, 050);
        }

        if (lvl == 4)
        {
            col = Color.Get(-1, 000, 111, 020);
        }

        if (ImmuneTime > 0)
        {
            col = Color.Get(-1, 555, 555, 555);
        }


        screen.Render(xo + (8 * flip1), yo + 0, xt + (yt * 32), col, (MirrorFlags)flip1);
        screen.Render(xo + 8 - (8 * flip1), yo + 0, xt + 1 + (yt * 32), col, (MirrorFlags)flip1);
        screen.Render(xo + (8 * flip2), yo + 8, xt + ((yt + 1) * 32), col, (MirrorFlags)flip2);
        screen.Render(xo + 8 - (8 * flip2), yo + 8, xt + 1 + ((yt + 1) * 32), col, (MirrorFlags)flip2);
    }

    public override void TouchedBy(Entity entity)
    {
        if (entity is Player)
        {
            entity.Hurt(lvl + 1, Direction);
        }
    }

    public override void Die()
    {
        base.Die();

        int count = Random.NextInt(2) + 1;
        for (int i = 0; i < count; i++)
        {
            Level.Add(new ItemEntity(new ResourceItem(Resource.cloth), X + Random.NextInt(11) - 5, Y + Random.NextInt(11) - 5));
        }

        if (Level.Player != null)
        {
            Level.Player.score += 50 * lvl;
        }

    }

}