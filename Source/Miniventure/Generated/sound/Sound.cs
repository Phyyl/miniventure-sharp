using Vildmark;
using Vildmark.Audio;
using Vildmark.Resources;

namespace com.mojang.ld22.sound;


public class Sound
{
    public static readonly Sound playerHurt = new Sound("playerhurt.wav"); //creates a sound from playerhurt.wav file
    public static readonly Sound playerDeath = new Sound("death.wav"); //creates a sound from death.wav file
    public static readonly Sound monsterHurt = new Sound("monsterhurt.wav"); //creates a sound from monsterhurt.wav file
    public static readonly Sound test = new Sound("test.wav"); //creates a sound from test.wav file
    public static readonly Sound pickup = new Sound("pickup.wav"); //creates a sound from pickup.wav file
    public static readonly Sound bossdeath = new Sound("bossdeath.wav"); //creates a sound from bossdeath.wav file
    public static readonly Sound craft = new Sound("craft.wav"); //creates a sound from craft.wav file

    private AudioTrack track;

    private Sound(string name)
    {
        track = ResourceLoader.LoadEmbedded<AudioTrack>(name);
    }

    public virtual void Play()
    {
        track.Play();
    }
}