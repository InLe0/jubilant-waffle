﻿using System;
using System.Collections.Generic;

namespace JubilantWaffle
{
    class Piece
    {
        public int DesiredPosition { get; set; }
        public int Orientation { get; set; }
        public string filePath { get; set; }

        public List<Piece> Shatter(int pieces)
        {
            Random r = new Random();
            List<Piece> fragments = new List<Piece>();

            for (int i = 0; i < pieces * pieces; i++)
            {
                int rInt = r.Next(0, 4);
                Piece edge = new Piece();
                edge.Orientation = rInt * 90;
                switch (pieces)
                {
                    case 1:
                        edge.filePath = "meme.png";
                        break;
                    case 2:
                        edge.filePath = "waa" + (i + 1) + ".png";
                        break;
                    case 3:
                        edge.filePath = "image_part_" + (i + 1) + ".png";
                        break;
                    case 4:
                        edge.filePath = "owlie_ouch" + (i + 1) + ".png";
                        break;
                    case 5:
                        if (i < 9)
                        {
                            edge.filePath = "imagepart00" + (i + 1) + ".png";
                        }
                        else
                        {
                            edge.filePath = "imagepart0" + (i + 1) + ".png";
                        }
                        break;
                    default:
                        break;
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
