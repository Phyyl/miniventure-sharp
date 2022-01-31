namespace Miniventure.Items;

public record class ToolType(string Name, int Sprite)
{
    public static readonly ToolType Shovel = new("Shvl", 0);
    public static readonly ToolType Hoe = new("Hoe", 1);
    public static readonly ToolType Sword = new("Swrd", 2);
    public static readonly ToolType Pickaxe = new("Pick", 3);
    public static readonly ToolType Axe = new("Axe", 4);

    public static EnumDictionary<string, ToolType> All { get; } = new(v => v.Name);

}
