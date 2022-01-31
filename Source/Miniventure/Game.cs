using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using Vildmark;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;
using Vildmark.Serialization;
using Vildmark.Windowing;
using Font = Miniventure.Graphics.Font;

namespace Miniventure;

public class Game : VildmarkGame
{
    public const string Name = "Miniventure";
    public const int GameHeight = 200;
    public const int GameWidth = 267;
    public const int Scale = 3;

    private RenderContext renderContext;
    private GLTexture2D texture;
    private Menu menu;

    private Pixels pixels;
    private Screen screen;
    private Pixels spriteSheet;
    private Screen lightScreen;
    private InputHandler input;

    private readonly int[] colors = new int[256];
    private int tickCount = 0;
    private bool showDebug;

    public int gameTime = 0;

    private Level level;
    private Level[] levels = new Level[5];
    private int currentLevel = 3;
    public Player player;

    private int playerDeadTime;
    private int pendingLevelChange;
    private int wonTimer = 0;
    public bool hasWon = false;

    public Menu Menu
    {
        get => menu;
        set
        {
            menu = value;
            menu?.Init(this, input);
        }
    }

    public void ResetGame()
    {
        playerDeadTime = 0;
        wonTimer = 0;
        gameTime = 0;
        hasWon = false;

        levels = new Level[5];
        currentLevel = 3;

        levels[4] = new Level(new FileLevelProvider("level4.dat", new SkyLevelGenerationProvider(128, 128)));
        levels[3] = new Level(new FileLevelProvider("level3.dat", new TopLevelGenerationProvider(128, 128, levels[4])));
        levels[2] = new Level(new FileLevelProvider("level2.dat", new UndergroundLevelGenerationProvider(128, 128, -1, levels[3])));
        levels[1] = new Level(new FileLevelProvider("level1.dat", new UndergroundLevelGenerationProvider(128, 128, -2, levels[2])));
        levels[0] = new Level(new FileLevelProvider("level0.dat", new UndergroundLevelGenerationProvider(128, 128, -3, levels[1])));

        level = levels[currentLevel];
        player = new Player(this, input);
        player.TrySpawn(level);

        level.Add(player);

        for (int i = 0; i < 5; i++)
        {
            levels[i].TrySpawn(5000);
        }
    }

    private void InitColors()
    {
        int pp = 0;
        for (int r = 0; r < 6; r++)
        {
            for (int g = 0; g < 6; g++)
            {
                for (int b = 0; b < 6; b++)
                {
                    int rr = r * 255 / 5;
                    int gg = g * 255 / 5;
                    int bb = b * 255 / 5;
                    int mid = (rr * 30 + gg * 59 + bb * 11) / 100;

                    int r1 = (rr + mid * 1) / 2 * 230 / 255 + 10;
                    int g1 = (gg + mid * 1) / 2 * 230 / 255 + 10;
                    int b1 = (bb + mid * 1) / 2 * 230 / 255 + 10;
                    colors[pp++] = r1 << 16 | g1 << 8 | b1;

                }
            }
        }
    }

    public void ChangeLevel(int dir)
    {
        level.Remove(player);
        currentLevel += dir;
        level = levels[currentLevel];
        player.X = (player.X >> 4) * 16 + 8;
        player.Y = (player.Y >> 4) * 16 + 8;
        level.Add(player);
    }

    public void Render()
    {
        int xScroll = player.X - screen.Width / 2;
        int yScroll = player.Y - (screen.Height - 8) / 2;

        if (xScroll < 16)
        {
            xScroll = 16;
        }

        if (yScroll < 16)
        {
            yScroll = 16;
        }

        if (xScroll > level.Width * 16 - screen.Width - 16)
        {
            xScroll = level.Width * 16 - screen.Width - 16;
        }

        if (yScroll > level.Height * 16 - screen.Height - 16)
        {
            yScroll = level.Height * 16 - screen.Height - 16;
        }

        if (currentLevel > 3)
        {
            int col = Color.Get(20, 20, 121, 121);
            for (int y = 0; y < 14; y++)
            {
                for (int x = 0; x < 24; x++)
                {
                    screen.Render(x * 8 - (xScroll / 4 & 7), y * 8 - (yScroll / 4 & 7), 0, col, 0);
                }
            }
        }

        level.RenderBackground(screen, xScroll, yScroll);
        level.RenderSprites(screen, xScroll, yScroll);


        if (currentLevel < 3)
        {
            lightScreen.Clear(0);
            level.RenderLight(lightScreen, xScroll, yScroll);
            screen.Overlay(lightScreen, xScroll, yScroll);
        }

        RenderGui();

        if (!Window.IsFocused)
        {
            RenderFocusNagger();
        }

        for (int y = 0; y < screen.Height; y++)
        {
            for (int x = 0; x < screen.Width; x++)
            {

                int cc = screen.Pixels[x + y * screen.Width];
                if (cc < 255)
                {
                    pixels[x + y * GameWidth] = colors[cc];
                }
            }
        }

        renderContext.RenderRectangle(new RectangleF(0, 0, Window.Width, Window.Height), texture);
    }

