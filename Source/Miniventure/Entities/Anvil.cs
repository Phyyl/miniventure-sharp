using Miniventure.Items.Tools;

namespace Miniventure.Entities;

public class Anvil : CraftingStation
{
    public Anvil()
        : base("Anvil", 0, Color.Get(-1, 000, 111, 222), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override IEnumerable<Recipe> GetRecipes(Player player)
    {
        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Iron, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.IronIngot, 5));
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Iron, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.IronIngot, 5));
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Iron, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.IronIngot, 5));
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Iron, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.IronIngot, 5));
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Iron, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.IronIngot, 5));

        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Gold, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.GoldIngot, 5));
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Gold, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.GoldIngot, 5));
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Gold, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.GoldIngot, 5));
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gold, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.GoldIngot, 5));
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Gold, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.GoldIngot, 5));

        yield return new ToolRecipe(ToolType.Sword, ToolLevel.Gem, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Gem, 50));
        yield return new ToolRecipe(ToolType.Axe, ToolLevel.Gem, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Gem, 50));
        yield return new ToolRecipe(ToolType.Hoe, ToolLevel.Gem, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Gem, 50));
        yield return new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gem, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Gem, 50));
        yield return new ToolRecipe(ToolType.Shovel, ToolLevel.Gem, new ResourceItem(Resource.Wood, 5), new ResourceItem(Resource.Gem, 50));
    }
}