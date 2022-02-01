namespace Miniventure.Entities;

public class Furnace : CraftingStation
{
    public Furnace()
        : base("Furnace", 3, Color.Get(-1, 000, 222, 333), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override IEnumerable<Recipe> GetRecipes(Player player)
    {
        yield return new ResourceRecipe(Resource.IronIngot, new ResourceItem(Resource.IronOre, 4), new ResourceItem(Resource.Coal, 1));
        yield return new ResourceRecipe(Resource.GoldIngot, new ResourceItem(Resource.GoldOre, 4), new ResourceItem(Resource.Coal, 1));
        yield return new ResourceRecipe(Resource.Glass, new ResourceItem(Resource.Sand, 4), new ResourceItem(Resource.Coal, 1));
        yield return new ResourceRecipe(Resource.Coal, new ResourceItem(Resource.Wood, 10));
    }
}