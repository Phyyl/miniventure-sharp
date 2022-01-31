using Vildmark.Serialization;

namespace Miniventure.Levels;

public abstract class Level : ISerializable, IDeserializable
{
    protected Random Random { get; } = new();

    private readonly List<Entity> entities = new();
    private readonly List<List<Entity>> entitiesInTiles;

    protected virtual Tile TileAroundStairs => Tile.HardRock;

    public virtual int DirtColor => 322;
    public virtual int GrassColor => 141;
    public virtual int SandColor => 550;
    public virtual int MonsterDensity => 8;

    public int Width => Data.Width;
    public int Height => Data.Height;
    public LevelData Data { get; private set; }
    public Player Player { get; private set; }

    public abstract int Depth { get; }

    public Level(int width, int height)
    {
        Data = new LevelData(width, height);

        //TODO: yeah no
        entitiesInTiles = new(width * height);
        for (int i = 0; i < entitiesInTiles.Capacity; i++)
        {
            entitiesInTiles.Add(new List<Entity>());
        }
    }

    public virtual void Serialize(IWriter writer)
    {
        writer.WriteObject(Data);
        writer.WriteObjects(entities.ToArray(), true);
    }

    public virtual void Deserialize(IReader reader)
    {
        Data = reader.ReadObject<LevelData>();

        entities.Clear();

        foreach (var entity in reader.ReadObjects<Entity>(true))
        {
            Add(entity);
        }
    }

    public void Generate(Level parentLevel)
    {
        do
        {
            Generate(Data);
        } while (!Validate(Data));

        PlaceStairs(Data, parentLevel);

        entities.Clear();
        entities.AddRange(GenerateEntities());
    }

    private void PlaceStairs(LevelData data, Level parentLevel)
    {
        if (parentLevel is null)
        {
            return;
        }

        for (int y = 0; y < data.Height; y++)
        {
            for (int x = 0; x < data.Width; x++)
            {
                if (parentLevel.GetTile(x, y) != Tile.StairsDown)
                {
                    continue;
                }

                data[x, y] = new(Tile.StairsUp.ID, 0);

                Tile tile = TileAroundStairs;

                data[x - 1, y] = new(tile.ID, 0);
                data[x + 1, y] = new(tile.ID, 0);
                data[x, y - 1] = new(tile.ID, 0);
                data[x, y + 1] = new(tile.ID, 0);
                data[x - 1, y - 1] = new(tile.ID, 0);
                data[x - 1, y + 1] = new(tile.ID, 0);
                data[x + 1, y - 1] = new(tile.ID, 0);
                data[x + 1, y + 1] = new(tile.ID, 0);

            }
        }
    }

    protected abstract void Generate(LevelData levelData);

    protected abstract bool Validate(LevelData data);

    protected virtual IEnumerable<Entity> GenerateEntities() => Enumerable.Empty<Entity>();

    public virtual void RenderBackground(Screen screen, int xScroll, int yScroll)
    {
        int xo = xScroll >> 4;
        int yo = yScroll >> 4;
        int w = screen.Width + 15 >> 4;
        int h = screen.Height + 15 >> 4;

        screen.SetOffset(xScroll, yScroll);

        for (int y = yo; y <= h + yo; y++)
        {
            for (int x = xo; x <= w + xo; x++)
            {
                GetTile(x, y).Render(screen, this, x, y);
            }
        }

        screen.SetOffset(0, 0);
    }

    public virtual void RenderSprites(Screen screen, int xScroll, int yScroll)
    {
        int xo = xScroll >> 4;
        int yo = yScroll >> 4;
        int w = screen.Width + 15 >> 4;
        int h = screen.Height + 15 >> 4;

        screen.SetOffset(xScroll, yScroll);

        List<Entity> rowSprites = new();

        for (int y = yo; y <= h + yo; y++)
        {
            for (int x = xo; x <= w + xo; x++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                {
                    continue;
                }

                rowSprites.AddRange(entitiesInTiles[x + y * Width]);
            }

            if (rowSprites.Count > 0)
            {
                SortAndRender(screen, rowSprites);
            }

            rowSprites.Clear();
        }
        screen.SetOffset(0, 0);
    }

