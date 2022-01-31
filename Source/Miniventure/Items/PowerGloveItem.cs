using Miniventure.Entities;
using Miniventure.Graphics;

namespace Miniventure.Items;


public class PowerGloveItem : Item
{
    public override int GetColor()
    {
        return Color.Get(-1, 100, 320, 430);
    }

    public override int GetSprite()
    {
        return 7 + 4 * 32;
    }

    public override void RenderIcon(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0);
    }

    public override void RenderInventory(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0);
        Font.Draw(GetName(), screen, x + 8, y, Color.Get(-1, 555, 555, 555));
    }

    public override string GetName()
    {
        return "Pow glove";
    }

    public override bool Interact(Player player, Entity entity, Direction attackDir)
    {
        if (entity is Furniture furniture)
        {
            furniture.Take(player);

            return true;
        }

        return false;
    }
}
