namespace Miniventure.Crafting;

public class FurnitureRecipe<TFurniture> : Recipe
    where TFurniture : Furniture, new()
{
    public FurnitureRecipe(params ResourceItem[] costs)
        : base(new FurnitureItem(new TFurniture()), costs)
    {
    }

    public override Item CreateItem()
    {
        return new FurnitureItem(new TFurniture());
    }
}
