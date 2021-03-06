﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_03
{
    public static class DrawingHelper
    {
        private static int bmpHeight = 1000;
        private static int bmpWidth = 1000;

        public static Bitmap DrawDot(List<CityInfo> cityList, float scale)
        {
            float rel_x, rel_y; //Used to store the city coordinate relative to the PictureBox form. 
            string info; //Used to store city info
            Bitmap newBmp = new Bitmap((int)(bmpHeight * scale), (int)(bmpWidth * scale));

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                //use white background
                g.Clear(Color.White);

                //Draw smoothly
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //set scale
                g.ScaleTransform(scale, scale);

                foreach (CityInfo city in cityList)
                {
                    //Since all real X and Y value fall into the rage between 0 and 100, I set the size of the PictureBox form to 1000x1000, so all relative coordinate is just their real coordinate times 10.

                    rel_x = city.X * 10;
                    rel_y = bmpWidth - city.Y * 10; //since the origin coordinate used in Graphics instance is the top-left corner of the form, while our data is based on bottom-left origin.                    

                    //Draw the dot
                    g.FillEllipse(Brushes.Black, rel_x, rel_y, 4, 4);

                    info = string.Format($"(ID: {city.ID}, X: {city.X}, Y: {city.Y})");
                    g.DrawString(info, new Font("Times New Roman", 6f), Brushes.Gray, new PointF(rel_x - 6, rel_y + 4));
                }
            }

            return newBmp;
        }

        public static Bitmap DrawLine(CityInfo firstCity, CityInfo secondCity, Bitmap bmp, float scale)
        {
            float rel_first_x = firstCity.X * 10 + 2;
            float rel_second_x = secondCity.X * 10 + 2;
            float rel_first_y = 1000 - firstCity.Y * 10 + 2;
            float rel_second_y = 1000 - secondCity.Y * 10 + 2;

            Bitmap newBmp = new Bitmap(bmp);

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                using (Pen myPen = new Pen(Brushes.DarkSeaGreen, 1))
                {
                    //Draw smoothly
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    //set scale
                    g.ScaleTransform(scale, scale);

                    //Draw the line
                    g.DrawLine(myPen, rel_first_x, rel_first_y, rel_second_x, rel_second_y);
                }
            }

            return newBmp;
        }
    }
}
