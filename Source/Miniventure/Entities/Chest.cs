using Vildmark.Serialization;

namespace Miniventure.Entities;

public class Chest : Furniture
{
    public Inventory Inventory { get; private set; } = new Inventory();

    public Chest() 
        : base("Chest", 1, Color.Get(-1, 110, 331, 552))
    {
    }

    public override bool Use(Player player, Direction attackDir)
    {
        Game.Instance.Menu = new ContainerMenu(player, "Chest", Inventory);

        return true;
    }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteObject(Inventory);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        Inventory = reader.ReadObject<Inventory>();
    }
}