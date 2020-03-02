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

        /// <summary>
        /// this method is to set up the puzzle depending on a value between 1 and 5 that is taken through an Entry set up in the XAML, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetSizeButton(object sender, EventArgs e)
        {   
            //getting the value for the size of the puzzle
            string puzzleSize = puzzleSizeEnt.Text;
            try
            {
                //Converted the string of the puzzle size to an int for later use, mainly with ensure that the puzzle does not go over the size of the screen
                int puzzleSizeInt = Convert.ToInt32(puzzleSize);

                //clearing the board to set a new puzzle once the button is pressed so we don't get several puzzles at once
                myGrid.Children.Clear();
                buttsList.Clear();
                fragments.Clear();
                myGrid.RowDefinitions.Clear();
                myGrid.ColumnDefinitions.Clear();
                singleDad = 420;


                fragments = piece.Shatter(puzzleSizeInt);

                //nested for loop to set buttons in the right row and column of the grid
                for (int i = 0; i < puzzleSizeInt; i++)
                {
                    for (int j = 0; j < puzzleSizeInt; j++)
                    {
                        
                        button = new ImageButton();

                        //adding an event for when we double click/double tap an event 
                        button.Clicked += ButtonDoubleTap;


                        //these next two lines are for setting up the size of each button, the formulas are the same and the size of the buttons vary depending on the size of the overall puzzle
                        //hence why we have the puzzleSizeInt in the formula, the App.screenWidth is to make sure that the puzzles will fit regardless of the device that we use, for more information about that 
                        //go to App.cs and MainActivity.cs
                        myGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                        myGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength((App.screenWidth * 3) / (puzzleSizeInt * 10)) });
                        //setting the buttons for each row and column depending on the value of the different loops
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);

                        //getting each of the fragments of pictures to fit to the button 
                        button.Aspect = Aspect.AspectFill;
                        // refer to piece.cs fot more information on those lines, but over all it's just loading the image for each button and giving them a base rotation
                        buttsList.Add(button);
                        button.Source = ImageSource.FromFile((fragments[buttsList.Count - 1].filePath));
                        button.RotateTo(fragments[buttsList.Count - 1].Orientation);
                        myGrid.Children.Add(button);
                    }
                }
            }
            catch (System.FormatException ex)
            {
               
            }
        }
        /// <summary>
        /// this method is to make the double clicking of a button work so when we double click the button does something different than just a single click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonDoubleTap(object sender, EventArgs e)
        {   
            // getting the button that we are currently clicking
            button = (ImageButton)sender;
            
            if (ButtonCount < 1)
            {   
                // setting the interval during which a double click is accepted in milliseconds, any longer than that and it will be considered a single click
                TimeSpan tt = new TimeSpan(0, 0, 0, 0, 200);
                //if the button is double clicked it invokes another method that tells the button what to do once it is double clicked
                Device.StartTimer(tt, TestHandleFunc);
            }
            ButtonCount++;

        }
        bool TestHandleFunc()
        {
            //logic for rotating images
            if (ButtonCount > 1)
            {
                // getting the button relative to the one that was pressed orginally
                int btnNum = buttsList.IndexOf(button);
                //rotates the button by 90 degrees relative to the saved rotation of the button and increment it at the same time
                fragments[btnNum].Orientation += 90;
                button.RotateTo(fragments[btnNum].Orientation);
                // ensuring that the rotation values does not exceed 360 for testing purposes mostly 
                fragments[btnNum].Orientation %= 360;
                //refer to chickenDinner comments, tl;dr winning conditions
                chickenDinner();
            }

            // logic for swapping images
            else
            {

                int btnNum = buttsList.IndexOf(button);
                
                if (singleDad != 420 && singleDad != btnNum)
                {
                    //saving values of the image that was clicked second 
                    string bluePath = fragments[btnNum].filePath;
                    int blueOrientation = fragments[btnNum].Orientation;
                    int bluePosition = fragments[btnNum].DesiredPosition;
                    //changes the variables of the image that was clicked second with the one that was clicked first
                    fragments[btnNum].filePath = fragments[singleDad].filePath;
                    fragments[btnNum].Orientation = fragments[singleDad].Orientation;
                    fragments[btnNum].DesiredPosition = fragments[singleDad].DesiredPosition;
                    //changes of the variables of the first image to the variable of the second image
                    fragments[singleDad].filePath = bluePath;
                    fragments[singleDad].Orientation = blueOrientation;
                    fragments[singleDad].DesiredPosition = bluePosition;
                    //refreshes the images and rotation to which that the images should have
                    buttsList[btnNum].Source = fragments[btnNum].filePath;
                    buttsList[btnNum].RotateTo(fragments[btnNum].Orientation);
                    buttsList[singleDad].Source = fragments[singleDad].filePath;
                    buttsList[singleDad].RotateTo(fragments[singleDad].Orientation);
                   
                    chickenDinner();
                    //sentinel value standing for null, making sure that we will not call a button randomly
                    singleDad = 420;
                }
                //if no button was clicked then the first button is called
                else if (singleDad == 420)
                {
                    singleDad = btnNum;
                }
            }
            // resetting the count to 0
            ButtonCount = 0;
            return false;
        }

        void chickenDinner()
        {
            //keeps track of the victory condtion
            bool weHasNukes = true;
            //current position of the image relative to the button
            int actualPosition = 0;
            //checks if the rotation is 0 degrees if one or more is not 0 then the win condition cannot be achieved
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
                //if any of the pieces is not in the position that they were generated on then the win condition cannot be achieved
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
            //checking if the win conditions have been achieved if so displays a message to the player telling him that he won
            if (weHasNukes)
            {
                DisplayAlert("", "All your base are belong to us", "OK");
            }
        }
    }
}
