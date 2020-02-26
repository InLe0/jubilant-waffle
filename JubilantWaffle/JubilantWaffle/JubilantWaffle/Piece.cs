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

            List<Piece> fragments = new List<Piece>();

            for (int i = 0; i < pieces*pieces; i++)
            {
                    int rInt = r.Next(0, 4);
                    Piece edge = new Piece();
                    edge.Orientation = rInt * 90;
                    edge.DesiredPosition = i;
            }
            fragments = Shuffle(fragments);
            return fragments;
        }

        public List<Piece> Shuffle(List<Piece> fragments)
        {
            Random r = new Random();
            Piece[] shufflerino = new Piece[fragments.Count];
            fragments.Reverse();
            while (fragments.Count > 1)
            {
                int rInt = r.Next(0, shufflerino.Length+1);
                if (shufflerino[rInt] == null)
                {
                    shufflerino[rInt] = fragments[0];
                    fragments.RemoveAt(0);
                }
            }
            foreach (Piece piece in shufflerino)
            {
                fragments.Add(piece);
            }
            fragments.Reverse();
            return fragments;
        }
    }
}
