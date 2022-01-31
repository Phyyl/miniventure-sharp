namespace Miniventure.Levels.Tiles;

public record class InfiniteFallTile : Tile
{
    public InfiniteFallTile(byte id)
        : base(id)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
    }

    public override void Update(Level level, int xt, int yt)
    {
    }

    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return e is AirWizard;
    }
}
