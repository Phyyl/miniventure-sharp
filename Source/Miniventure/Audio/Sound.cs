using Vildmark.Audio;
using Vildmark.Resources;

namespace Miniventure.Audio;

public class Sound
{
    public static readonly Sound playerHurt = new("playerhurt.wav");
    public static readonly Sound playerDeath = new("death.wav");
    public static readonly Sound monsterHurt = new("monsterhurt.wav");
    public static readonly Sound test = new("test.wav");
    public static readonly Sound pickup = new("pickup.wav");
    public static readonly Sound bossdeath = new("bossdeath.wav");
    public static readonly Sound craft = new("craft.wav");

    private readonly AudioTrack track;

    private Sound(string name)
    {
        track = ResourceLoader.LoadEmbedded<AudioTrack>(name);
    }

    public virtual void Play()
    {
        track.Play();
    }
}