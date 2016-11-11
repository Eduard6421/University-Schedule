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
            for (int i = 0; i <= Width; i += 60)
                for (int j = 0; j <= Height; j += 22)
                {
                    Rectangle rec = new Rectangle(new Point(i, j), new Size(new Point(60, 22)));
                    g.DrawRectangle(new Pen(Brushes.Gray), rec);
                }

            for (int i = 0; i <= Width; i += 120)
                for (int j = 0; j <= Height; j += 88)
                {
                    Rectangle rec = new Rectangle(new Point(i, j), new Size(new Point(120, 88)));
                    g.DrawRectangle(new Pen(Brushes.Black), rec);
                    
                }
            return img;
        }


        public static Point SearchForMatch(int x,int y)
        {
            int xx = 0, yy = 0;
            for(int i = 0;i<=60*12;i+=60)
                if(x<i)
                {
                    xx = i;
                    xx -= 60;
                    break;
                }
            for(int i = 0;i<22*20;i+=22)
                if(y<i)
                {
                    yy = i;
                    yy -= 22;
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
        /// <summary>
        /// Trebuie schimbat dimensiunea font-ului dupa curs astfel intr-un curs de un sfert de patratica sa scrie mic,
        /// iar intr-unul de curs saptamanar sa scrie font mare ... dimensiunea se schimba in 'new Font("Segoe", AICI)'.
        /// SFAT : Dimensiunea fontului sa fie trimisa prin parametru in momentul in care se vede de cate dreptungiuri sunt in stiva.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="image"></param>
        /// <param name="rectf"></param>
        /// <returns></returns>

        public static Bitmap DrawString(string text,Image image,RectangleF rectf)
        {
            Bitmap bmp = new Bitmap(image);
            //incarcam imaginea intr-un Graphics pentru a desena
            Graphics g = Graphics.FromImage(bmp);
            //pentru claritate proprietati
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //functia asta va scrie mesajul din primul parametru in imagine la coordonatele dreptunghiului
            g.DrawString(text, new Font("Segoe", 10), Brushes.Black, rectf);

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
