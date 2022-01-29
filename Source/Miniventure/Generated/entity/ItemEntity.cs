namespace com.mojang.ld22.entity;


public class ItemEntity : Entity
{
    private readonly int lifeTime;
    public double xa, ya, za;
    public double xx, yy, zz;
    public Item item;
    private int time = 0;

    public ItemEntity(Item item, int x, int y)
        : base(x, y, 3, 3)
    {
        this.item = item;
        xx = x;
        yy = y;

        zz = 2;

        xa = Random.NextGaussian() * 0.3;
        ya = Random.NextGaussian() * 0.2;
        za = (Random.NextFloat() * 0.7) + 1;

        lifeTime = (60 * 10) + Random.NextInt(60);
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
        if (time >= lifeTime - (6 * 20))
        { 
            if (time / 6 % 2 == 0)
            {
                return; 
            }
        }

        screen.Render(X - 4, Y - 4, item.GetSprite(), Color.Get(-1, 0, 0, 0), 0);
        screen.Render(X - 4, Y - 4 - (int)zz, item.GetSprite(), item.GetColor(), 0);
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
        Sound.pickup.Play();
        player.score++;
        item.OnTake(this);
        Remove();
    }
}
