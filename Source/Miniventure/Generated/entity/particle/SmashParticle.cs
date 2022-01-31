using Miniventure.Generated.gfx;

namespace com.mojang.ld22.entity.particle;

public class SmashParticle : Particle
{
    public SmashParticle(int x, int y)
        : base(x, y, 10)
    {
        X = x;
        Y = y;

        Sound.monsterHurt.Play();
    }

    private SmashParticle() : this(0, 0) { }

    public override void Render(Screen screen)
    {
        int col = Color.Get(-1, 555, 555, 555);
        screen.Render(X - 8, Y - 8, 5 + (12 * 32), col, MirrorFlags.Vertical);
        screen.Render(X - 0, Y - 8, 5 + (12 * 32), col, MirrorFlags.Both);
        screen.Render(X - 8, Y - 0, 5 + (12 * 32), col, MirrorFlags.None);
        screen.Render(X - 0, Y - 0, 5 + (12 * 32), col, MirrorFlags.Horizontal);
    }
}
