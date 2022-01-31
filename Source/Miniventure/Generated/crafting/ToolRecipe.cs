namespace com.mojang.ld22.crafting;

public class ToolRecipe : Recipe
{
    private ToolType type;
    private ToolLevel level;

    public ToolRecipe(ToolType type, ToolLevel level) : base(new ToolItem(type, level))
    {
        this.type = type;
        this.level = level;
    }

    public override void Craft(Player player)
    {
        player.inventory.Add(0, new ToolItem(type, level));
    }
}
