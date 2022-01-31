using Miniventure.Graphics;

namespace Miniventure.UI;



public class Menu
{
    public Game game;
    public InputHandler input;

    public virtual void Init(Game game, InputHandler input)
    {
        this.input = input;
        this.game = game;
    }

    public virtual void Update()
    {
    }

    public virtual void Render(Screen screen)
    {
    }

    public static void RenderItemList<T>(Screen screen, int xo, int yo, int x1, int y1, T[] items, int selected)
        where T : IListItem
    {
        bool renderCursor = true;
        if (selected < 0)
        {
            selected = -selected - 1;
            renderCursor = false;
        }
        int w = x1 - xo;
        int h = y1 - yo - 1;
        int i0 = 0;
        int i1 = items.Length;
        if (i1 > h)
        {
            i1 = h;
        }

        int io = selected - h / 2;

        if (io > items.Length - h)
        {
            io = items.Length - h;
        }

        if (io < 0)
        {
            io = 0;
        }

        for (int i = i0; i < i1; i++)
        {
            items[i + io].RenderInventory(screen, (1 + xo) * 8, (i + 1 + yo) * 8);
        }

        if (renderCursor)
        {
            int yy = selected + 1 - io + yo;
            Font.Draw(">", screen, (xo + 0) * 8, yy * 8, Color.Get(5, 555, 555, 555));
            Font.Draw("<", screen, (xo + w) * 8, yy * 8, Color.Get(5, 555, 555, 555));
        }
    }
}
