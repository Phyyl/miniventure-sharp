namespace Miniventure.Entities;

public class Workbench : CraftingStation
{
    public Workbench()
        : base("Workbench", 4, Color.Get(-1, 100, 321, 431), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override IEnumerable<Recipe> GetRecipes(Player player)
    {
        yield return new FurnitureRecipe<Lantern>().AddCost(Resource.Wood, 5).AddCost(Resource.Slime, 10).AddCost(Resource.Glass, 4);
        yield return new FurnitureRecipe<Oven>().AddCost(Resource.Stone, 15);
        yield return new FurnitureRecipe<Furnace>().AddCost(Resource.Stone, 20);
        yield return new FurnitureRecipe<Workbench>().AddCost(Resource.Wood, 20);
        yield return new FurnitureRecipe<Chest>().AddCost(Resource.Wood, 20);
        yield return new FurnitureRecipe<Anvil>().AddCost(Resource.IronIngot, 5);
        yield return new ToolRecipe(ToolType.Sword, 0).AddCost(Resource.Wood, 5);
        yield return new ToolRecipe(ToolType.Axe, 0).AddCost(Resource.Wood, 5);
        yield return new ToolRecipe(ToolType.Hoe, 0).AddCost(Resource.Wood, 5);
        yield return new ToolRecipe(ToolType.Pickaxe, 0).AddCost(Resource.Wood, 5);
        yield return new ToolRecipe(ToolType.Shovel, 0).AddCost(Resource.Wood, 5);
        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5);
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5);
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5);
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5);
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5);
        yield return new ItemRecipe<PowerGloveItem>().AddCost(Resource.Cloth, 10).AddCost(Resource.IronIngot, 5);
    }
}