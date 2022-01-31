using Miniventure.Entities;
using Vildmark.Serialization;

namespace Miniventure.Entities.Particles;


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

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteValue(life);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        life = reader.ReadValue<int>();
    }
}
