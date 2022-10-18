using Vildmark.Serialization;

namespace Miniventure.Entities;

public abstract class Furniture : Entity
{
    private int pushTime = 0;
    private Direction? pushDir = null;
    public int col, sprite;
    public string name;

    public Furniture(string name, int sprite, int col, int x = 0, int y = 0, int horizontalRadius = 3, int verticalRadius = 3)
        : base(x, y, horizontalRadius, verticalRadius)
    {
        this.name = name;
        this.sprite = sprite;
        this.col = col;
    }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteValue(pushTime);

        if (!writer.WriteIsDefault(pushDir))
        {
            writer.WriteValue(pushDir.Value);
        }

        writer.WriteValue(col);
        writer.WriteValue(sprite);
        writer.WriteString(name);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        pushTime = reader.ReadValue<int>();

        if (reader.ReadIsDefault())
        {
            pushDir = null;
        }
        else
        {
            pushDir = reader.ReadValue<Direction>();
        }

        col = reader.ReadValue<int>();
        sprite = reader.ReadValue<int>();
        name = reader.ReadString();
    }

    public override void Update()
    {
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
        if (entity is Player player && pushTime == 0 && player.activeItem is PowerGloveItem)
        {
            pushDir = player.Direction;
            pushTime = 10;
        }
    }
}