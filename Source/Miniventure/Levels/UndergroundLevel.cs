namespace Miniventure.Levels;

public class UndergroundLevel : Level
{
    public override int Depth { get; }

    public override int DirtColor => 222;
    public override int MonsterDensity => 4;

    protected override Tile TileAroundStairs => Tile.Dirt;

    public UndergroundLevel(int depth)
        : base(128, 128)
    {
        Depth = -depth;
    }

    private UndergroundLevel() : this(0) { }

    protected override void Generate(LevelData data)
    {
        LevelNoise mnoise1 = new(data.Width, Height, 16);
        LevelNoise mnoise2 = new(data.Width, Height, 16);
        LevelNoise mnoise3 = new(data.Width, Height, 16);

        LevelNoise nnoise1 = new(Width, Height, 16);
        LevelNoise nnoise2 = new(Width, Height, 16);
        LevelNoise nnoise3 = new(Width, Height, 16);

        LevelNoise wnoise1 = new(Width, Height, 16);
        LevelNoise wnoise2 = new(Width, Height, 16);
        LevelNoise wnoise3 = new(Width, Height, 16);

        LevelNoise noise1 = new(Width, Height, 32);
        LevelNoise noise2 = new(Width, Height, 32);

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                double val = Math.Abs(noise1[x, y] - noise2[x, y]) * 3 - 2;
                double mval = Math.Abs(mnoise1[x, y] - mnoise2[x, y]);

                mval = Math.Abs(mval - mnoise3[x, y]) * 3 - 2;

                double nval = Math.Abs(nnoise1[x, y] - nnoise2[x, y]);

                nval = Math.Abs(nval - nnoise3[x, y]) * 3 - 2;

                double wval = Math.Abs(wnoise1[x, y] - wnoise2[x, y]);

                wval = Math.Abs(nval - wnoise3[x, y]) * 3 - 2; // wval?

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

                if (val > -2 && wval < -2.0 + Depth / 2 * 3)
                {
                    if (Depth > 2)
                    {
                        data[x, y] = Tile.Lava;
                    }
                    else
                    {
                        data[x, y] = Tile.Water;
                    }
                }
                else if (val > -2 && (mval < -1.7 || nval < -1.4))
                {
                    data[x, y] = Tile.Dirt;
                }
                else
                {
                    data[x, y] = Tile.Rock;
                }
            }
        }

        {
            int r = 2;

            for (int i = 0; i < Width * Height / 400; i++)
            {
                int x = Random.NextInt(Width);
                int y = Random.NextInt(Height);

                for (int j = 0; j < 30; j++)
                {
                    int xx = x + Random.NextInt(5) - Random.NextInt(5);
                    int yy = y + Random.NextInt(5) - Random.NextInt(5);

                    if (xx >= r && yy >= r && xx < Width - r && yy < Height - r)
                    {
                        if (data[xx, yy].ID == Tile.Rock.ID)
                        {
                            data[xx, yy] = new LevelTile((byte)(Tile.IronOre.ID + Depth - 1));
                        }
                    }
                }
            }
        }

        if (Depth < 3)
        {
            int count = 0;

            for (int i = 0; i < Width * Height / 100; i++)
            {
                int x = Random.NextInt(Width - 20) + 10;
                int y = Random.NextInt(Height - 20) + 10;

                for (int yy = y - 1; yy <= y + 1; yy++)
                {
                    for (int xx = x - 1; xx <= x + 1; xx++)
                    {
                        if (data[xx, yy].ID != Tile.Rock.ID)
                        {
                            goto stairsLoop;
                        }
                    }
                }

                data[x, y] = Tile.StairsDown;
                count++;

                if (count == 4)
                {
                    break;
                }

stairsLoop:
                while (false) ;
            }
        }
    }

    protected override bool Validate(LevelData data)
    {
        int[] count = CountTiles(data);

        if (count[Tile.Rock.ID] < 100)
        {
            return false;
        }

        if (count[Tile.Dirt.ID] < 100)
        {
            return false;
        }

        if (count[Tile.IronOre.ID + Depth - 1] < 20)
        {
            return false;
        }

        if (Depth < 3)
        {
            if (count[Tile.StairsDown.ID] < 2)
            {
                return false;
            }
        }

        return true;
    }
}
