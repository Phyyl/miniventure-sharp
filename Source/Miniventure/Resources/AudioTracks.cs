using Vildmark.Audio;
using Vildmark.Resources;

namespace Miniventure.Resources;

internal static class AudioTracks
{
    public static readonly AudioTrack PlayerHurt = ResourceLoader.LoadEmbedded<AudioTrack>("playerhurt.wav");
    public static readonly AudioTrack PlayerDeath = ResourceLoader.LoadEmbedded<AudioTrack>("death.wav");
    public static readonly AudioTrack MonsterHurt = ResourceLoader.LoadEmbedded<AudioTrack>("monsterhurt.wav");
    public static readonly AudioTrack Test = ResourceLoader.LoadEmbedded<AudioTrack>("test.wav");
    public static readonly AudioTrack Pickup = ResourceLoader.LoadEmbedded<AudioTrack>("pickup.wav");
    public static readonly AudioTrack Bossdeath = ResourceLoader.LoadEmbedded<AudioTrack>("bossdeath.wav");
    public static readonly AudioTrack Craft = ResourceLoader.LoadEmbedded<AudioTrack>("craft.wav");
}
