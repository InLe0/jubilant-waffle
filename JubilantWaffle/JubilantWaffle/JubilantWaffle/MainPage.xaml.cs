using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;


namespace JubilantWaffle
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ImageButton button;
        int ButtonCount;
        Piece piece = new Piece();
        List<Piece> fragments = new List<Piece>();
        List<ImageButton> buttsList = new List<ImageButton>();
        Random r = new Random();
        public int singleDad;
        public MainPage()
        {
            InitializeComponent();
            puzzleSizeButton.Clicked += SetSizeButton;
            singleDad = 420;
        }


        void SetSizeButton(object sender, EventArgs e)
        {
            string puzzleSize = puzzleSizeEnt.Text;
            int puzzleSizeInt = Convert.ToInt32(puzzleSize);
            fragments = piece.Shatter(puzzleSizeInt);

            for (int i = 0; i < puzzleSizeInt; i++)
            {
                for (int j = 0; j < puzzleSizeInt; j++)
                {

                    button = new ImageButton();

                    button.Clicked += ButtonDoubleTap;
                    int mathedValue = App.screenWidth * 3 / (puzzleSizeInt * 10) * puzzleSizeInt;
                    MathedValue.Text = mathedValue.ToString();
                    ScreenValue.Text = App.screenWidth.ToString();
                    myGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    myGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    button.Aspect = Aspect.AspectFill;
                    buttsList.Add(button);
                    button.Source = ImageSource.FromFile((fragments[buttsList.Count - 1].filePath));
                    button.RotateTo(fragments[buttsList.Count - 1].Orientation);
                    myGrid.Children.Add(button);
                }
            }
        }
        void ButtonDoubleTap(object sender, EventArgs e)
        {
            button = (ImageButton)sender;
            if (ButtonCount < 1)
            {
                TimeSpan tt = new TimeSpan(0, 0, 0, 0, 200);
                Device.StartTimer(tt, TestHandleFunc);
            }
            ButtonCount++;

        }
        bool TestHandleFunc()
        {
            if (ButtonCount > 1)
            {
                int btnNum = buttsList.IndexOf(button);
                fragments[btnNum].Orientation += 90;
                button.RotateTo(fragments[btnNum].Orientation);
                fragments[btnNum].Orientation %= 360;
                chickenDinner();
            }
            else
            {
                int btnNum = buttsList.IndexOf(button);
                if (singleDad != 420 && singleDad != btnNum)
                {
                    String bluePath = fragments[btnNum].filePath;
                    int blueOrientation = fragments[btnNum].Orientation;
                    int bluePosition = fragments[btnNum].DesiredPosition;

                    fragments[btnNum].filePath = fragments[singleDad].filePath;
                    fragments[btnNum].Orientation = fragments[singleDad].Orientation;
                    fragments[btnNum].DesiredPosition = fragments[singleDad].DesiredPosition;

                    fragments[singleDad].filePath = bluePath;
                    fragments[singleDad].Orientation = blueOrientation;
                    fragments[singleDad].DesiredPosition = bluePosition;

                    buttsList[btnNum].Source = fragments[btnNum].filePath;
                    buttsList[btnNum].RotateTo(fragments[btnNum].Orientation);
                    buttsList[singleDad].Source = fragments[singleDad].filePath;
                    buttsList[singleDad].RotateTo(fragments[singleDad].Orientation);

                    singleDad = 420;
                }
                else if (singleDad == 420)
                {
                    singleDad = btnNum;
                }
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
                    actualPosition = fragments.IndexOf(piece);
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
