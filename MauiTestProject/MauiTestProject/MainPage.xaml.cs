using System.Diagnostics;

namespace MauiTestProject;

public partial class MainPage : ContentPage
{
    private int Threshold = 5;
    private bool ButtonDown;

    public MainPage()
    {
        InitializeComponent();
    }

    private void ButtonPressed(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        ButtonDown = true;
        button.ScaleTo(.9, 100, Easing.SinInOut);
    }

    private void ButtonReleased(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        ButtonDown = false;
        button.ScaleTo(1, 100, Easing.SinInOut);

        button.TranslationX = 0;
        button.TranslationY = 0;
    }

    private void Moved(object sender, PointerEventArgs e)
    {
        Button button = (Button)sender;
        Point position = e.GetPosition(button).Value;

        if (ButtonDown)
        {
            Task.Delay(20);
            double centerX = Math.Round((position.X - button.Width / 2) / 4, 0, MidpointRounding.ToZero);
            double centerY = Math.Round((position.Y - button.Height / 2) / 4, 0, MidpointRounding.ToZero);

            double distanceSquared = centerX * centerX + centerY * centerY;
            double thresholdSquared = Threshold * Threshold;

            if (distanceSquared > thresholdSquared)
            {
                double angle = Math.Atan2(centerY, centerX);
                double thresholdX = Math.Cos(angle) * Threshold;
                double thresholdY = Math.Sin(angle) * Threshold;

                centerX = thresholdX;
                centerY = thresholdY;
            }

            button.TranslationX = centerX;
            button.TranslationY = centerY;

            Debug.WriteLine($"{button.TranslationX}, {button.TranslationY}");
        }
    }
}