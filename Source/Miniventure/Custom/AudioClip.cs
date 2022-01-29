using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Audio;
using Vildmark.Resources;

namespace MiniventureSharp.Custom
{
    public class AudioClip
    {
        private string url;
        private AudioTrack audioTrack;

        public AudioClip(string url)
        {
            this.url = url;

            audioTrack = ResourceLoader.Load<AudioTrack>(GetType().getResourceAsStream(url));
        }

        internal void play()
        {
            audioTrack.Play();
        }
    }
}
