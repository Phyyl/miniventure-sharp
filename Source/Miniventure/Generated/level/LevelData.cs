using System.Runtime.InteropServices;
using Vildmark.Serialization;

namespace com.mojang.ld22.level;

public class LevelData : ISerializable
{
    private LevelTile[,] tiles;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public LevelTile this[int x, int y]
    {
        get
        {
            if (!IsValidPosition(x, y))
            {
                return default;
            }

            return tiles[x, y];
        }
        set
        {
            if (!IsValidPosition(x, y))
            {
                return;
            }

            tiles[x, y] = value;
        }
    }

    public LevelData()
        : this(128, 128)
    {
    }

    public LevelData(int width, int height)
    {
        tiles = new LevelTile[width, height];

        Width = width;
        Height = height;
    }

    private bool IsValidPosition(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

    public void Serialize(IWriter writer)
    {
        writer.WriteValue(Width);
        writer.WriteValue(Height);
        writer.WriteValues(tiles);
    }

    public void Deserialize(IReader reader)
    {
        Width = reader.ReadValue<int>();
        Height = reader.ReadValue<int>();
        tiles = reader.Read2DValues<LevelTile>();
    }
}
