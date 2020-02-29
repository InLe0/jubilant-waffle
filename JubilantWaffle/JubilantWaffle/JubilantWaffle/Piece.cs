using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using FFImageLoading.Transformations;
using System.IO;

namespace JubilantWaffle
{
    class Piece
    {
        public int DesiredPosition { get; set; }
        public int Orientation { get; set; }
        public string filePath { get; set; }

        public List<Piece> Shatter(int pieces, string path = "grommash.png")
        {
            Random r = new Random();
            Image image = new Image();
            image.Source = "grommash.png";

            List<Piece> fragments = new List<Piece>();

            for (int i = 0; i < pieces*pieces; i++)
            {
                    int rInt = r.Next(0, 4);
                    Piece edge = new Piece();
                    edge.Orientation = rInt * 90;
                if (i<9)
                {
                    edge.filePath = "imagepart00" + (i+1) + ".png";
                }
                else
                {
                    edge.filePath = "imagepart0" + (i+1) + ".png";
                }
                   
                    edge.DesiredPosition = i;
                    fragments.Add(edge);
            }
            fragments = Shuffle(fragments);
            return fragments;
        }

        public List<Piece> Shuffle(List<Piece> fragments)
        {
            Random r = new Random();
            List<Piece> shufflerino = new List<Piece>();
            while (fragments.Count > 0)
            {
                int rInt = r.Next(0, fragments.Count);
                    shufflerino.Add(fragments[rInt]);
                    Console.WriteLine(fragments[rInt].Orientation);
                    fragments.RemoveAt(rInt);
            }
            return shufflerino;
        }
    }
}
