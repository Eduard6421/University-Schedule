using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Schedule
{
    class Drawing
    {
        Drawing() { }

        /// <summary>
        /// Mai trebuie sa impartim iar in dreptungiuri mai fine pentru saptamani pare si impare precum si pentru fiecare ora.
        /// </summary>
        /// <param i = "Width">Contorizeaza pe verticala la ore 120 fiind lungimea unui patratel la ore</param>
        /// <param j="Height">Contorizeaza pe origontala 90 fiind lungimea unui patratel la zile</param>
        /// <returns></returns>
        public static Bitmap DrawRectangleOnImage(int Width,int Height)
        {
            Bitmap img = DrawFilledRectangle(Width, Height, Brushes.White);

            Graphics g = Graphics.FromImage(img);
            for (int i = 0; i <= Width; i += 120)
                for (int j = 0; j <= Height; j += 90)
                {
                    Rectangle rec = new Rectangle(new Point(i, j), new Size(new Point(120, 90)));
                    g.DrawRectangle(new Pen(Brushes.Black), rec);
                }

            return img;
        }


        public static Point SearchForMatch(int x,int y)
        {
            int xx = 0, yy = 0;
            for(int i = 0;i<=120*6;i+=120)
                if(x<i)
                {
                    xx = i;
                    xx -= 120;
                    break;
                }
            for(int i = 0;i<90*5;i+=90)
                if(y<i)
                {
                    yy = i;
                    yy -= 90;
                    break;
                }
            return (new Point(xx, yy));
        }

        public static Bitmap FillRectangleWithAColor(Point point1,Size size,Bitmap myImage,Brush color)
        {
           
            using (Graphics graph = Graphics.FromImage(myImage))
            {
                Rectangle rec = new Rectangle(point1, size);
                graph.FillRectangle(color, rec);
            }
            return myImage;
        }

        public static Bitmap DrawString(string filename)
        {
            Bitmap bmp = new Bitmap(filename);
            //patratul in care va fi desenat textul cu coordonatele din imaginea preluata la linia precedenta
            RectangleF rectf = new RectangleF(0, 0, 150, 150);
            //incarcam imaginea intr-un Graphics pentru a desena
            Graphics g = Graphics.FromImage(bmp);
            //pentru claritate proprietati
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //functia asta va scrie mesajul din primul parametru in imagine la coordonatele dreptunghiului
            g.DrawString("yourText", new Font("Tahoma", 20), Brushes.Black, rectf);

            //curata streamul
            g.Flush();


            //returneaza imaginea cu mesajul scris in ea
            return bmp;
        }

        private static Bitmap DrawFilledRectangle(int x, int y, Brush br)
        {
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, x, y);
                graph.FillRectangle(br, ImageSize);
            }
            return bmp;
        }
    }
}
