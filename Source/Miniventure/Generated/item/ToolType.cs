namespace com.mojang.ld22.item;

//TODO: convert to record
public class ToolType
{
    public static readonly ToolType Shovel = new("Shvl", 0);
    public static readonly ToolType Hoe = new("Hoe", 1);
    public static readonly ToolType Sword = new("Swrd", 2);
    public static readonly ToolType Pickaxe = new("Pick", 3);
    public static readonly ToolType Axe = new("Axe", 4);

    public string Name { get; }
    public int Sprite { get; }

    private ToolType(string name, int sprite)
    {
        Name = name;
        Sprite = sprite;
    }
}
