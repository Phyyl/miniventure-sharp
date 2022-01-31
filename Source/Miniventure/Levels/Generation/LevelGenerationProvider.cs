using Miniventure.Entities;
using Miniventure.Levels;
using Miniventure.Levels.Tiles;

namespace Miniventure.Levels.Generation;

public abstract class LevelGenerationProvider : ILevelProvider
{
    protected static Random Random { get; } = new();

    private readonly Level parentLevel;

    protected virtual Tile TileAroundStairs => Tile.hardRock;

    public virtual int DirtColor => 322;
    public virtual int GrassColor => 141;
    public virtual int SandColor => 550;
    public virtual int MonsterDensity => 8;

    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }

    protected LevelGenerationProvider(int width, int height, int depth, Level parentLevel)
    {
        Width = width;
        Height = height;
        Depth = depth;

        this.parentLevel = parentLevel;
    }

    public LevelData GetLevelData()
    {
        LevelData data;

        do
        {
            data = new LevelData(Width, Height);
            Generate(data);
        } while (!Validate(data));

        PlaceStairs(data);

        return data;
    }

    public virtual IEnumerable<Entity> GetEntities()
    {
        return Enumerable.Empty<Entity>();
    }

    protected abstract void Generate(LevelData data);

    protected abstract bool Validate(LevelData data);

    private void PlaceStairs(LevelData data)
    {
        if (parentLevel is null)
        {
            return;
        }

        for (int y = 0; y < data.Height; y++)
        {
            for (int x = 0; x < data.Width; x++)
            {
                if (parentLevel.GetTile(x, y) != Tile.stairsDown)
                {
                    continue;
                }

                data[x, y] = new(Tile.stairsUp.id, 0);

                Tile tile = TileAroundStairs;

                data[x - 1, y] = new(tile.id, 0);
                data[x + 1, y] = new(tile.id, 0);
                data[x, y - 1] = new(tile.id, 0);
                data[x, y + 1] = new(tile.id, 0);
                data[x - 1, y - 1] = new(tile.id, 0);
                data[x - 1, y + 1] = new(tile.id, 0);
                data[x + 1, y - 1] = new(tile.id, 0);
                data[x + 1, y + 1] = new(tile.id, 0);

            }
        }
    }

    protected static int[] CountTiles(LevelData data)
    {
        int[] count = new int[byte.MaxValue];

        for (int y = 0; y < data.Height; y++)
        {
            for (int x = 0; x < data.Width; x++)
            {
                count[data[x, y].ID]++;
            }
        }

        return count;
    }

    public LevelData GetLevelData(Level parentLeven)
    {
        throw new NotImplementedException();
    }
}
