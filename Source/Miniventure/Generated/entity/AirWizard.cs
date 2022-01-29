using Miniventure.Generated.gfx;

namespace com.mojang.ld22.entity;

public class AirWizard : Mob
{
    private int xa, ya;
    private int randomWalkTime = 0;
    private int attackDelay = 0;
    private int attackTime = 0;
    private int attackType = 0;

    public AirWizard()
        : base(2000, Random.NextInt(64 * 16), Random.NextInt(64 * 16))
    {
    }

    public override void Update()
    {
        base.Update();

        if (attackDelay > 0)
        {
            Direction = (Direction)((attackDelay - 45) / 4 % 4);
            Direction = (Direction)(((int)Direction * 2 % 4) + ((int)Direction / 2));

            if (attackDelay < 45)
            {
                Direction = Direction.Down;
            }

            attackDelay--;

            if (attackDelay == 0)
            {
                attackType = 0;

                if (Health < 1000)
                {
                    attackType = 1; 
                }

                if (Health < 200)
                {
                    attackType = 2; 
                }

                attackTime = 60 * 2; 
            }

            return;
        }

        if (attackTime > 0)
        {
            attackTime--;

            double dir = attackTime * 0.25 * ((attackTime % 2 * 2) - 1);
            double curSpeed = 0.7 + (attackType * 0.2);

            Level.Add(new Spark(this, Math.Cos(dir) * curSpeed, Math.Sin(dir) * curSpeed));

            return;
        }

        if (Level.player != null && randomWalkTime == 0)
        {
            int xd = Level.player.X - X;
            int yd = Level.player.Y - Y;

            if ((xd * xd) + (yd * yd) < 32 * 32)
            {
                xa = 0;
                ya = 0;

                if (xd < 0)
                {
                    xa = 1;
                }

                if (xd > 0)
                {
                    xa = -1;
                }

                if (yd < 0)
                {
                    ya = 1;
                }

                if (yd > 0)
                {
                    ya = -1;
                }
            }
            else if ((xd * xd) + (yd * yd) > 80 * 80)
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

        int speed = (TickTime % 4) == 0 ? 0 : 1;

        if (!Move(xa * speed, ya * speed) || Random.NextInt(100) == 0)
        {
            randomWalkTime = 30;

            xa = Random.NextInt(3) - 1;
            ya = Random.NextInt(3) - 1;
        }
        if (randomWalkTime > 0)
        {
            randomWalkTime--;

            if (Level.player != null && randomWalkTime == 0)
            {
                int xd = Level.player.X - X;
                int yd = Level.player.Y - Y;

                if (Random.NextInt(4) == 0 && (xd * xd) + (yd * yd) < 50 * 50)
                {
                    if (attackDelay == 0 && attackTime == 0)
                    {
                        attackDelay = 60 * 2;
                    }
                }
            }
        }
    }

    public override void Render(Screen screen)
    {
        int xt = 8; 
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

        int col1 = Color.Get(-1, 100, 500, 555); 
        int col2 = Color.Get(-1, 100, 500, 532); 

        if (Health < 200)
        { 
            if (TickTime / 3 % 2 == 0)
            { 
                col1 = Color.Get(-1, 500, 100, 555); 
                col2 = Color.Get(-1, 500, 100, 532); 
            }
        }
        else if (Health < 1000)
        { 
            if (TickTime / 5 % 4 == 0)
            { 
                col1 = Color.Get(-1, 500, 100, 555); 
                col2 = Color.Get(-1, 500, 100, 532); 
            }
        }

        if (ImmuneTime > 0)
        {
            col1 = Color.Get(-1, 555, 555, 555); 
            col2 = Color.Get(-1, 555, 555, 555); 
        }

        //TODO: flip1 and flip2 should already be MirrorFlags
        screen.Render(xo + (8 * flip1), yo + 0, xt + (yt * 32), col1, (MirrorFlags)flip1); 
        screen.Render(xo + 8 - (8 * flip1), yo + 0, xt + 1 + (yt * 32), col1, (MirrorFlags)flip1); 
        screen.Render(xo + (8 * flip2), yo + 8, xt + ((yt + 1) * 32), col2, (MirrorFlags)flip2); 
        screen.Render(xo + 8 - (8 * flip2), yo + 8, xt + 1 + ((yt + 1) * 32), col2, (MirrorFlags)flip2); 
    }

    public override void TouchedBy(Entity entity)
    {
        if (entity is Player)
        { 
            entity.Hurt(this, 3, Direction); 
        }
    }

    public override void Die()
    {
        base.Die(); 

        if (Level.player != null)
        { 
            Level.player.score += 1000; 
            Level.player.GameWon(); 
        }

        Sound.bossdeath.Play(); 
    }
}