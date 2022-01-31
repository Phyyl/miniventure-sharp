namespace com.mojang.ld22.crafting;

public class ResourceRecipe : Recipe
{
    private readonly Resource resource;

    public ResourceRecipe(Resource resource)
        : base(new ResourceItem(resource, 1))
    {
        this.resource = resource;
    }

    public override void Craft(Player player)
    {
        player.inventory.Add(0, new ResourceItem(resource, 1));
    }
}
