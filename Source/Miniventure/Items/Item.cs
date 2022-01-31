using Vildmark.Serialization;

namespace Miniventure.Items;


public class Item : IListItem, ISerializable, IDeserializable
{
    public virtual int GetColor()
    {
        return 0;
    }

    public virtual int GetSprite()
    {
        return 0;
    }

    public virtual void OnTake(ItemEntity itemEntity)
    {
    }

    public virtual void RenderInventory(Screen screen, int x, int y)
    {
    }

    public virtual bool Interact(Player player, Entity entity, Direction attackDir)
    {
        return false;
    }

    public virtual void RenderIcon(Screen screen, int x, int y)
    {
    }

    public virtual bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        return false;
    }

    public virtual bool IsDepleted()
    {
        return false;
    }

    public virtual bool CanAttack()
    {
        return false;
    }

    public virtual int GetAttackDamageBonus(Entity e)
    {
        return 0;
    }

    public virtual string GetName()
    {
        return "";
    }

    public virtual bool Matches(Item item)
    {
        return item.GetType() == GetType();
    }

    public virtual void Serialize(IWriter writer)
    {
    }

    public virtual void Deserialize(IReader reader)
    {
    }
}