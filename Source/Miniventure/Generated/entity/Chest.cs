namespace com.mojang.ld22.entity;


public class Chest : Furniture
{
    public Inventory Inventory { get; } = new Inventory();

    public Chest() : base("Chest")
    { 
        col = Color.Get(-1, 110, 331, 552);
        sprite = 1; 
    }

    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new ContainerMenu(player, "Chest", Inventory);

        return true;
    }
}