using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JubilantWaffle
{
    public partial class App : Application
    {
        //variables that allows access to the screen width and height 
        public static int screenHeight, screenWidth;
        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}
