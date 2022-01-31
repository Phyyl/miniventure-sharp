namespace Miniventure.Crafting;

public class ToolRecipe : Recipe
{
    private readonly ToolType type;
    private readonly ToolLevel level;

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
