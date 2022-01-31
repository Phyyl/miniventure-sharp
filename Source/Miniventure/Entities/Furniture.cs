using Miniventure.Graphics;
using Miniventure.Items;

namespace Miniventure.Entities;


public class Furniture : Entity
{
    private int pushTime = 0;
    private Direction? pushDir = null;
    public int col, sprite;
    public string name;
    private Player shouldTake;

    public Furniture(string name, int x = 0, int y = 0, int horizontalRadius = 3, int verticalRadius = 3)
        : base(x, y, horizontalRadius, verticalRadius)
    {
        this.name = name;
    }

    public override void Update()
    {
        if (shouldTake != null)
        {
            if (shouldTake.activeItem is PowerGloveItem)
            {
                Remove();
                shouldTake.inventory.Add(0, shouldTake.activeItem);
                shouldTake.activeItem = new FurnitureItem(this);
            }

            shouldTake = null;
        }

        if (pushDir == Direction.Down)
        {
            Move(0, 1);
        }

        if (pushDir == Direction.Up)
        {
            Move(0, -1);
        }

        if (pushDir == Direction.Left)
        {
            Move(-1, 0);
        }

        if (pushDir == Direction.Right)
        {
            Move(1, 0);
        }

        pushDir = null;

        if (pushTime > 0)
        {
            pushTime--;
        }
    }

    public override void Render(Screen screen)
    {
        screen.Render(X - 8, Y - 8 - 4, sprite * 2 + 8 * 32, col, 0);
        screen.Render(X - 0, Y - 8 - 4, sprite * 2 + 8 * 32 + 1, col, 0);
        screen.Render(X - 8, Y - 0 - 4, sprite * 2 + 8 * 32 + 32, col, 0);
        screen.Render(X - 0, Y - 0 - 4, sprite * 2 + 8 * 32 + 33, col, 0);
    }

    public override bool Blocks(Entity e)
    {
        return true;
    }

    public override void TouchedBy(Entity entity)
    {
        if (entity is Player player && pushTime == 0)
        {
            pushDir = player.Direction;
            pushTime = 10;
        }
    }

    public virtual void Take(Player player)
    {
        shouldTake = player;
    }
}