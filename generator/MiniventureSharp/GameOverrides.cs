using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp
{
    public class GameOverrides : com.mojang.ld22.Game
    {
        public static GameOverrides Instance { get; private set; }

        public GameOverrides()
        {
            Instance = this;
        }

        public override void start()
        {
            stop();
            run();
        }

        public override void Update(float delta)
        {
            tick();
            base.Update(delta);
        }

        public override void Render(float delta)
        {
            render();
            base.Render(delta);
        }
    }
}
