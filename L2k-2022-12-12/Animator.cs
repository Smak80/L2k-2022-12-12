using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2k_2022_12_12
{
    public class Animator
    {
        private object _graphSync = new ();

        private Graphics _mainG;

        public Graphics MainGraphics
        {
            get =>_mainG;
            set { lock (_graphSync){ _mainG = value; } }
        }
        
    public Size ContainerSize {
        get
        {
            lock (_graphSync)
            {
                return new(
                    (int)MainGraphics.VisibleClipBounds.Width,
                    (int)MainGraphics.VisibleClipBounds.Height
                );
            }
        }
    }


    private Random r = new Random((int)DateTime.Now.Ticks);
        public Animator(Graphics g)
        {
            MainGraphics = g;
        }

        public void Animate()
        {
            var c = new Circle(
                Color.FromArgb(
                    r.Next(200),
                    r.Next(200),
                    r.Next(200)),
                50,
                ContainerSize);
            var dx = 1;
            var dy = 0;
            Thread t = new Thread(() =>
            {
                BufferedGraphics bg;
                lock (_graphSync)
                {
                    bg = BufferedGraphicsManager.Current.Allocate(
                        MainGraphics,
                        new Rectangle(0, 0, ContainerSize.Width, ContainerSize.Height)
                    );
                }

                while (c.X < ContainerSize.Width - c.R * 2)
                {
                    bg.Graphics.Clear(Color.White);
                    c.Paint(bg.Graphics);
                    Monitor.Enter(_graphSync);
                    try
                    {
                        bg.Render(MainGraphics);
                    }
                    finally
                    {
                        Monitor.Exit(_graphSync);
                    }

                    Thread.Sleep(50);
                    c.Move(dx, dy);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
    }
}
