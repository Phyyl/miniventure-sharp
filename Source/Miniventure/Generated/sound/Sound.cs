using Vildmark;
using Vildmark.Audio;
using Vildmark.Resources;

namespace com.mojang.ld22.sound;


public class Sound
{
    public static readonly Sound playerHurt = new("playerhurt.wav"); //creates a sound from playerhurt.wav file
    public static readonly Sound playerDeath = new("death.wav"); //creates a sound from death.wav file
    public static readonly Sound monsterHurt = new("monsterhurt.wav"); //creates a sound from monsterhurt.wav file
    public static readonly Sound test = new("test.wav"); //creates a sound from test.wav file
    public static readonly Sound pickup = new("pickup.wav"); //creates a sound from pickup.wav file
    public static readonly Sound bossdeath = new("bossdeath.wav"); //creates a sound from bossdeath.wav file
    public static readonly Sound craft = new("craft.wav"); //creates a sound from craft.wav file

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