using Miniventure.Levels.Tiles;

namespace Miniventure.Levels.Generation;

public class TopLevelGenerationProvider : LevelGenerationProvider
{
    public TopLevelGenerationProvider(int width, int height, Level parentLevel)
        : base(width, height, 0, parentLevel)
    {
    }

    protected override void Generate(LevelData data)
    {
        LevelNoise mnoise1 = new(Width, Height, 16);
        LevelNoise mnoise2 = new(Width, Height, 16);
        LevelNoise mnoise3 = new(Width, Height, 16);

        LevelNoise noise1 = new(Width, Height, 32);
        LevelNoise noise2 = new(Width, Height, 32);

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                double val = Math.Abs(noise1[x, y] - noise2[x, y]) * 3 - 2;
                double mval = Math.Abs(mnoise1[x, y] - mnoise2[x, y]);

                mval = Math.Abs(mval - mnoise3[x, y]) * 3 - 2;

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
                val = val + 1 - dist * 20;

                if (val < -0.5)
                {
                    data[x, y] = Tile.water;
                }
                else if (val > 0.5 && mval < -1.5)
                {
                    data[x, y] = Tile.rock;
                }
                else
                {
                    data[x, y] = Tile.grass;
                }
            }
        }

        for (int i = 0; i < Width * Height / 2800; i++)
        {
            int xs = Random.NextInt(Width);
            int ys = Random.NextInt(Height);

            for (int k = 0; k < 10; k++)
            {
                int x = xs + Random.NextInt(21) - 10;
                int y = ys + Random.NextInt(21) - 10;

                for (int j = 0; j < 100; j++)
                {
                    int xo = x + Random.NextInt(5) - Random.NextInt(5);
                    int yo = y + Random.NextInt(5) - Random.NextInt(5);

                    for (int yy = yo - 1; yy <= yo + 1; yy++)
                    {
                        for (int xx = xo - 1; xx <= xo + 1; xx++)
                        {
                            if (xx >= 0 && yy >= 0 && xx < Width && yy < Height)
                            {
                                if (data[xx, yy].ID == Tile.grass.id)
                                {
                                    data[xx, yy] = Tile.sand;
                                }
                            }
                        }
                    }
                }
            }
        }

        for (int i = 0; i < Width * Height / 400; i++)
        {
            int x = Random.NextInt(Width);
            int y = Random.NextInt(Height);

            for (int j = 0; j < 200; j++)
            {
                int xx = x + Random.NextInt(15) - Random.NextInt(15);
                int yy = y + Random.NextInt(15) - Random.NextInt(15);

                if (xx >= 0 && yy >= 0 && xx < Width && yy < Height)
                {
                    if (data[xx, yy].ID == Tile.grass.id)
                    {
                        data[xx, yy] = Tile.tree;
                    }
                }
            }
        }

        for (int i = 0; i < Width * Height / 400; i++)
        {
            int x = Random.NextInt(Width);
            int y = Random.NextInt(Height);
            int col = Random.NextInt(4);

            for (int j = 0; j < 30; j++)
            {
                int xx = x + Random.NextInt(5) - Random.NextInt(5);
                int yy = y + Random.NextInt(5) - Random.NextInt(5);

                if (xx >= 0 && yy >= 0 && xx < Width && yy < Height)
                {
                    if (data[xx, yy].ID == Tile.grass.id)
                    {
                        data[xx, yy] = new(Tile.flower.id, (byte)(col + Random.NextInt(4) * 16));
                    }
                }
            }
        }

        for (int i = 0; i < Width * Height / 100; i++)
        {
            int xx = Random.NextInt(Width);
            int yy = Random.NextInt(Height);

            if (xx >= 0 && yy >= 0 && xx < Width && yy < Height)
            {
                if (data[xx, yy].ID == Tile.sand.id)
                {
                    data[xx, yy] = Tile.cactus;
                }
            }
        }

        int count = 0;

        for (int i = 0; i < Width * Height / 100; i++)
        {
            int x = Random.NextInt(Width - 2) + 1;
            int y = Random.NextInt(Height - 2) + 1;

            for (int yy = y - 1; yy <= y + 1; yy++)
            {
                for (int xx = x - 1; xx <= x + 1; xx++)
                {
                    if (data[xx, yy].ID != Tile.rock.id)
                    {
                        goto stairsLoop;
                    }
                }
            }

            data[x, y] = Tile.stairsDown;
            count++;

            if (count == 4)
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

        if (count[Tile.rock.id] < 100)
        {
            return false;
        }

        if (count[Tile.sand.id] < 100)
        {
            return false;
        }

        if (count[Tile.grass.id] < 100)
        {
            return false;
        }

        if (count[Tile.tree.id] < 100)
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
