namespace Miniventure.Crafting;

public class ToolRecipe : Recipe
{
    public ToolType Type { get; }
    public ToolLevel Level { get; }

    public ToolRecipe(ToolType type, ToolLevel level, params ResourceItem[] costs) 
        : base(new ToolItem(type, level), costs)
    {
        Type = type;
        Level = level;
    }

    public override Item CreateItem()
    {
        return new ToolItem(Type, Level);
    }
}
