using Vildmark;

namespace Miniventure.Crafting;

public class FurnitureRecipe<TFurniture> : Recipe
    where TFurniture : Furniture, new()
{
    public FurnitureRecipe()
        : base(new FurnitureItem(new TFurniture()))
    {
    }

    public override void Craft(Player player)
    {
        try
        {
            player.inventory.Add(0, new FurnitureItem(new TFurniture()));
        }
        catch (Exception ex)
        {
            Logger.Exception(ex);
            throw;
        }
    }
}
