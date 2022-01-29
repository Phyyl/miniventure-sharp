namespace com.mojang.ld22.screen;

public class CraftingMenu : Menu
{
    private Player player;
    private int selected = 0;

    private InventoryRecipe[] recipes;

    private class InventoryRecipe : IListItem
    {
        public Inventory Inventory { get; }
        public Recipe Recipe { get; }

        public bool CanCraft => Recipe.CanCraft(Inventory);

        public InventoryRecipe(Inventory inventory, Recipe recipe)
        {
            Inventory = inventory;
            Recipe = recipe;
        }

        public void RenderInventory(Screen screen, int x, int y)
        {
            screen.Render(x, y, Recipe.resultTemplate.GetSprite(), Recipe.resultTemplate.GetColor(), 0);

            int textColor = Recipe.CanCraft(Inventory) ? Color.Get(-1, 555, 555, 555) : Color.Get(-1, 222, 222, 222);

            Font.Draw(Recipe.resultTemplate.GetName(), screen, x + 8, y, textColor);
        }
    }

    public CraftingMenu(Recipe[] recipes, Player player)
    {
        this.recipes = recipes.Select(r => new InventoryRecipe(player.inventory, r)).ToArray();
        this.player = player;

        Array.Sort(this.recipes, (r1, r2) =>
        {
            if (r1.CanCraft && !r2.CanCraft)
            {
                return -1;
            }

            if (!r1.CanCraft && r2.CanCraft)
            {
                return 1;
            }

            return 0;
        });
    }

    public override void Update()
    {
        if (input.menu.clicked)
        {
            game.Menu = null;
        }

        if (input.up.clicked)
        {
            selected--;
        }

        if (input.down.clicked)
        {
            selected++;
        }

        int len = recipes.Length;

        if (len == 0)
        {
            selected = 0;
        }

        if (selected < 0)
        {
            selected += len;
        }

        if (selected >= len)
        {
            selected -= len;
        }

        if (input.attack.clicked && len > 0)
        {
            Recipe r = recipes[selected].Recipe;

            //TODO: TryCraft maybe
            if (r.CanCraft(player.inventory))
            {
                r.DeductCost(player.inventory);
                r.Craft(player);
                Sound.craft.Play();
            }
        }
    }

    public override void Render(Screen screen)
    {
        Font.RenderFrame(screen, "Have", 12, 1, 19, 3);
        Font.RenderFrame(screen, "Cost", 12, 4, 19, 11);
        Font.RenderFrame(screen, "Crafting", 0, 1, 11, 11);

        RenderItemList(screen, 0, 1, 11, 11, recipes.ToArray(), selected);

        if (recipes.Length > 0)
        {
            Recipe recipe = recipes[selected].Recipe;
            int hasResultItems = player.inventory.Count(recipe.resultTemplate);
            int xo = 13 * 8;
            screen.Render(xo, 2 * 8, recipe.resultTemplate.GetSprite(), recipe.resultTemplate.GetColor(), 0); // Renders the sprites in the 'have' & 'cost' windows
            Font.Draw("" + hasResultItems, screen, xo + 8, 2 * 8, Color.Get(-1, 555, 555, 555)); // draws the amount in the 'have' menu

            List<Item> costs = recipe.costs; // the list items that is needed to make the recipe
            for (int i = 0; i < costs.Count; i++)
            { // Loops through the costs list
                Item item = costs[i]; // Current cost item
                int yo = (5 + i) * 8; // y coordinate of the cost item
                screen.Render(xo, yo, item.GetSprite(), item.GetColor(), 0); // renders the cost item
                int requiredAmt = 1; // required amount need to craft (normally 1)
                if (item is ResourceItem)
                { // If the item is a params resource[]
                    requiredAmt = ((ResourceItem)item).Count; // get's the amount needed to craft the item
                }
                int has = player.inventory.Count(item); // This is the amount of the resource you have in your inventory
                int color = Color.Get(-1, 555, 555, 555); // color in the 'cost' window
                if (has < requiredAmt)
                { // If the player has less than required of the resource
                    color = Color.Get(-1, 222, 222, 222); // then change the color to gray.
                }
                if (has > 99)
                {
                    has = 99; // if the player has over 99 of the resource, then just display 99 (for space)
                }

                Font.Draw("" + requiredAmt + "/" + has, screen, xo + 8, yo, color); // Draw "#required/#has" text next to the icon 
            }
        }

    }
}