    private void RenderGui()
    {
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 20; x++)
            {

                screen.Render(x * 8, screen.Height - 16 + y * 8, 0 + 12 * 32, Color.Get(000, 000, 000, 000), 0);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (i < player.Health)
            {
                screen.Render(i * 8, screen.Height - 16, 0 + 12 * 32, Color.Get(000, 200, 500, 533), 0);
            }
            else
            {
                screen.Render(i * 8, screen.Height - 16, 0 + 12 * 32, Color.Get(000, 100, 000, 000), 0);
            }

            if (player.staminaRechargeDelay > 0)
            {
                if (player.staminaRechargeDelay / 4 % 2 == 0)
                {
                    screen.Render(i * 8, screen.Height - 8, 1 + 12 * 32, Color.Get(000, 555, 000, 000), 0);
                }
                else
                {
                    screen.Render(i * 8, screen.Height - 8, 1 + 12 * 32, Color.Get(000, 110, 000, 000), 0);
                }
            }
            else
            {
                if (i < player.stamina)
                {
                    screen.Render(i * 8, screen.Height - 8, 1 + 12 * 32, Color.Get(000, 220, 550, 553), 0);
                }
                else
                {
                    screen.Render(i * 8, screen.Height - 8, 1 + 12 * 32, Color.Get(000, 110, 000, 000), 0);
                }
            }
        }

        if (showDebug)
        {
            Font.Draw($"{player.X / 16},{player.Y / 16}", screen, 0, 0, Color.Get(444));
        }

        if (player.activeItem != null)
        {
            player.activeItem.RenderInventory(screen, 10 * 8, screen.Height - 16);
        }

        if (menu != null)
        {
            menu.Render(screen);
        }
    }

    private void RenderFocusNagger()
    {
        string msg = "Click to focus!";
        int xx = (GameWidth - msg.Length * 8) / 2;
        int yy = (GameHeight - 8) / 2;
        int w = msg.Length;
        int h = 1;

        screen.Render(xx - 8, yy - 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.None);
        screen.Render(xx + w * 8, yy - 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Horizontal);
        screen.Render(xx - 8, yy + 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Vertical);
        screen.Render(xx + w * 8, yy + 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Both);

        for (int x = 0; x < w; x++)
        {
            screen.Render(xx + x * 8, yy - 8, 1 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.None);
            screen.Render(xx + x * 8, yy + 8, 1 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Vertical);
        }

        for (int y = 0; y < h; y++)
        {
            screen.Render(xx - 8, yy + y * 8, 2 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.None);
            screen.Render(xx + w * 8, yy + y * 8, 2 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Horizontal);
        }

        if (tickCount / 20 % 2 == 0)
        {
            Font.Draw(msg, screen, xx, yy, Color.Get(5, 333, 333, 333));
        }
        else
        {
            Font.Draw(msg, screen, xx, yy, Color.Get(5, 555, 555, 555));
        }
    }

    public void ScheduleLevelChange(int dir)
    {
        pendingLevelChange = dir;
    }

    public override void Load()
    {
        renderContext = Create2DRenderContext();
        texture = new GLTexture2D(GameWidth, GameHeight, pixelInternalFormat: PixelInternalFormat.Rgb, magFilter: TextureMagFilter.Nearest);
        input = new InputHandler(Keyboard);
        spriteSheet = ResourceLoader.LoadEmbedded<Pixels>("icons.png");
        pixels = new Pixels(GameWidth, GameHeight);
        screen = new Screen(GameWidth, GameHeight, spriteSheet);
        lightScreen = new Screen(GameWidth, GameHeight, spriteSheet);

        InitColors();
        ResetGame();

        Menu = new TitleMenu();
    }
    public override void Update(float delta)
    {
        tickCount++;

        if (!player.Removed && !hasWon)
        {
            gameTime++;
        }

        input.Update();

        if (input.Debug.Clicked)
        {
            showDebug = !showDebug;
        }

        if (menu != null)
        {
            menu.Update();
        }
        else
        {
            if (player.Removed)
            {
                playerDeadTime++;

                if (playerDeadTime > 60)
                {
                    Menu = new DeadMenu();
                }
            }
            else
            {
                if (pendingLevelChange != 0)
                {
                    Menu = new LevelTransitionMenu(pendingLevelChange);
                    pendingLevelChange = 0;
                }
            }
            if (wonTimer > 0)
            {
                if (--wonTimer == 0)
                {
                    Menu = new WonMenu();
                }
            }

            level.Update();
            Tile.TickCount++;
        }
    }

    public override void Render(float delta)
    {
        Render();
        texture.SetData(pixels.Width, pixels.Height, pixels.Data.AsSpan());

        renderContext.Begin();
        renderContext.RenderRectangle(new RectangleF(0, 0, Window.Size.X, Window.Size.Y), texture);
        renderContext.End();
    }

    protected override void InitializeWindowSettings(WindowSettings settings)
    {
        base.InitializeWindowSettings(settings);

        settings.Border = OpenTK.Windowing.Common.WindowBorder.Fixed;
        settings.Width = GameWidth * Scale;
        settings.Height = GameHeight * Scale;
    }

    public virtual void won()
    {
        wonTimer = 60 * 3;
        hasWon = true;
    }
}

public class FileLevelProvider : ILevelProvider
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Depth { get; private set; }
    public int DirtColor { get; private set; }
    public int GrassColor { get; private set; }
    public int SandColor { get; private set; }
    public int MonsterDensity { get; private set; }

    private readonly LevelData data;
    private readonly Entity[] entities;

    public FileLevelProvider(string path, LevelGenerationProvider generationProvider)
    {
        if (!File.Exists(path))
        {
            Width = generationProvider.Width;
            Height = generationProvider.Height;
            Depth = generationProvider.Depth;
            DirtColor = generationProvider.DirtColor;
            GrassColor = generationProvider.GrassColor;
            SandColor = generationProvider.SandColor;
            MonsterDensity = generationProvider.MonsterDensity;
            data = generationProvider.GetLevelData();
            entities = generationProvider.GetEntities().ToArray();
        }
        else
        {

        }
    }

    public IEnumerable<Entity> GetEntities()
    {
        return entities;
    }

    public LevelData GetLevelData()
    {
        return data;
    }
}