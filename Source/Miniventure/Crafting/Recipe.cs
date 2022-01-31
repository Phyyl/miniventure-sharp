using Miniventure.Entities;
using Miniventure.Items;

namespace Miniventure.Crafting;

//TODO: Make immutable
public abstract class Recipe
{
    public List<Item> costs = new();
    public Item resultTemplate;

    public Recipe(Item resultTemplate)
    {
        this.resultTemplate = resultTemplate;
    }

    public abstract void Craft(Player player);

    public virtual Recipe AddCost(Resource resource, int count)
    {
        costs.Add(new ResourceItem(resource, count));
        return this;
    }

    public virtual bool CanCraft(Inventory inventory)
    {
        foreach (var item in costs.OfType<ResourceItem>())
        {
            if (!inventory.HasResources(item.Resource, item.Count))
            {
                return false;
            }
        }

        return true;
    }

    public virtual void DeductCost(Inventory inventory)
    {
        foreach (var item in costs.OfType<ResourceItem>())
        {
            inventory.RemoveResource(item.Resource, item.Count);
        }
    }
}