﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using University_Schedule.Properties;




/// <summary>
/// Point pentru amfiteatre cu cifrele lor de rahat .... la linia 113
/// </summary>
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
                    g.DrawRectangle(new Pen(Brushes.Black), rec);
                }

            for (int i = 0; i <= Width; i += 188)
                for (int j = 0; j <= Height; j += 128)
                {
                    Rectangle rec = new Rectangle(new Point(i, j), new Size(new Point(188, 128)));
                    g.DrawRectangle(new Pen(Brushes.Black, 2), rec);
                    
                }
            return img;
        }
        //725,10 ,485,20
        public static Bitmap CombineImages(Bitmap bmp1,string number,string profil)
        {
            Bitmap bmp = Resources.schema_orar;
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawString(number, new Font("Segoe", 30), Brushes.Black, new PointF(720, 10));
            g.DrawString(profil, new Font("Segoe", 30), Brushes.Black, new PointF(470, 10));

            g.Flush();
            
            g.DrawImage(bmp1, new Point(128, 149));
           
            return bmp;
        }


        public static bool ContainsAny(string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }

        public static Bitmap InsertDataInImage(Bitmap source, Brush color, Rectangle rec, Course cours)
        {
            Rectangle profesor;
            Rectangle curs;
            Rectangle sala;
            Rectangle semigrupa;
            using (Graphics graph = Graphics.FromImage(source))
            {
                if (rec.Height == 31)
                {
                    curs = new Rectangle(new Point(rec.X + 1, rec.Y + 1), new Size(rec.Width, rec.Height));
                    semigrupa = new Rectangle(new Point(rec.X + ((rec.Width / 4) + (rec.Width / 4) + (rec.Height / 4) + (rec.Width / 4)), rec.Y + 1), new Size(rec.Width, rec.Height));
                    profesor = new Rectangle(new Point(rec.X + 1, rec.Y + (rec.Height / 2)), new Size(rec.Width, rec.Height));
                    sala = new Rectangle(new Point(rec.X + ((rec.Width / 4) + (rec.Width / 4) + (rec.Height / 4) + (rec.Width / 4)), rec.Y + (rec.Height / 2)), new Size(rec.Width, rec.Height));
                }
                else if (rec.Height == 63)
                {
                    curs = new Rectangle(new Point((rec.X + rec.Width / 3 + 5), (rec.Y + rec.Height / 3)), new Size(rec.Width, rec.Height));
                    semigrupa = new Rectangle(new Point(rec.X + 1, (rec.Y + rec.Height / 4 + rec.Height / 4 + rec.Height / 4)), new Size(rec.Width, rec.Height));
                    profesor = new Rectangle(new Point(rec.X + 1, rec.Y + 1), new Size(rec.Width, rec.Height));
                    sala = new Rectangle(new Point(rec.X + ((rec.Width / 4) + (rec.Height / 4) + (rec.Width / 4) + (rec.Width / 4) - 5), rec.Y + (rec.Height / 4) + (rec.Height / 4) + (rec.Height / 4)), new Size(rec.Width, rec.Height));
                }
                else if (rec.Height == 95)
                {
                    curs = new Rectangle(new Point((rec.X + rec.Width / 3 + 10), (rec.Y + rec.Height / 3 + 13)), new Size(rec.Width, rec.Height));
                    semigrupa = new Rectangle(new Point(rec.X + 1, (rec.Y + rec.Height / 3 + rec.Height / 4 + rec.Height / 4)), new Size(rec.Width, rec.Height));
                    profesor = new Rectangle(new Point(rec.X + 1, rec.Y + 3), new Size(rec.Width, rec.Height));
                    sala = new Rectangle(new Point(rec.X + ((rec.Width / 4) + (rec.Height / 3) + (rec.Width / 4) + (rec.Width / 4) - 55), rec.Y + (rec.Height / 5) + (rec.Height / 4) + (rec.Height / 4) + 13), new Size(rec.Width, rec.Height));
                }
                else if (rec.Height == 127)
                {
                    curs = new Rectangle(new Point((rec.X + rec.Width / 3 - 5), (rec.Y + rec.Height / 3 + 15)), new Size(rec.Width, rec.Height));
                    semigrupa = new Rectangle(new Point(rec.X + 1, (rec.Y + rec.Height / 4 + rec.Height / 4 + rec.Height / 4) + 13), new Size(rec.Width, rec.Height));
                    profesor = new Rectangle(new Point(rec.X + 1, rec.Y + 3), new Size(rec.Width, rec.Height));
                    sala = new Rectangle(new Point(rec.X + ((rec.Width / 4) + (rec.Height / 4) + (rec.Width / 4) + (rec.Width / 4) - 65), rec.Y + (rec.Height / 4) + (rec.Height / 4) + (rec.Height / 4) + 13), new Size(rec.Width, rec.Height));
                }
                else
                {
                    curs = new Rectangle();
                    semigrupa = new Rectangle();
                    profesor = new Rectangle();
                    sala = new Rectangle();
                    Debug.WriteLine("Error!");
                }
                source = DrawString(cours.access_materia, source, curs);
                source = DrawString(cours.access_semigrupa, source, semigrupa);
                source = DrawString(cours.access_profesor, source, profesor);
                string[] amfiteatre = { "Spiru Haret", "Stoilow", "Pompeiu","Titeica" };
                try
                {
                    if ((cours.access_sala != "") && (Int32.Parse(cours.access_sala) == 7 || Int32.Parse(cours.access_sala) == 27 || Int32.Parse(cours.access_sala) == 37 || Int32.Parse(cours.access_sala) == 17))
                    {
                        string salaDraw = string.Empty;
                        switch (Int32.Parse(cours.access_sala))
                        {
                            case 7:
                                salaDraw += "(0)" + amfiteatre[0];
                                break;
                            case 17:
                                salaDraw += "(1)" + amfiteatre[1];
                                break;
                            case 27:
                                salaDraw += "(2)" + amfiteatre[2];
                                break;
                            case 37:
                                salaDraw += "(3)" + amfiteatre[3];
                                break;
                            default:
                                Debug.WriteLine("Error");
                                break;
                        }
                        source = DrawString(salaDraw, source, sala);
                    }
                    else
                    {
                        source = DrawString(cours.access_sala, source, sala);
                    }
                }
                catch(Exception e)
                {
                    source = DrawString("Error", source, sala);
                    Debug.WriteLine(e.Message);
                }
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
            if (x>94*12)
                xx = 94 * 12;
            if (y > 32 * 20)
                yy = 32 * 20;    

            for (int i = 0;i<= 94 * 12;i+= 94)
                if(x<i)
                {
                    xx = i;
                    xx -= 94;
                    break;
                }
            for(int i = 0;i<= 32 * 20;i+= 32)
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

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        static public System.Drawing.Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

    }
}
