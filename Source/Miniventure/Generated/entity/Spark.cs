using Miniventure.Generated.gfx;

namespace com.mojang.ld22.entity;

public class Spark : Entity
{
    private readonly int lifeTime; // how much time until the spark disappears
    public double xa, ya; // the x and y acceleration
    public double xx, yy; // the x and y positions
    private int time; // the amount of time that has occurred
    private readonly AirWizard owner; // the AirWizard that created this spark

    public Spark(AirWizard owner, double xa, double ya)
        : base(owner.X, owner.Y, 0, 0)
    {
        this.owner = owner; // assigns the owner
        xx = owner.X; // assigns the x position
        yy = owner.Y; // assigns the y position

        this.xa = xa; // assigns the x acceleration
        this.ya = ya; // assigns the x acceleration

        // Max time = 629 ticks. Min time = 600 ticks.
        lifeTime = (60 * 10) + Random.NextInt(30); // the lifetime of this spark is (60 * 10 + (random value between 0 to 29)).
    }

    /** Update method, updates (ticks) 60 times a second */
    public override void Update()
    {
        time++; // increases time by 1
        if (time >= lifeTime)
        { // if time is larger or equal to lifeTime params then[]
            Remove(); // remove this from the world
            return; // skip the rest of the code
        }
        xx += xa; // move the xx position in the x acceleration direction
        yy += ya; // move the yy position in the x acceleration direction
        X = (int)xx; // the x position equals the integer converted xx position.
        Y = (int)yy; // the y position equals the integer converted yy position.


        foreach (var entity in Level.GetEntities(X, Y, X, Y))
        {
            if (entity is Mob mob && entity is not AirWizard)
            { // if the entity is a mob, but not a Air Wizard params then[]
                entity.Hurt(owner, 1, mob.Direction.GetOpposite()); // hurt the mob with 1 damage
            }
        }
    }

    /** Can this entity block you? Nope. */
    public override bool IsBlockableBy(Mob mob)
    {
        return false;
    }

    /** Renders the spark on the screen */
    public override void Render(Screen screen)
    {
        /* this first part is for the blinking effect */
        if (time >= lifeTime - (6 * 20))
        {// if time is larger or equal to lifeTime - 6 * 20 params then[]
            if (time / 6 % 2 == 0)
            {
                return; // if the remainder of (time/6)/2 = 0 then skip the rest of the code.
            }
        }

        int xt = 8; // the x coordinate on the sprite-sheet
        int yt = 13; // the y coordinate on the sprite-sheet

        screen.Render(X - 4, Y - 4 - 2, xt + (yt * 32), Color.Get(-1, 555, 555, 555), (MirrorFlags)Random.NextInt(4)); // renders the spark
        screen.Render(X - 4, Y - 4 + 2, xt + (yt * 32), Color.Get(-1, 000, 000, 000), (MirrorFlags)Random.NextInt(4)); // renders the shadow on the ground
    }
}
