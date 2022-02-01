namespace Miniventure.Crafting;

public class ResourceRecipe : Recipe
{
    public Resource Resource { get; }

    public ResourceRecipe(Resource resource, params ResourceItem[] costs)
        : base(new ResourceItem(resource, 1), costs)
    {
        Resource = resource;
    }

    public override Item CreateItem()
    {
        return new ResourceItem(Resource, 1);
    }
}
