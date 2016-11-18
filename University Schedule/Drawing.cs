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
            for (int i = 0; i <= Width; i += 94)
                for (int j = 0; j <= Height; j += 32)
                {
                    Rectangle rec = new Rectangle(new Point(i, j), new Size(new Point(94, 32)));
                    g.DrawRectangle(new Pen(Brushes.Gray), rec);
                }

            for (int i = 0; i <= Width; i += 188)
                for (int j = 0; j <= Height; j += 128)
                {
                    Rectangle rec = new Rectangle(new Point(i, j), new Size(new Point(188, 128)));
                    g.DrawRectangle(new Pen(Brushes.DarkMagenta,2), rec);
                    
                }
            return img;
        }

        /// <summary>
        /// Trebuie caz special pentru fiecare situatie : 
        /// 1) este curs deci are 8 patratele.
        /// 2) este laborator ori curs odata la 2 saptamani deci 4 patratele
        /// 3) este o data la 2 saptamani si este laborator de 2 patratele
        ///  .... Trebuie o formula matematica care sa dea direct incepului dreptunghiului in care sa scrie
        ///  ........ la fel si pentru celelelalte date.
        ///  EXEMPLU de dreptunghi care returneaza. 
        ///  0,0    0,1     0,2     0,3   Pointul de start va fii (0,0) iar indexul va avea numarul de patrate din interior asa ca 
        ///  1,0    1,1     1,2     1,3     sizeul ca trebui sa fie dimensiunea unui patratel ori indexul.
        ///  2,0    2,1     2,2     2,3   Adaug la Point-ul start x+35 si y+15 ca sa il mut pe mijloc in cazul laboratorului o data pe saptamana de 4 patratele.
        ///  3,0    3,1     3,2     3,3   astfel punctul de unde porneste dreptunghiul returnat pentru scriere este de 1,1 aproximativ.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Rectangle SearchTypeCourse(Point start,int index)
        {
            return (new Rectangle(new Point(start.X +35,start.Y+15), new Size(index * 94, index * 32)));
        }

      

        public static Point SearchForMatch(int x,int y)
        {
            int xx = 0, yy = 0;
            for(int i = 0;i<= 94 * 12;i+= 94)
                if(x<i)
                {
                    xx = i;
                    xx -= 94;
                    break;
                }
            for(int i = 0;i< 32 * 20;i+= 32)
                if(y<i)
                {
                    yy = i;
                    yy -= 32;
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