    public virtual void RenderLight(Screen screen, int xScroll, int yScroll)
    {
        int xo = xScroll >> 4;
        int yo = yScroll >> 4;
        int w = screen.Width + 15 >> 4;
        int h = screen.Height + 15 >> 4;

        screen.SetOffset(xScroll, yScroll);
        int r = 4;

        for (int y = yo - r; y <= h + yo + r; y++)
        {
            for (int x = xo - r; x <= w + xo + r; x++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                {
                    continue;
                }

                List<Entity> entities = entitiesInTiles[x + y * Width];

                foreach (var entity in entities)
                {
                    int elr = entity.GetLightRadius();

                    if (elr > 0)
                    {
                        screen.RenderLight(entity.X - 1, entity.Y - 4, elr * 8);
                    }
                }

                int lr = GetTile(x, y).GetLightRadius(this, x, y);

                if (lr > 0)
                {
                    screen.RenderLight(x * 16 + 8, y * 16 + 8, lr * 8);
                }
            }
        }

        screen.SetOffset(0, 0);
    }

    private void SortAndRender(Screen screen, List<Entity> list)
    {
        list.Sort((e0, e1) =>
        {
            if (e1.Y < e0.Y)
            {
                return 1;
            }

            if (e1.Y > e0.Y)
            {
                return -1;
            }

            return 0;
        });

        foreach (var item in list)
        {
            item.Render(screen);
        }
    }

    public virtual Tile GetTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return Tile.Rock;
        }

        return Tile.tiles[Data[x, y].ID];
    }

    public virtual void SetTile(int x, int y, Tile tile, byte tileData)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }

        Data[x, y] = new(tile.ID, tileData);
    }

    public virtual byte GetData(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return 0;
        }

        return Data[x, y].Data;
    }

    public virtual void SetData(int x, int y, byte tileData)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }

        Data[x, y] = Data[x, y] with { Data = tileData };
    }

    public virtual void Add(Entity entity)
    {
        if (entity is Player player)
        {
            Player = player;
        }

        entity.Removed = false;
        entities.Add(entity);
        entity.Level = this;

        InsertEntity(entity.X >> 4, entity.Y >> 4, entity);
    }

    public virtual void Remove(Entity e)
    {
        entities.Remove(e);

        int xto = e.X >> 4;
        int yto = e.Y >> 4;

        RemoveEntity(xto, yto, e);
    }

    private void InsertEntity(int x, int y, Entity e)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }

        entitiesInTiles[x + y * Width].Add(e);
    }

    private void RemoveEntity(int x, int y, Entity e)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }

        entitiesInTiles[x + y * Width].Remove(e);
    }

    public virtual void TrySpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Mob mob;

            int minLevel = 1;
            int maxLevel = 1;

            if (Depth < 0)
            {
                maxLevel = -Depth + 1;
            }
            if (Depth > 0)
            {
                minLevel = maxLevel = 4;
            }

            int lvl = Random.NextInt(maxLevel - minLevel + 1) + minLevel;
            if (Random.NextInt(2) == 0)
            {
                mob = new Slime(lvl);
            }
            else
            {
                mob = new Zombie(lvl);
            }

            if (mob.TrySpawn(this))
            {
                Add(mob);
            }
        }
    }

    public virtual void Update()
    {
        TrySpawn(1);

        for (int i = 0; i < Width * Height / 50; i++)
        {
            int xt = Random.NextInt(Width);
            int yt = Random.NextInt(Height);

            GetTile(xt, yt).Update(this, xt, yt);
        }

        for (int i = 0; i < entities.Count; i++)
        {
            Entity e = entities[i];

            int xto = e.X >> 4;
            int yto = e.Y >> 4;

            e.Update();

            if (e.Removed)
            {
                entities.RemoveAt(i--);
                RemoveEntity(xto, yto, e);
            }
            else
            {
                int xt = e.X >> 4;
                int yt = e.Y >> 4;

                if (xto != xt || yto != yt)
                {
                    RemoveEntity(xto, yto, e);
                    InsertEntity(xt, yt, e);
                }
            }
        }
    }

    public IEnumerable<Entity> GetEntities(int x0, int y0, int x1, int y1)
    {
        int xt0 = (x0 >> 4) - 1;
        int yt0 = (y0 >> 4) - 1;
        int xt1 = (x1 >> 4) + 1;
        int yt1 = (y1 >> 4) + 1;

        for (int y = yt0; y <= yt1; y++)
        {
            for (int x = xt0; x <= xt1; x++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                {
                    continue;
                }

                Entity[] entities = entitiesInTiles[x + y * Width].ToArray();

                foreach (var entity in entities)
                {
                    if (entity.Intersects(x0, y0, x1, y1))
                    {
                        yield return entity;
                    }
                }
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
}