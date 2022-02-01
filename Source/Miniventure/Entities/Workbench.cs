namespace Miniventure.Entities;

public class Workbench : CraftingStation
{
    public Workbench()
        : base("Workbench", 4, Color.Get(-1, 100, 321, 431), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override IEnumerable<Recipe> GetRecipes(Player player)
    {
        yield return new FurnitureRecipe<Lantern>(new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Slime, 10), new ResourceItem(Resource.Glass, 4));
        yield return new FurnitureRecipe<Oven>(new ResourceItem(Resource.Stone, 15));
        yield return new FurnitureRecipe<Furnace>(new ResourceItem(Resource.Stone, 20));
        yield return new FurnitureRecipe<Workbench>(new ResourceItem(Resource.Wood, 20));
        yield return new FurnitureRecipe<Chest>(new ResourceItem(Resource.Wood, 20));
        yield return new FurnitureRecipe<Anvil>(new ResourceItem(Resource.IronIngot, 5));
        yield return new ToolRecipe(ToolType.Sword, 0, new ResourceItem(Resource.Wood, 5));
        yield return new ToolRecipe(ToolType.Axe, 0, new ResourceItem(Resource.Wood, 5));
        yield return new ToolRecipe(ToolType.Hoe, 0, new ResourceItem(Resource.Wood, 5));
        yield return new ToolRecipe(ToolType.Pickaxe, 0, new ResourceItem(Resource.Wood, 5));
        yield return new ToolRecipe(ToolType.Shovel, 0, new ResourceItem(Resource.Wood, 5));
        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Stone, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Stone, 5));
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Stone, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Stone, 5));
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Stone, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Stone, 5));
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Stone, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Stone, 5));
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Stone, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Stone, 5));
        yield return new ItemRecipe<PowerGloveItem>(new ResourceItem(Resource.Cloth, 10), new ResourceItem(Resource.IronIngot, 5));
        yield return new ResourceRecipe(Resource.Sand, new ResourceItem(Resource.Dirt), new ResourceItem(Resource.Stone));
    }
}