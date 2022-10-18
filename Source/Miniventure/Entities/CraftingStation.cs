namespace Miniventure.Entities;

public abstract class CraftingStation : Furniture
{
    public CraftingStation(string name, int sprite, int col, int x = 0, int y = 0, int horizontalRadius = 3, int verticalRadius = 3)
        : base(name, sprite, col, x, y, horizontalRadius, verticalRadius)
    {
    }

    public abstract IEnumerable<Recipe> GetRecipes(Player player);

    public override bool Use(Player player, Direction attackDir)
    {
        Game.Instance.Menu = new CraftingMenu(GetRecipes(player).ToArray(), player);

        return true;
    }
}
