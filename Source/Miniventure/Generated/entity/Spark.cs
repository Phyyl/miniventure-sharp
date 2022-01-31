using Miniventure.Generated.gfx;
using Vildmark.Serialization;

namespace com.mojang.ld22.entity;

public class Spark : Entity
{
    private int lifeTime;
    private int time;
    private double xa, ya;
    private double xx, yy;

    public Spark(int x, int y, double xa, double ya)
        : base(x, y, 0, 0)
    {
        xx = x;
        yy = y;

        this.xa = xa;
        this.ya = ya;


        lifeTime = (60 * 10) + Random.NextInt(30);
    }

    private Spark()
        : this(0, 0, 0, 0)
    { }

    public override void Update()
    {
        time++;
        if (time >= lifeTime)
        {
            Remove();
            return;
        }
        xx += xa;
        yy += ya;
        X = (int)xx;
        Y = (int)yy;


        foreach (var entity in Level.GetEntities(X, Y, X, Y))
        {
            if (entity is Mob mob && entity is not AirWizard)
            {
                entity.Hurt(1, mob.Direction.GetOpposite());
            }
        }
    }


    public override bool IsBlockableBy(Mob mob)
    {
        return false;
    }


    public override void Render(Screen screen)
    {

        if (time >= lifeTime - (6 * 20))
        {
            if (time / 6 % 2 == 0)
            {
                return;
            }
        }

        int xt = 8;
        int yt = 13;

        screen.Render(X - 4, Y - 4 - 2, xt + (yt * 32), Color.Get(-1, 555, 555, 555), (MirrorFlags)Random.NextInt(4));
        screen.Render(X - 4, Y - 4 + 2, xt + (yt * 32), Color.Get(-1, 000, 000, 000), (MirrorFlags)Random.NextInt(4));
    }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteValue(lifeTime);
        writer.WriteValue(time);
        writer.WriteValue(xa);
        writer.WriteValue(ya);
        writer.WriteValue(xx);
        writer.WriteValue(yy);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        lifeTime = reader.ReadValue<int>();
        time = reader.ReadValue<int>();
        xa = reader.ReadValue<double>();
        ya = reader.ReadValue<double>();
        xx = reader.ReadValue<double>();
        yy = reader.ReadValue<double>();
    }
}
