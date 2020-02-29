using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;


namespace JubilantWaffle
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible (false)]
    public partial class MainPage : ContentPage
    {
        ImageButton button;
        int ButtonCount;
        Piece piece = new Piece();
        List<Piece> fragments = new List<Piece>();
        ArrayList buttonList = new ArrayList();
        Random r = new Random();
        public MainPage()
        {
            InitializeComponent();
            puzzleSizeButton.Clicked += SetSizeButton;
            
        }
        

        void SetSizeButton(object sender, EventArgs e)
        {
            //to fix mergies
            //another mergies fixis

            string puzzleSize = puzzleSizeEnt.Text;
            int puzzleSizeInt = Convert.ToInt32(puzzleSize);
 
            
           
           
            
            fragments = piece.Shatter(puzzleSizeInt);
            for (int i = 0; i < puzzleSizeInt; i++)
            {
                for (int j = 0; j < puzzleSizeInt; j++)
                {

                    button = new ImageButton();
                    
                    button.Clicked += ButtonDoubleTap;
                    int mathedValue = App.screenWidth * 3 / (puzzleSizeInt * 10)*puzzleSizeInt;
                    MathedValue.Text = mathedValue.ToString();
                    ScreenValue.Text = App.screenWidth.ToString();
                    myGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    myGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    
                    button.Aspect = Aspect.AspectFill;
                    buttonList.Add(button);
                    button.Source = ImageSource.FromFile((fragments[buttonList.Count - 1].filePath));
                    button.RotateTo(fragments[buttonList.Count-1].Orientation);
                    myGrid.Children.Add(button);   
                }
            }
        }
        void ButtonDoubleTap(object sender, EventArgs e)
        {
            button = (ImageButton)sender;
            if (ButtonCount < 1)
            {
                TimeSpan tt = new TimeSpan(0, 0, 1);
                Device.StartTimer(tt, TestHandleFunc);
            }
            ButtonCount++;

        }
        bool TestHandleFunc()
        {
            if (ButtonCount > 1)
            {
                int btnNum = buttonList.IndexOf(button);
                fragments[btnNum].Orientation += 90;
                fragments[btnNum].Orientation %= 360;
                button.RotateTo(fragments[btnNum].Orientation);
                //DisplayAlert("", "Two Clicks", "OK");
                chickenDinner();
            }
            else
            {
                //Your action for Single Click here
                 //DisplayAlert("", "One Click", "OK");
            }
            ButtonCount = 0;
            return false;
        }

        void chickenDinner()
        {
            bool weHasNukes = true;
            int actualPosition = 0;
                foreach (Piece piece in fragments)
                {
                    if (!weHasNukes)
                    {
                        break;
                    }
                    if (piece.Orientation != 0)
                    {
                        weHasNukes = false;
                    }
                }
            if (weHasNukes)
            {
                DisplayAlert("", "First Win condition Achieved", "OK");
                foreach (Piece piece in fragments)
                {
                    if (!weHasNukes)
                    {
                        break;
                    }
                    if (piece.DesiredPosition != actualPosition)
                    {
                        weHasNukes = false;
                    }
                    actualPosition++;
                }
            }
            if (weHasNukes)
            {
                DisplayAlert("", "All your base are belong to us", "OK");
            }
        }
    }
}
