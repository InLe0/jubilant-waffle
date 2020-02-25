using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            string puzzleSize = puzzleSizeEnt.Text;
            int puzzleSizeInt = System.Convert.ToInt32(puzzleSize);
            int rotation90 = 90;
            int rotation180 = 180;
            int rotation270 = 270;
            Random r = new Random();
            ArrayList buttonList = new ArrayList();
            
            for (int i = 0; i < puzzleSizeInt; i++)
            {

                for (int j = 0; j < puzzleSizeInt; j++)
                {

                    ImageButton button = new ImageButton();
                    int mathedValue = App.screenWidth * 3 / (puzzleSizeInt * 10)*puzzleSizeInt;
                    MathedValue.Text = mathedValue.ToString();
                    ScreenValue.Text = App.screenWidth.ToString();
                    Console.WriteLine(App.screenWidth);
                    Console.WriteLine(App.screenWidth * 3 / (puzzleSizeInt * 10));
                    myGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    myGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Source = ImageSource.FromFile("grommash.png");
                    button.Aspect = Aspect.AspectFill;
                    myGrid.Children.Add(button);
                    int rInt = r.Next(0, 2);
                    switch(rInt)
                    {
                        case 0:
                            button.RotateTo(rotation90);
                            break;
                        case 1:
                            button.RotateTo(rotation180);
                            break;
                        case 2:
                            button.RotateTo(rotation270);
                            break;
                        default:   
                            break;
                    }
                }
               
            }
        }
        void SwapImage(object sender, EventArgs e)
        {
            Button button = (Button)sender;

        }

    }
}
