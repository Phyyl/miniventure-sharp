using Miniventure.Items.Tools;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System.Drawing;
using Vildmark;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Textures;
using Vildmark.Helpers;
using Vildmark.Logging;
using Vildmark.Resources;
using Vildmark.Serialization;
using Font = Miniventure.Graphics.Font;

namespace Miniventure;

[WindowSettings(Border = OpenTK.Windowing.Common.WindowBorder.Fixed, Width = GameWidth * Scale, Height = GameHeight * Scale)]
public class Game : VildmarkGame<Game>
{
    public const string Name = "Miniventure";
    public const int GameHeight = 200;
    public const int GameWidth = 267;
    public const int Scale = 5;

    private Camera camera = CreateAutoOrthographicOffCenterCamera();
    private Texture2D texture;
    private Menu menu;

    private Pixels pixels;
    private Screen screen;
    private Pixels spriteSheet;
    private Screen lightScreen;

    private readonly int[] colors = new int[256];
    private int tickCount = 0;
    private bool showDebug;

    public GameState state;

    public InputHandler Input { get; private set; }

    public Menu Menu
    {
        get => menu;
        set
        {
            menu = value;
            menu?.Init(this, Input);
        }
    }

    protected override void Load()
    {
        Logger.EnableLogType(LogType.Debug);

        TypeHelper.RunStaticConstructor<Tile>();
        TypeHelper.RunStaticConstructor<Resource>();
        TypeHelper.RunStaticConstructor<ToolType>();

        texture = new Texture2D(GameWidth, GameHeight);
        Input = new InputHandler();
        spriteSheet = ResourceLoader.LoadEmbedded<Pixels>("icons.png");
        pixels = new Pixels(GameWidth, GameHeight);
        screen = new Screen(GameWidth, GameHeight, spriteSheet);
        lightScreen = new Screen(GameWidth, GameHeight, spriteSheet);

        InitColors();

        Menu = new TitleMenu();
    }

    protected override void Unload()
    {
        base.Unload();

        try
        {
            SaveGame();
        }
        catch (Exception ex)
        {
            Logger.Exception(ex);
        }
    }

    protected override bool ShouldClose()
    {
        return base.ShouldClose();
    }

    protected override void Update(float delta)
    {
        tickCount++;

#if !DEBUG
        if (!IsFocused)
        {
            Input.ReleaseAll();
            return;
        }
#endif

        if (state is not null && !state.player.Removed && !state.hasWon)
        {
            state.gameTime++;

            if (Input.Debug.Clicked)
            {
                showDebug = !showDebug;
            }
        }

        Input.Update();

        if (menu != null)
        {
            menu.Update();
        }
        else if (state != null)
        {
            if (state.player.Removed)
            {
                state.playerDeadTime++;

                if (state.playerDeadTime > 60)
                {
                    Menu = new DeadMenu();
                }
            }
            else
            {
                if (state.pendingLevelChange != 0)
                {
                    Menu = new LevelTransitionMenu(state.pendingLevelChange);
                    state.pendingLevelChange = 0;
                }
            }

            if (state.wonTimer > 0)
            {
                if (--state.wonTimer == 0)
                {
                    Menu = new WonMenu();
                }
            }

            state.level.Update();
            Tile.TickCount++;
        }
    }

    protected override void Render(float delta)
    {
        Render();
        texture.SetData(pixels.Width, pixels.Height, pixels.Data.AsSpan());

        Renderer.Begin(camera);
        Renderer.RenderRectangle(new Box2(0, 0, Size.X, Size.Y), texture);
        Renderer.End();
    }

    public void NewGame()
    {
        if (!LoadGame())
        {
            state = new();
            state.Generate();
        }
    }

    public bool LoadGame()
    {
        try
        {
            state = new();
            state.Deserialize(new Reader(File.OpenRead("save.dat")));

            return true;
        }
        catch (Exception ex)
        {
            Logger.Exception(ex);
            return false;
        }
    }

    public void SaveGame()
    {
        try
        {
            if (state is null)
            {
                return;
            }

            using Stream stream = File.Open("save.dat", FileMode.Create);
            state.Serialize(new Writer(stream));

            stream.Flush();
            stream.Close();
        }
        catch (Exception ex)
        {
            Logger.Exception(ex);
        }
    }

    public void Render()
    {
        if (state is not null)
        {
            int xScroll = state.player.X - screen.Width / 2;
            int yScroll = state.player.Y - (screen.Height - 8) / 2;

            if (xScroll < 16)
            {
                xScroll = 16;
            }

            if (yScroll < 16)
            {
                yScroll = 16;
            }

            if (xScroll > state.level.Width * 16 - screen.Width - 16)
            {
                xScroll = state.level.Width * 16 - screen.Width - 16;
            }

            if (yScroll > state.level.Height * 16 - screen.Height - 16)
            {
                yScroll = state.level.Height * 16 - screen.Height - 16;
            }

            if (state.currentLevel > 3)
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

            state.level.RenderBackground(screen, xScroll, yScroll);
            state.level.RenderSprites(screen, xScroll, yScroll);

            if (state.currentLevel < 3)
            {
                lightScreen.Clear(0);
                state.level.RenderLight(lightScreen, xScroll, yScroll);
                screen.Overlay(lightScreen, xScroll, yScroll);
            }
        }

        RenderGui();

#if !DEBUG
        if (!IsFocused)
        {
            RenderFocusNagger();
        }
#endif

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

        //Renderer.RenderRectangle(new Box2(0, 0, Width, Height), texture);
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

    private void RenderGui()
    {
        if (state is not null)
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
                if (i < state.player.Health)
                {
                    screen.Render(i * 8, screen.Height - 16, 0 + 12 * 32, Color.Get(000, 200, 500, 533), 0);
                }
                else
                {
                    screen.Render(i * 8, screen.Height - 16, 0 + 12 * 32, Color.Get(000, 100, 000, 000), 0);
                }

                if (state.player.staminaRechargeDelay > 0)
                {
                    if (state.player.staminaRechargeDelay / 4 % 2 == 0)
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
                    if (i < state.player.stamina)
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
                Font.Draw($"{state.player.X / 16},{state.player.Y / 16}", screen, 0, 0, Color.Get(444));
            }

            if (state.player.activeItem != null)
            {
                state.player.activeItem.RenderInventory(screen, 10 * 8, screen.Height - 16);
            }
        }

        menu?.Render(screen);
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

    public void ChangeLevel(int dir)
    {
        if (state is null)
        {
            return;
        }

        state.level.Remove(state.player);
        state.currentLevel += dir;
        state.player.X = (state.player.X >> 4) * 16 + 8;
        state.player.Y = (state.player.Y >> 4) * 16 + 8;
        state.level.Add(state.player);
    }

    public void ScheduleLevelChange(int dir)
    {
        if (state is null)
        {
            return;
        }

        state.pendingLevelChange = dir;
    }

    public virtual void Won()
    {
        if (state is null)
        {
            return;
        }

        state.wonTimer = 60 * 3;
        state.hasWon = true;
    }
}
