namespace Miniventure.Crafting;

public class ItemRecipe<TItem> : Recipe
    where TItem : Item, new()
{
    public ItemRecipe(params ResourceItem[] costs) 
        : base(new TItem(), costs)
    {
    }

    public override Item CreateItem()
    {
        return new TItem();
    }
}
