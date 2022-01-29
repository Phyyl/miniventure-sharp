namespace com.mojang.ld22.entity.particle;


public abstract class Particle : Entity
{
    private int life;

    protected Particle(int x, int y, int life)
         : base(x, y)
    {
        this.life = life;
    }

    public override void Update()
    {
        base.Update();

        life--;

        if (life <= 0)
        {
            Remove();
        }
    }
}
