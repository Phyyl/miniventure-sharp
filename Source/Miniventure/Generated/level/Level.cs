namespace com.mojang.ld22.level;

public class Level
{
    private Random random = new Random();

    public int Width, Height;

    public byte[] tiles;
    public byte[] data;

    //TODO: Either convert to Dictionary or array
    public List<List<Entity>> entitiesInTiles;

    public int grassColor = 141;
    public int dirtColor = 322;
    public int sandColor = 550;
    private int depth;
    public int monsterDensity = 8;

    public List<Entity> entities = new List<Entity>();

    public Level(int w, int h, int level, Level parentLevel)
    {
        if (level < 0)
        {
            dirtColor = 222;
        }
        depth = level;
        Width = w;
        Height = h;
        byte[][] maps;

        if (level == 0)
        {
            maps = LevelGen.createAndValidateTopMap(w, h);
        }
        else if (level < 0)
        {
            maps = LevelGen.createAndValidateUndergroundMap(w, h, -level);
            monsterDensity = 4;
        }
        else
        {
            maps = LevelGen.createAndValidateSkyMap(w, h);
            monsterDensity = 4;
        }

        tiles = maps[0];
        data = maps[1];

        if (parentLevel != null)
        {
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (parentLevel.GetTile(x, y) == Tile.stairsDown)
                    {

                        SetTile(x, y, Tile.stairsUp, 0);

                        Tile tile = Tile.dirt;
                        if (level == 0)
                        {
                            tile = Tile.hardRock;
                        }

                        SetTile(x - 1, y, tile, 0);
                        SetTile(x + 1, y, tile, 0);
                        SetTile(x, y - 1, tile, 0);
                        SetTile(x, y + 1, tile, 0);
                        SetTile(x - 1, y - 1, tile, 0);
                        SetTile(x - 1, y + 1, tile, 0);
                        SetTile(x + 1, y - 1, tile, 0);
                        SetTile(x + 1, y + 1, tile, 0);
                    }

                }
            }
        }

        entitiesInTiles = new List<List<Entity>>(w * h);
        for (int i = 0; i < w * h; i++)
        {
            entitiesInTiles.Add(new List<Entity>());
        }

        if (level == 1)
        {
            AirWizard aw = new AirWizard();
            aw.X = w * 8;
            aw.Y = h * 8;
            Add(aw);
        }
    }

    public virtual void renderBackground(Screen screen, int xScroll, int yScroll)
    {
        int xo = xScroll >> 4;
        int yo = yScroll >> 4;
        int w = (screen.Width + 15) >> 4;
        int h = (screen.Height + 15) >> 4;
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

    private List<Entity> rowSprites = new List<Entity>();

    public Player player;

    public virtual void RenderSprites(Screen screen, int xScroll, int yScroll)
    {
        int xo = xScroll >> 4;
        int yo = yScroll >> 4;
        int w = (screen.Width + 15) >> 4;
        int h = (screen.Height + 15) >> 4;

        screen.SetOffset(xScroll, yScroll);

        for (int y = yo; y <= h + yo; y++)
        {
            for (int x = xo; x <= w + xo; x++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                {
                    continue;
                }

                rowSprites.AddRange(entitiesInTiles[x + (y * Width)]);
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
        int w = (screen.Width + 15) >> 4;
        int h = (screen.Height + 15) >> 4;

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

                List<Entity> entities = entitiesInTiles[x + (y * Width)];

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
                    screen.RenderLight((x * 16) + 8, (y * 16) + 8, lr * 8);
                }
            }
        }

        screen.SetOffset(0, 0);
    }

    private void SortAndRender(Screen screen, List<Entity> list)
    {
        list.Sort((Entity e0, Entity e1) =>
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
            return Tile.rock;
        }

        return Tile.tiles[tiles[x + (y * Width)]];
    }

    public virtual void SetTile(int x, int y, Tile t, int dataVal)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }

        tiles[x + (y * Width)] = t.id;
        data[x + (y * Width)] = (byte)dataVal;
    }

    public virtual int GetData(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return 0;
        }

        return data[x + (y * Width)] & 0xff;
    }

    public virtual void SetData(int x, int y, int val)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }

        data[x + (y * Width)] = (byte)val;
    }

    public virtual void Add(Entity entity)
    {
        if (entity is Player)
        {
            player = (Player)entity;
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

        entitiesInTiles[x + (y * Width)].Add(e);
    }

    private void RemoveEntity(int x, int y, Entity e)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }

        entitiesInTiles[x + (y * Width)].Remove(e);
    }

    public virtual void TrySpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Mob mob;

            int minLevel = 1;
            int maxLevel = 1;
            if (depth < 0)
            {
                maxLevel = (-depth) + 1;
            }
            if (depth > 0)
            {
                minLevel = maxLevel = 4;
            }

            int lvl = random.NextInt(maxLevel - minLevel + 1) + minLevel;
            if (random.NextInt(2) == 0)
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
            int xt = random.NextInt(Width);
            int yt = random.NextInt(Height);

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

                List<Entity> entities = entitiesInTiles[x + (y * Width)];

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
}