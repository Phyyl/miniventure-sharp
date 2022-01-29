using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public abstract class Applet // Stub
    {
        public void setLayout(BorderLayout borderLayout)
        {

        }

        public void add(Game game, BorderLayout borderLayout)
        {

        }

        internal static AudioClip newAudioClip(string url)
        {
            return new AudioClip(url);
        }

        public abstract void init();
        public abstract void start();
        public abstract void stop();
    }
}
