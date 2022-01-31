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
                new FurnitureRecipe<Lantern>().AddCost(Resource.wood, 5).AddCost(Resource.slime, 10).AddCost(Resource.glass, 4),
                new FurnitureRecipe<Oven>().AddCost(Resource.stone, 15),
                new FurnitureRecipe<Furnace>().AddCost(Resource.stone, 20),
                new FurnitureRecipe<Workbench>().AddCost(Resource.wood, 20),
                new FurnitureRecipe<Chest>().AddCost(Resource.wood, 20),
                new FurnitureRecipe<Anvil>().AddCost(Resource.ironIngot, 5),
                new ToolRecipe(ToolType.Sword, 0).AddCost(Resource.wood, 5),
                new ToolRecipe(ToolType.Axe, 0).AddCost(Resource.wood, 5),
                new ToolRecipe(ToolType.Hoe, 0).AddCost(Resource.wood, 5),
                new ToolRecipe(ToolType.Pickaxe, 0).AddCost(Resource.wood, 5),
                new ToolRecipe(ToolType.Shovel, 0).AddCost(Resource.wood, 5),
                new ToolRecipe(ToolType.Sword, ToolLevel.Stone).AddCost(Resource.wood, 5).AddCost(Resource.stone, 5),
                new ToolRecipe(ToolType.Axe, ToolLevel.Stone).AddCost(Resource.wood, 5).AddCost(Resource.stone, 5),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Stone).AddCost(Resource.wood, 5).AddCost(Resource.stone, 5),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Stone).AddCost(Resource.wood, 5).AddCost(Resource.stone, 5),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Stone).AddCost(Resource.wood, 5).AddCost(Resource.stone, 5),
            };

            AnvilRecipes = new Recipe[]
            {
                new ToolRecipe(ToolType.Sword, ToolLevel.Iron).AddCost(Resource.wood, 5).AddCost(Resource.ironIngot, 5),
                new ToolRecipe(ToolType.Axe, ToolLevel.Iron).AddCost(Resource.wood, 5).AddCost(Resource.ironIngot, 5),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Iron).AddCost(Resource.wood, 5).AddCost(Resource.ironIngot, 5),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Iron).AddCost(Resource.wood, 5).AddCost(Resource.ironIngot, 5),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Iron).AddCost(Resource.wood, 5).AddCost(Resource.ironIngot, 5),

                new ToolRecipe(ToolType.Sword, ToolLevel.Gold).AddCost(Resource.wood, 5).AddCost(Resource.goldIngot, 5),
                new ToolRecipe(ToolType.Axe, ToolLevel.Gold).AddCost(Resource.wood, 5).AddCost(Resource.goldIngot, 5),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Gold).AddCost(Resource.wood, 5).AddCost(Resource.goldIngot, 5),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gold).AddCost(Resource.wood, 5).AddCost(Resource.goldIngot, 5),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Gold).AddCost(Resource.wood, 5).AddCost(Resource.goldIngot, 5),

                new ToolRecipe(ToolType.Sword, ToolLevel.Gem).AddCost(Resource.wood, 5).AddCost(Resource.gem, 50),
                new ToolRecipe(ToolType.Axe, ToolLevel.Gem).AddCost(Resource.wood, 5).AddCost(Resource.gem, 50),
                new ToolRecipe(ToolType.Hoe, ToolLevel.Gem).AddCost(Resource.wood, 5).AddCost(Resource.gem, 50),
                new ToolRecipe(ToolType.Pickaxe, ToolLevel.Gem).AddCost(Resource.wood, 5).AddCost(Resource.gem, 50),
                new ToolRecipe(ToolType.Shovel, ToolLevel.Gem).AddCost(Resource.wood, 5).AddCost(Resource.gem, 50),
            };

            FurnaceRecipes = new Recipe[]
            {
                new ResourceRecipe(Resource.ironIngot).AddCost(Resource.ironOre, 4).AddCost(Resource.coal, 1),
                new ResourceRecipe(Resource.goldIngot).AddCost(Resource.goldOre, 4).AddCost(Resource.coal, 1),
                new ResourceRecipe(Resource.glass).AddCost(Resource.sand, 4).AddCost(Resource.coal, 1),
                new ResourceRecipe(Resource.coal).AddCost(Resource.wood, 10)
            };

            OvenRecipes = new Recipe[]
            {
                new ResourceRecipe(Resource.bread).AddCost(Resource.wheat, 4),
            };
        }
        catch (Exception ex)
        {
            Logger.Exception(ex);
            throw;
        }
    }
}
