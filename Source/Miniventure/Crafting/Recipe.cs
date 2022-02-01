using System.Collections.Immutable;

namespace Miniventure.Crafting;

public abstract class Recipe
{
    public ImmutableArray<ResourceItem> Costs { get; }
    public Item ResultTemplate { get; }

    public Recipe(Item resultTemplate, ResourceItem[] costs)
    {
        Costs = costs.ToImmutableArray();
        ResultTemplate = resultTemplate;
    }

    public abstract Item CreateItem();

    public virtual bool CanCraft(Inventory inventory)
    {
        foreach (var item in Costs)
        {
            if (!inventory.HasResources(item.Resource, item.Count))
            {
                return false;
            }
        }

        return true;
    }

    public bool Craft(Inventory inventory)
    {
        if (!CanCraft(inventory))
        {
            return false;
        }

        foreach (var item in Costs)
        {
            inventory.RemoveResource(item.Resource, item.Count);
        }

        inventory.Add(0, CreateItem());

        return true;
    }
}