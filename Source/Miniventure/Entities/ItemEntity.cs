using Vildmark.Serialization;

namespace Miniventure.Entities;

public class ItemEntity : Entity
{
    private int lifeTime;
    private int time = 0;
    private double xa, ya, za;
    private double xx, yy, zz;

    public Item Item { get; private set; }

    public ItemEntity(Item item, int x, int y) : this(item, x, y, Random.NextGaussian() * 0.3, Random.NextGaussian() * 0.2, 60 * 10 + Random.NextInt(60)) { }

    public ItemEntity(Item item, int x, int y, double xa, double ya, int lifeTime)
        : base(x, y, 3, 3)
    {
        this.xa = xa;
        this.ya = ya;
        this.lifeTime = lifeTime;

        Item = item;
        xx = x;
        yy = y;

        zz = 2;

        za = Random.NextFloat() * 0.7 + 1;
    }

    private ItemEntity() : this(null, 0, 0) { }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteValue(lifeTime);
        writer.WriteValue(time);

        writer.WriteValue(xa);
        writer.WriteValue(ya);
        writer.WriteValue(za);

        writer.WriteValue(xx);
        writer.WriteValue(yy);
        writer.WriteValue(zz);

        writer.WriteObject(Item, true);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        lifeTime = reader.ReadValue<int>();
        time = reader.ReadValue<int>();

        xa = reader.ReadValue<double>();
        ya = reader.ReadValue<double>();
        za = reader.ReadValue<double>();

        xx = reader.ReadValue<double>();
        yy = reader.ReadValue<double>();
        zz = reader.ReadValue<double>();

        Item = reader.ReadObject<Item>(true);
    }

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
        zz += za;

        if (zz < 0)
        {
            zz = 0;
            za *= -0.5;
            xa *= 0.6;
            ya *= 0.6;
        }

        za -= 0.15;

        int ox = X;
        int oy = Y;
        int nx = (int)xx;
        int ny = (int)yy;
        int expectedx = nx - X;
        int expectedy = ny - Y;

        Move(nx - X, ny - Y);

        int gotx = X - ox;
        int goty = Y - oy;

        xx += gotx - expectedx;
        yy += goty - expectedy;
    }

    public override bool IsBlockableBy(Mob mob)
    {
        return false;
    }

    public override void Render(Screen screen)
    {
        if (time >= lifeTime - 6 * 20)
        {
            if (time / 6 % 2 == 0)
            {
                return;
            }
        }

        screen.Render(X - 4, Y - 4, Item.GetSprite(), Color.Get(-1, 0, 0, 0), 0);
        screen.Render(X - 4, Y - 4 - (int)zz, Item.GetSprite(), Item.GetColor(), 0);
    }

    public override void TouchedBy(Entity entity)
    {
        if (time > 30)
        {
            entity.TouchItem(this);
        }
    }

    public virtual void Take(Player player)
    {
        AudioTracks.Pickup.Play();
        player.score++;
        Item.OnTake(this);
        Remove();
    }
}
