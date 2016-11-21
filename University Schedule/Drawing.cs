﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Trebuie selectate dimensiunile corect
        /// </summary>
        /// <param name="source"></param>
        /// <param name="color"></param>
        /// <param name="rec"></param>
        /// <param name="cours"></param>
        /// <returns></returns>
        public static Bitmap InsertDataInImage(Bitmap source,Brush color,Rectangle rec, Course cours)
        {
            Rectangle profesor;
            Rectangle curs;
            Rectangle sala;
            Rectangle semigrupa;
            using (Graphics graph = Graphics.FromImage(source))
            {

                curs = new Rectangle(new Point(rec.X + 1, rec.Y + 1), new Size(rec.Width, rec.Height));
                semigrupa = new Rectangle(new Point(rec.X + ((rec.Width / 4) + (rec.Width / 4) + (rec.Height / 4) + (rec.Width / 4)), rec.Y + 1), new Size(rec.Width, rec.Height));
                profesor = new Rectangle(new Point(rec.X + 1, rec.Y + (rec.Height / 2)), new Size(rec.Width, rec.Height));
                sala = new Rectangle(new Point(rec.X + ((rec.Width / 4) + (rec.Width / 4) + (rec.Height / 4) + (rec.Width / 4)), rec.Y + (rec.Height / 2)), new Size(rec.Width, rec.Height));

                source = DrawString(cours.access_materia, source, curs);
                source = DrawString(cours.acces_semigrupa, source, semigrupa);
                source = DrawString(cours.access_profesor, source, profesor);
                source = DrawString(cours.access_sala, source, sala);

            }
            return source;
        }
      
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
       

        public static Bitmap DrawString(string text,Image image,RectangleF rectf)
        {
            Bitmap bmp = new Bitmap(image);
            
            Graphics g = Graphics.FromImage(bmp);
            
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawString(text, new Font("Segoe", 10), Brushes.Black, rectf);

         
            g.Flush();


           
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
