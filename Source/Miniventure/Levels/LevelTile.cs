namespace Miniventure.Levels;

public record struct LevelTile(byte ID, byte Data = 0)
{
    public static implicit operator LevelTile(Tile tile) => new LevelTile(tile.ID);
}
