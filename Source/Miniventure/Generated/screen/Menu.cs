namespace com.mojang.ld22.screen;



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

    public void RenderItemList<T>(Screen screen, int xo, int yo, int x1, int y1, T[] items, int selected)
        where T : IListItem
    {
        bool renderCursor = true;// Renders the ">" "<" arrows between a name.
        if (selected < 0)
        {
            selected = -selected - 1; // If the selected is smaller than 0, then it will add one.
            renderCursor = false; // doesn't render the arrows between the name
        }
        int w = x1 - xo; // Width variable determined by given values
        int h = y1 - yo - 1; // Height variable determined by given values
        int i0 = 0; // Beginning variable of the list loop
        int i1 = items.Count(); // End variable of the list loop
        if (i1 > h)
        {
            i1 = h; // If the end variable is larger than the height variable, then end variable will equal height variable.
        }

        int io = selected - (h / 2); // Middle of the list, (selected item). For scrolling effect

        if (io > items.Count() - h)
        {
            io = items.Count() - h; //if the middle is coming near the bottom, then the selected will change.
        }

        if (io < 0)
        {
            io = 0; // if the middle is coming near the top, then the selected will change
        }

        for (int i = i0; i < i1; i++)
        {
            items[i + io].RenderInventory(screen, (1 + xo) * 8, (i + 1 + yo) * 8); // this will render all the items in the inventory
        }

        if (renderCursor)
        {
            int yy = selected + 1 - io + yo; // the y position of the currently selected item
            Font.Draw(">", screen, (xo + 0) * 8, yy * 8, Color.Get(5, 555, 555, 555)); // draws the left cursor next to the name
            Font.Draw("<", screen, (xo + w) * 8, yy * 8, Color.Get(5, 555, 555, 555)); // draws the right cursor next to the name
        }
    }
}
