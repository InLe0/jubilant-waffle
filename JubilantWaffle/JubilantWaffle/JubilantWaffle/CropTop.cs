using System;
using System.Collections.Generic;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Xamarin.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using Image = System.Drawing.Image;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using FFImageLoading.Forms;
using FFImageLoading.Work;
using System.Collections.Generic;
using ImageSource = Xamarin.Forms.ImageSource;
using FFImageLoading.Transformations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using FFImageLoading;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FFImageLoading.Forms.Sample
{

    public class CropTop
    {
        double mX = 0f;
        double mY = 0f;
        double mRatioPan = -0.0015f;
        double mRatioZoom = 0.8f;
        readonly CropTransformation _crop;
        public static readonly BindableProperty ImageRotationProperty = BindableProperty.Create(nameof(ImageRotation), typeof(int), typeof(CropTop), default(int));

        public int ImageRotation
        {
            //get { return (int)GetValue(ImageRotationProperty); }
            //set { SetValue(ImageRotationProperty, value); }
            get { return 0; }
            set {}
        }
        public List<ITransformation> Transformations { get; set; }

        public string ImageUrl { get; set; } = "http://loremflickr.com/600/600/nature?filename=crop_transformation.jpg";

        /*void ReloadImage()
        {
            Transformations = new List<ITransformation>() {
                new CropTransformation(CurrentZoomFactor, CurrentXOffset, CurrentYOffset, 1f, 1f)
            };

            var page = this.GetCurrentPage() as CropTransformationPage;
            page.ReloadImage();
        }*/
        public Image SnipSnip(Image image,int offx, int offy,int x, int y)
        {
            Transformations = new List<ITransformation>() {
                new CropTransformation(1.0, offx, offy, x, y)
            };
            return image;
        }

        public Task<Stream> GetImageAsJpegAsync(int quality = 90, int maxWidth = 0, int maxHeight = 0, double framePadding = 0d)
        {
            _crop.XOffset = 50;
            _crop.YOffset = 50;
            _crop.CropHeightRatio = 1;
            _crop.CropWidthRatio = 1;
            _crop.ZoomFactor = 1;
            TaskParameter task = null;
                    task = ImageService.Instance.LoadCompiledResource("grommash.png");
                    //task = ImageService.Instance.LoadEmbeddedResource(_image.Path);
                  
            var applied = (1 + (2 * (framePadding / _crop.CropHeightRatio)));

            var transformations = (Transformations?.ToList() ?? new List<ITransformation>());
            transformations.Insert(0, new CropTransformation()
            {
                XOffset = _crop.XOffset,
                YOffset = _crop.YOffset,
                CropHeightRatio = _crop.CropHeightRatio,
                CropWidthRatio = _crop.CropWidthRatio,
                ZoomFactor = _crop.ZoomFactor * applied,
            });

            if (ImageRotation != 0)
                transformations.Insert(0, new RotateTransformation(Math.Abs(ImageRotation), ImageRotation < 0) { Resize = true });

            return task
                .WithCache(FFImageLoading.Cache.CacheType.Disk)
                .Transform(transformations)
                .DownSample(maxWidth, maxHeight)
                .AsJPGStreamAsync(quality);
        }

    }
}