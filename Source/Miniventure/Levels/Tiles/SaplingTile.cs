using Miniventure.Entities;
using Miniventure.Graphics;

namespace Miniventure.Levels.Tiles;


public class SaplingTile : Tile
{
    private Tile onType;
    private Tile growsTo;

    public SaplingTile(int id, Tile onType, Tile growsTo) : base(id)
    {
        this.onType = onType;
        this.growsTo = growsTo;
        connectsToSand = onType.connectsToSand;
        connectsToGrass = onType.connectsToGrass;
        connectsToWater = onType.connectsToWater;
        connectsToLava = onType.connectsToLava;
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