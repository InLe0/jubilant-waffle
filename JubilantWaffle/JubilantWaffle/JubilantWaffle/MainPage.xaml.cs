using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JubilantWaffle
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible (false)]
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            puzzleSizeButton.Clicked += SetSizeButton;
        }
        

        void SetSizeButton(object sender, EventArgs e)
        {
            Piece piece = new Piece();
            List<Piece> fragments = new List<Piece>();
            string puzzleSize = puzzleSizeEnt.Text;
            int puzzleSizeInt = System.Convert.ToInt32(puzzleSize);
            Random r = new Random();
            ArrayList buttonList = new ArrayList();
            Image image = new Image();
            image.Source = "grommash.png";
            fragments = piece.Shatter(puzzleSizeInt);
            for (int i = 0; i < puzzleSizeInt; i++)
            {
                for (int j = 0; j < puzzleSizeInt; j++)
                {
                    ImageButts button = new ImageButts();
                    int mathedValue = App.screenWidth * 3 / (puzzleSizeInt * 10)*puzzleSizeInt;
                    MathedValue.Text = mathedValue.ToString();
                    ScreenValue.Text = App.screenWidth.ToString();
                    myGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    myGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Source = ImageSource.FromFile("grommash.png");                  
                    button.Aspect = Aspect.AspectFill;
                    buttonList.Add(button);
                    myGrid.Children.Add(button);   
                }
            }
        }
        /*public Stream Grunhilde(string imagePath)
        {
            Image image = new Image();
            image.Source=imagePath;
            var table = new Image();
            I
            table = ImageSource.FromStream(() =>
            {
                var stream = table.GetStream();
                _mediaFile.Dispose();
                return stream;
            });
        }*/
    }
}
