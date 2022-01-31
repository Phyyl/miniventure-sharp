using Vildmark;
using Vildmark.Serialization;

namespace Miniventure;

public class GameState : ISerializable, IDeserializable
{
    public int currentLevel = 3;

    public Level[] levels = new Level[5];
    public Player player;

    public int gameTime;
    public int playerDeadTime;
    public int pendingLevelChange;
    public int wonTimer;
    public bool hasWon;

    public Level level => levels[currentLevel];

    public GameState()
    {
        levels = new Level[5];

        levels[4] = new SkyLevel();
        levels[3] = new TopLevel();
        levels[2] = new UndergroundLevel(-1);
        levels[1] = new UndergroundLevel(-2);
        levels[0] = new UndergroundLevel(-3);
    }

    public void Serialize(IWriter writer)
    {
        writer.WriteValue(currentLevel);
        writer.WriteValue(gameTime);
        writer.WriteValue(playerDeadTime);
        writer.WriteValue(pendingLevelChange);
        writer.WriteValue(wonTimer);
        writer.WriteValue(hasWon);

        levels[4].Serialize(writer);
        levels[3].Serialize(writer);
        levels[2].Serialize(writer);
        levels[1].Serialize(writer);
        levels[0].Serialize(writer);
    }

    public void Deserialize(IReader reader)
    {
        currentLevel = reader.ReadValue<int>();
        gameTime = reader.ReadValue<int>();
        playerDeadTime = reader.ReadValue<int>();
        pendingLevelChange = reader.ReadValue<int>();
        wonTimer = reader.ReadValue<int>();
        hasWon = reader.ReadValue<bool>();

        levels[4].Deserialize(reader);
        levels[3].Deserialize(reader);
        levels[2].Deserialize(reader);
        levels[1].Deserialize(reader);
        levels[0].Deserialize(reader);

        player = levels.Select(l => l.Player).NotNull().FirstOrDefault();
    }

    public void Generate()
    {
        currentLevel = 3;

        levels[4].Generate(null);
        levels[3].Generate(levels[4]);
        levels[2].Generate(levels[3]);
        levels[1].Generate(levels[2]);
        levels[0].Generate(levels[1]);

        player = new Player();
        player.TrySpawn(level);

        level.Add(player);

        //for (int i = 0; i < 5; i++)
        //{
        //    levels[i].TrySpawn(5000);
        //}
    }
}
