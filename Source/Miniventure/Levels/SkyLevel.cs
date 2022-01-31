namespace Miniventure.Levels;

public class SkyLevel : Level
{
    public override int MonsterDensity => 4;
    public override int Depth => 1;

    public SkyLevel() : base(128, 128) { }

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
                    data[x, y] = Tile.InfiniteFall;
                }
                else
                {
                    data[x, y] = Tile.Cloud;
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
                     if (data[xx, yy].ID != Tile.Cloud.ID)
                    {
                        goto cactusLoop;
                    }
                }
            }

            data[x, y] = Tile.CloudCactus;

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
                    if (data[xx, yy].ID != Tile.Cloud.ID)
                    {
                        goto stairsLoop;
                    }
                }
            }

            data[x, y] = Tile.StairsDown;
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

        if (count[Tile.Cloud.ID] < 2000)
        {
            return false;
        }

        if (count[Tile.StairsDown.ID] < 2)
        {
            return false;
        }

        return true;
    }

    protected override IEnumerable<Entity> GenerateEntities()
    {
        yield return new AirWizard(Width * 8, Height * 8);
    }
}
