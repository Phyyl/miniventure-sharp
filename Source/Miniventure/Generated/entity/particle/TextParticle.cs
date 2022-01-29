namespace com.mojang.ld22.entity.particle;


public class TextParticle : Particle
{
    private readonly string msg;
    private readonly int col;
    private double xa, ya, za;
    private double xx, yy, zz;

    public TextParticle(string msg, int x, int y, int col)
        : base(x, y, 60)
    {
        this.msg = msg;
        this.col = col;

        xx = x;
        yy = y;
        zz = 2;

        xa = Random.NextGaussian() * 0.3;
        ya = Random.NextGaussian() * 0.2;
        za = (Random.NextFloat() * 0.7) + 2;
    }

    public override void Update()
    {
        base.Update();

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

        X = (int)xx;
        Y = (int)yy;
    }

    public override void Render(Screen screen)
    {
        Font.Draw(msg, screen, X - (msg.Length * 4) + 1, Y - (int)zz + 1, Color.Get(-1, 0, 0, 0));
        Font.Draw(msg, screen, X - (msg.Length * 4), Y - (int)zz, col);
    }
}
