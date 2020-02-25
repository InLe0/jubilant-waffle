using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace JubilantWaffle
{
    class Imagify
    {
        public Imagify()
        {
        }

        public Stream ToStream(System.Drawing.Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }
}
