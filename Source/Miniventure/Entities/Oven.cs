namespace Miniventure.Entities;

public class Oven : CraftingStation
{
    public Oven()
        : base("Oven", 2, Color.Get(-1, 000, 332, 442), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override IEnumerable<Recipe> GetRecipes(Player player)
    {
        yield return new ResourceRecipe(Resource.bread).AddCost(Resource.Wheat, 4);
    }
}