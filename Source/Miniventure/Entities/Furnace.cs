namespace Miniventure.Entities;

public class Furnace : CraftingStation
{
    public Furnace()
        : base("Furnace", 3, Color.Get(-1, 000, 222, 333), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override IEnumerable<Recipe> GetRecipes(Player player)
    {
        yield return new ResourceRecipe(Resource.IronIngot).AddCost(Resource.IronOre, 4).AddCost(Resource.Coal, 1);
        yield return new ResourceRecipe(Resource.GoldIngot).AddCost(Resource.GoldOre, 4).AddCost(Resource.Coal, 1);
        yield return new ResourceRecipe(Resource.Glass).AddCost(Resource.Sand, 4).AddCost(Resource.Coal, 1);
        yield return new ResourceRecipe(Resource.Coal).AddCost(Resource.Wood, 10);
    }
}