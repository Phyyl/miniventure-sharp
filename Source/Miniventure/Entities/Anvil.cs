namespace Miniventure.Entities;

public class Anvil : CraftingStation
{
    public Anvil()
        : base("Anvil", 0, Color.Get(-1, 000, 111, 222), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override IEnumerable<Recipe> GetRecipes(Player player)
    {
        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5);
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5);
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5);
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5);
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5);

        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5);
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5);
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5);
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5);
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5);

        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50);
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50);
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50);
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50);
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50);
    }
}