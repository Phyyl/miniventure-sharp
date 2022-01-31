using Vildmark.Serialization;

namespace Miniventure.Entities;

public class Inventory : ISerializable
{
    public List<Item> Items { get; } = new List<Item>();

    public virtual void Add(Item item)
    {
        Add(Items.Count, item);
    }

    public virtual void Add(int slot, Item item)
    {
        if (item is ResourceItem resourceItem)
        {
            ResourceItem existing = FindResource(resourceItem.Resource);

            if (existing == null)
            {
                Items.Insert(slot, resourceItem);
            }
            else
            {
                existing.Count += resourceItem.Count;
            }
        }
        else
        {
            Items.Insert(slot, item);
        }
    }

    private ResourceItem FindResource(Resource resource)
    {
        return Items.OfType<ResourceItem>().FirstOrDefault(item => item.Resource == resource);
    }

    public bool HasResources(Resource r, int count)
    {
        return FindResource(r)?.Count >= count;
    }

    public bool RemoveResource(Resource r, int count)
    {
        if (FindResource(r) is not ResourceItem item || item.Count < count)
        {
            return false;
        }

        item.Count -= count;

        if (item.Count <= 0)
        {
            Items.Remove(item);
        }

        return true;
    }

    public int Count(Item item)
    {
        if (item is ResourceItem resourceItem)
        {
            ResourceItem ri = FindResource(resourceItem.Resource);

            if (ri != null)
            {
                return ri.Count;
            }
        }
        else
        {
            int count = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Matches(item))
                {
                    count++;
                }
            }
            return count;
        }
        return 0;
    }

    public void Serialize(IWriter writer)
    {

    }

    public void Deserialize(IReader reader)
    {

    }
}
