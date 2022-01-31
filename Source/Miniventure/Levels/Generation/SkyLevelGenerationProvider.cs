using Miniventure.Entities;
using Miniventure.Levels.Tiles;

namespace Miniventure.Levels.Generation;

public class SkyLevelGenerationProvider : LevelGenerationProvider
{
    public override int MonsterDensity => 4;

    public SkyLevelGenerationProvider(int width, int height, Level parentLevel = null)
        : base(width, height, 1, parentLevel)
    {
    }

    public override IEnumerable<Entity> GetEntities()
    {
        yield return new AirWizard(Width * 8, Height * 8);
    }

    protected override void Generate(LevelData data)
    {
        LevelNoise noise1 = new(Width, Height, 8);
        LevelNoise noise2 = new(Width, Height, 8);

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                double val = Math.Abs(noise1[x, y] - noise2[x, y]) * 3 - 2;
                double xd = x / (Width - 1.0) * 2 - 1;
                double yd = y / (Height - 1.0) * 2 - 1;

                if (xd < 0)
                {
                    xd = -xd;
                }

                if (yd < 0)
                {
                    yd = -yd;
                }

                double dist = xd >= yd ? xd : yd;

                dist = dist * dist * dist * dist;
                dist = dist * dist * dist * dist;
                val = -val * 1 - 2.2;
                val = val + 1 - dist * 20;

                if (val < -0.25)
                {
                    data[x, y] = Tile.infiniteFall;
                }
                else
                {
                    data[x, y] = Tile.cloud;
                }
            }
        }


        for (int i = 0; i < Width * Height / 50; i++)
        {
            int x = Random.NextInt(Width - 2) + 1;
            int y = Random.NextInt(Height - 2) + 1;

            for (int yy = y - 1; yy <= y + 1; yy++)
            {
                for (int xx = x - 1; xx <= x + 1; xx++)
                {
                    if (data[xx, yy].ID != Tile.cloud.id)
                    {
                        goto cactusLoop;
                    }
                }
            }

            data[x, y] = Tile.cloudCactus;

        cactusLoop:
            while (false) ;
        }

        int count = 0;

        for (int i = 0; i < Width * Height; i++)
        {
            int x = Random.NextInt(Width - 2) + 1;
            int y = Random.NextInt(Height - 2) + 1;

            for (int yy = y - 1; yy <= y + 1; yy++)
            {
                for (int xx = x - 1; xx <= x + 1; xx++)
                {
                    if (data[xx, yy].ID != Tile.cloud.id)
                    {
                        goto stairsLoop;
                    }
                }
            }

            data[x, y] = Tile.stairsDown;
            count++;

            if (count == 2)
            {
                break;
            }

        stairsLoop:
            while (false) ;
        }
    }

    protected override bool Validate(LevelData data)
    {
        int[] count = CountTiles(data);

        if (count[Tile.cloud.id] < 2000)
        {
            return false;
        }

        if (count[Tile.stairsDown.id] < 2)
        {
            return false;
        }

        return true;
    }
}
