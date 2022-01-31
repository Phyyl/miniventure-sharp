using Miniventure.Entities;

namespace Miniventure.Levels.Generation;

public interface ILevelProvider
{
    public int GrassColor { get; }
    public int DirtColor { get; }
    public int SandColor { get; }
    public int MonsterDensity { get; }

    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }

    LevelData GetLevelData();
    IEnumerable<Entity> GetEntities();
}
