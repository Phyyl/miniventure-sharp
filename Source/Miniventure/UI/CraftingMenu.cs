using System.Collections.Immutable;

namespace Miniventure.UI;

public class CraftingMenu : Menu
{
    private readonly Player player;
    private int selected = 0;

    private readonly InventoryRecipe[] recipes;

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
            screen.Render(x, y, Recipe.ResultTemplate.GetSprite(), Recipe.ResultTemplate.GetColor(), 0);

            int textColor = Recipe.CanCraft(Inventory) ? Color.Get(-1, 555, 555, 555) : Color.Get(-1, 222, 222, 222);

            Font.Draw(Recipe.ResultTemplate.GetName(), screen, x + 8, y, textColor);
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
        if (input.Menu.Clicked)
        {
            game.Menu = null;
        }

        if (input.Up.Clicked)
        {
            selected--;
        }

        if (input.Down.Clicked)
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

        if (input.Attack.Clicked && len > 0)
        {
            Recipe r = recipes[selected].Recipe;

            if (r.Craft(player.inventory))
            {
                AudioTracks.Craft.Play();
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
            int hasResultItems = player.inventory.Count(recipe.ResultTemplate);
            int xo = 13 * 8;

            screen.Render(xo, 2 * 8, recipe.ResultTemplate.GetSprite(), recipe.ResultTemplate.GetColor(), 0);
            
            Font.Draw("" + hasResultItems, screen, xo + 8, 2 * 8, Color.Get(-1, 555, 555, 555));

            ImmutableArray<ResourceItem> costs = recipe.Costs;

            for (int i = 0; i < costs.Length; i++)
            {
                ResourceItem item = costs[i];

                int yo = (5 + i) * 8;

                screen.Render(xo, yo, item.GetSprite(), item.GetColor(), 0);

                int requiredAmt = 1;

                if (item is ResourceItem item1)
                {
                    requiredAmt = item1.Count;
                }

                int has = player.inventory.Count(item);
                int color = Color.Get(-1, 555, 555, 555);

                if (has < requiredAmt)
                {
                    color = Color.Get(-1, 222, 222, 222);
                }

                if (has > 99)
                {
                    has = 99;
                }

                Font.Draw("" + has + "/" + requiredAmt, screen, xo + 8, yo, color);
            }
        }

    }
}