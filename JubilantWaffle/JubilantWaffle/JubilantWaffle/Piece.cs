using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using FFImageLoading.Transformations;
using System.IO;

namespace JubilantWaffle
{
    class Piece
    {
        public Image Shard { get; set; }
        public int DesiredPosition { get; set; }
        public int Orientation { get; set; }

        public List<Piece> Shatter(int pieces, string path = "grommash.png")
        {
            Random r = new Random();
            Image image = Image.FromFile(path);

            List<Piece> Fragments = new List<Piece>();

            for (int i = 0; i < pieces*pieces; i++)
            {
                    int rInt = r.Next(0, 4);
                    Piece edge = new Piece();
                    edge.Orientation = rInt * 90;
                    edge.DesiredPosition = i;
                
            }
            return Fragments;
        }


   
    }
}
