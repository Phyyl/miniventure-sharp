namespace Miniventure.Levels.Tiles;

public record class SaplingTile : Tile
{
    private readonly Tile onType;
    private readonly Tile growsTo;

    public SaplingTile(byte id, Tile onType, Tile growsTo)
        : base(id, onType.ConnectsToGrass, onType.ConnectsToSand, onType.ConnectsToLava, onType.ConnectsToWater)
    {
        this.onType = onType;
        this.growsTo = growsTo;
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        onType.Render(screen, level, x, y);

        int col = Color.Get(10, 40, 50, -1);

        screen.Render(x * 16 + 4, y * 16 + 4, 11 + 3 * 32, col, 0);
    }

    public override void Update(Level level, int x, int y)
    {
        byte age = (byte)(level.GetData(x, y) + 1);

        if (age > 100)
        {
            level.SetTile(x, y, growsTo, 0);
        }
        else
        {
            level.SetData(x, y, age);
        }
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        level.SetTile(x, y, onType, 0);
    }
}
