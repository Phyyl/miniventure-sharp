using Miniventure.Graphics;
using Miniventure.Items;

namespace Miniventure.Entities;


public class Slime : Mob
{
    private int xa, ya;
    private int jumpTime = 0;
    private readonly int lvl;

    public Slime(int lvl)
        : base(lvl * lvl * 5, Random.NextInt(64 * 16), Random.NextInt(64 * 16))
    {
        this.lvl = lvl;
    }

    public override void Update()
    {
        base.Update();

        int speed = 1;
        if (!Move(xa * speed, ya * speed) || Random.NextInt(40) == 0)
        {
            if (jumpTime <= -10)
            {
                xa = Random.NextInt(3) - 1;
                ya = Random.NextInt(3) - 1;

                if (Level.Player != null)
                {
                    int xd = Level.Player.X - X;
                    int yd = Level.Player.Y - Y;


                    if (xd * xd + yd * yd < 50 * 50)
                    {

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

                if (xa != 0 || ya != 0)
                {
                    jumpTime = 10;
                }
            }
        }

        jumpTime--;
        if (jumpTime == 0)
        {
            xa = ya = 0;
        }
    }

    public override void Die()
    {
        base.Die();

        int count = Random.NextInt(2) + 1;
        for (int i = 0; i < count; i++)
        {
            Level.Add(new ItemEntity(new ResourceItem(Resource.slime), X + Random.NextInt(11) - 5, Y + Random.NextInt(11) - 5));
        }

        if (Level.Player != null)
        {
            Level.Player.score += 25 * lvl;
        }

    }

    public override void Render(Screen screen)
    {

        int xt = 0;
        int yt = 18;


        int xo = X - 8;
        int yo = Y - 11;

        if (jumpTime > 0)
        {
            xt += 2;
            yo -= 4;
        }

        int col = Color.Get(-1, 10, 252, 555);
        if (lvl == 2)
        {
            col = Color.Get(-1, 100, 522, 555);
        }

        if (lvl == 3)
        {
            col = Color.Get(-1, 111, 444, 555);
        }

        if (lvl == 4)
        {
            col = Color.Get(-1, 000, 111, 224);
        }

        if (ImmuneTime > 0)
        {
            col = Color.Get(-1, 555, 555, 555);
        }



        screen.Render(xo + 0, yo + 0, xt + yt * 32, col, 0);
        screen.Render(xo + 8, yo + 0, xt + 1 + yt * 32, col, 0);
        screen.Render(xo + 0, yo + 8, xt + (yt + 1) * 32, col, 0);
        screen.Render(xo + 8, yo + 8, xt + 1 + (yt + 1) * 32, col, 0);
    }

    public override void TouchedBy(Entity entity)
    {
        if (entity is Player)
        {
            entity.Hurt(lvl, Direction);
        }
    }
}