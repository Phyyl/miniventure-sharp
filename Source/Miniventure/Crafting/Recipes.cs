using Vildmark;

namespace Miniventure.Crafting;

public class Recipes
{
    public static readonly Recipe[] AnvilRecipes;
    public static readonly Recipe[] OvenRecipes;
    public static readonly Recipe[] FurnaceRecipes;
    public static readonly Recipe[] WorkbenchRecipes;

    static Recipes()
    {
        try
        {
            WorkbenchRecipes = new Recipe[]
            {
                new FurnitureRecipe<Lantern>().AddCost(Resource.Wood, 5).AddCost(Resource.Slime, 10).AddCost(Resource.Glass, 4),
                new FurnitureRecipe<Oven>().AddCost(Resource.Stone, 15),
                new FurnitureRecipe<Furnace>().AddCost(Resource.Stone, 20),
                new FurnitureRecipe<Workbench>().AddCost(Resource.Wood, 20),
                new FurnitureRecipe<Chest>().AddCost(Resource.Wood, 20),
                new FurnitureRecipe<Anvil>().AddCost(Resource.IronIngot, 5),
                new ToolRecipe(ToolType.Sword, 0).AddCost(Resource.Wood, 5),
                new ToolRecipe(ToolType.Axe, 0).AddCost(Resource.Wood, 5),
                new ToolRecipe(ToolType.Hoe, 0).AddCost(Resource.Wood, 5),
                new ToolRecipe(ToolType.Pickaxe, 0).AddCost(Resource.Wood, 5),
                new ToolRecipe(ToolType.Shovel, 0).AddCost(Resource.Wood, 5),
                new ToolRecipe(ToolType.Sword, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5),
                new ToolRecipe(ToolType.Axe, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Stone).AddCost(Resource.Wood, 5).AddCost(Resource.Stone, 5),
                new ItemRecipe<PowerGloveItem>().AddCost(Resource.Cloth, 10).AddCost(Resource.IronIngot, 5)
            };

            AnvilRecipes = new Recipe[]
            {
                new ToolRecipe(ToolType.Sword, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5),
                new ToolRecipe(ToolType.Axe, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Iron).AddCost(Resource.Wood, 5).AddCost(Resource.IronIngot, 5),

                new ToolRecipe(ToolType.Sword, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5),
                new ToolRecipe(ToolType.Axe, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Gold).AddCost(Resource.Wood, 5).AddCost(Resource.GoldIngot, 5),

                new ToolRecipe(ToolType.Sword, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50),
                new ToolRecipe(ToolType.Axe, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Gem).AddCost(Resource.Wood, 5).AddCost(Resource.Gem, 50),
            };

            FurnaceRecipes = new Recipe[]
            {
                new ResourceRecipe(Resource.IronIngot).AddCost(Resource.IronOre, 4).AddCost(Resource.Coal, 1),
                new ResourceRecipe(Resource.GoldIngot).AddCost(Resource.GoldOre, 4).AddCost(Resource.Coal, 1),
                new ResourceRecipe(Resource.Glass).AddCost(Resource.Sand, 4).AddCost(Resource.Coal, 1),
                new ResourceRecipe(Resource.Coal).AddCost(Resource.Wood, 10)
            };

            OvenRecipes = new Recipe[]
            {
                new ResourceRecipe(Resource.bread).AddCost(Resource.Wheat, 4),
            };
        }
        catch (Exception ex)
        {
            Logger.Exception(ex);
            throw;
        }
    }
}
