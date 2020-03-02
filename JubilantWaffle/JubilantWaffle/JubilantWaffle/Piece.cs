using System;
using System.Collections.Generic;

namespace JubilantWaffle
{
    class Piece
    {
        //the position on which the image was generated
        public int DesiredPosition { get; set; }
        //current orientation of the image
        public int Orientation { get; set; }
        // file path of each image
        public string filePath { get; set; }

        /// <summary>
        ///  creates list of all the images based on the size of the puzzle
        /// </summary>
        /// <param name="pieces"></param>
        /// <returns></returns>
        public List<Piece> Shatter(int pieces)
        {
            Random r = new Random();
            //creating the list of all the images
            List<Piece> fragments = new List<Piece>();
            //creates the instances of the images and loads them into the fragments
            for (int i = 0; i < pieces * pieces; i++)
            {
                int rInt = r.Next(0, 4);
                Piece edge = new Piece();
                //sets a random rotation
                edge.Orientation = rInt * 90;
                // sets the image based on the chosen size
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
                
                //sets the positions based on the order of creation
                edge.DesiredPosition = i;
                fragments.Add(edge);
            }
            //refer to shuffle method
            fragments = Shuffle(fragments);
            return fragments;
        }
        /// <summary>
        /// randomises the position of the images in the list and keeps the desired position as a tracker of the original one
        /// </summary>
        /// <param name="fragments"></param>
        /// <returns></returns>
        public List<Piece> Shuffle(List<Piece> fragments)
        {
            Random r = new Random();
            // shuffled list
            List<Piece> shufflerino = new List<Piece>();
            //shuffles until there is no more fragments
            while (fragments.Count > 0)
            {
                int rInt = r.Next(0, fragments.Count);
                // add a random fragment to the list
                shufflerino.Add(fragments[rInt]);
                fragments.RemoveAt(rInt);
            }
            return shufflerino;
        }
    }
}
