namespace com.mojang.ld22.crafting;



public class Crafting {
	public static readonly List<Recipe> anvilRecipes = new List<Recipe>(); // A list that contains all the recipes for the anvil
	public static readonly List<Recipe> ovenRecipes = new List<Recipe>(); // A list that contains all the recipes for the oven
	public static readonly List<Recipe> furnaceRecipes = new List<Recipe>(); // A list that contains all the recipes for the furnace
	public static readonly List<Recipe> workbenchRecipes = new List<Recipe>(); // A list that contains all the recipes for the workbench

	static Crafting() {
		try {
			/*
			 * workbenchRecipes.add() adds a new recipe that has to be crafted in the workbench
			 * anvilRecipes.add(), furnaceRecipes.add(), ovenRecipes.add() does the same, but has to be crafted in anvil/furnace/oven
			 * (new FurnitureRecipe(typeof(Lantern))) makes a new furniture recipe for the lantern class
			 * addCost(Resource.wood, 5) adds a material to the recipe. The name after "Resource." is what material and the number is how many needed
			 */
			
			
			workbenchRecipes.add(new FurnitureRecipe(typeof(Lantern)).addCost(Resource.wood, 5).addCost(Resource.slime, 10).addCost(Resource.glass, 4));
			workbenchRecipes.add(new FurnitureRecipe(typeof(Oven)).addCost(Resource.stone, 15));
			workbenchRecipes.add(new FurnitureRecipe(typeof(Furnace)).addCost(Resource.stone, 20));
			workbenchRecipes.add(new FurnitureRecipe(typeof(Workbench)).addCost(Resource.wood, 20));
			workbenchRecipes.add(new FurnitureRecipe(typeof(Chest)).addCost(Resource.wood, 20));
			workbenchRecipes.add(new FurnitureRecipe(typeof(Anvil)).addCost(Resource.ironIngot, 5));

			workbenchRecipes.add(new ToolRecipe(ToolType.sword, 0).addCost(Resource.wood, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.axe, 0).addCost(Resource.wood, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.hoe, 0).addCost(Resource.wood, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.pickaxe, 0).addCost(Resource.wood, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.shovel, 0).addCost(Resource.wood, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.sword, 1).addCost(Resource.wood, 5).addCost(Resource.stone, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.axe, 1).addCost(Resource.wood, 5).addCost(Resource.stone, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.hoe, 1).addCost(Resource.wood, 5).addCost(Resource.stone, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.pickaxe, 1).addCost(Resource.wood, 5).addCost(Resource.stone, 5));
			workbenchRecipes.add(new ToolRecipe(ToolType.shovel, 1).addCost(Resource.wood, 5).addCost(Resource.stone, 5));

			anvilRecipes.add(new ToolRecipe(ToolType.sword, 2).addCost(Resource.wood, 5).addCost(Resource.ironIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.axe, 2).addCost(Resource.wood, 5).addCost(Resource.ironIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.hoe, 2).addCost(Resource.wood, 5).addCost(Resource.ironIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.pickaxe, 2).addCost(Resource.wood, 5).addCost(Resource.ironIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.shovel, 2).addCost(Resource.wood, 5).addCost(Resource.ironIngot, 5));

			anvilRecipes.add(new ToolRecipe(ToolType.sword, 3).addCost(Resource.wood, 5).addCost(Resource.goldIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.axe, 3).addCost(Resource.wood, 5).addCost(Resource.goldIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.hoe, 3).addCost(Resource.wood, 5).addCost(Resource.goldIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.pickaxe, 3).addCost(Resource.wood, 5).addCost(Resource.goldIngot, 5));
			anvilRecipes.add(new ToolRecipe(ToolType.shovel, 3).addCost(Resource.wood, 5).addCost(Resource.goldIngot, 5));

			anvilRecipes.add(new ToolRecipe(ToolType.sword, 4).addCost(Resource.wood, 5).addCost(Resource.gem, 50));
			anvilRecipes.add(new ToolRecipe(ToolType.axe, 4).addCost(Resource.wood, 5).addCost(Resource.gem, 50));
			anvilRecipes.add(new ToolRecipe(ToolType.hoe, 4).addCost(Resource.wood, 5).addCost(Resource.gem, 50));
			anvilRecipes.add(new ToolRecipe(ToolType.pickaxe, 4).addCost(Resource.wood, 5).addCost(Resource.gem, 50));
			anvilRecipes.add(new ToolRecipe(ToolType.shovel, 4).addCost(Resource.wood, 5).addCost(Resource.gem, 50));

			furnaceRecipes.add(new ResourceRecipe(Resource.ironIngot).addCost(Resource.ironOre, 4).addCost(Resource.coal, 1));
			furnaceRecipes.add(new ResourceRecipe(Resource.goldIngot).addCost(Resource.goldOre, 4).addCost(Resource.coal, 1));
			furnaceRecipes.add(new ResourceRecipe(Resource.glass).addCost(Resource.sand, 4).addCost(Resource.coal, 1));

			ovenRecipes.add(new ResourceRecipe(Resource.bread).addCost(Resource.wheat, 4));
		} catch (Exception e) {
			throw;
		}
	}
}
