namespace MauiTestProject.Views;

public partial class CustomButtons : ContentPage
{
    //private readonly int Threshold = 10;
    //private bool IsButtonPressed;
    //private bool IsWithinThreshold;

    public CustomButtons()
    {
        InitializeComponent();
    }

    //private void ButtonPressed(object sender, EventArgs e)
    //{
    //    Button button = (Button)sender;

    //    if (!IsButtonPressed)
    //    {
    //        button.ScaleTo(0.9, 100, Easing.SinInOut);
    //        IsButtonPressed = true;
    //    }
    //}

    //private void ButtonReleased(object sender, EventArgs e)
    //{
    //    Button button = (Button)sender;
    //    button.ScaleTo(1, 100, Easing.SinInOut);
    //    IsButtonPressed = false;
    //}

    //private void PanUpdated(object sender, PanUpdatedEventArgs e)
    //{
    //    Button button = (Button)sender;
    //    Point position = new(e.TotalX, e.TotalY);

    //    if (e.StatusType == GestureStatus.Started)
    //    {
    //        button.ScaleTo(0.9, 100, Easing.SinInOut);
    //    }
    //    else if (e.StatusType == GestureStatus.Canceled || e.StatusType == GestureStatus.Completed)
    //    {
    //        if (IsButtonPressed)
    //        {
    //            button.ScaleTo(1, 100, Easing.SinInOut);
    //            IsButtonPressed = false;

    //            if (IsWithinThreshold)
    //            {
    //                button.SendClicked();
    //            }
    //        }

    //        button.TranslateTo(0, 0);
    //    }

    //    if (IsButtonPressed)
    //    {
    //        double centerX = (position.X) / 4;
    //        double centerY = (position.Y) / 4;

    //        double distanceSquared = centerX * centerX + centerY * centerY;
    //        double thresholdSquared = Threshold * Threshold;

    //        if (distanceSquared > thresholdSquared)
    //        {
    //            double angle = Math.Atan2(centerY, centerX);
    //            double thresholdX = Math.Cos(angle) * Threshold;
    //            double thresholdY = Math.Sin(angle) * Threshold;

    //            centerX = thresholdX;
    //            centerY = thresholdY;
    //        }

    //        if (distanceSquared <= thresholdSquared)
    //            IsWithinThreshold = true;
    //        else
    //            IsWithinThreshold = false;

    //        button.TranslationX = centerX;
    //        button.TranslationY = centerY;
    //    }

    //    Debug.WriteLine($"{button.TranslationX}, {button.TranslationY}");
    //}

    private void ButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Notification", "The button was clicked!", "Ok");
    }

    #region Old Concept

    //private void ButtonReleased(object sender, EventArgs e)
    //{
    //    Button button = (Button)sender;
    //    button.ScaleTo(1, 100, Easing.SinInOut);

    //    button.TranslationX = 0;
    //    button.TranslationY = 0;
    //}

    //private void Moved(object sender, PointerEventArgs e)
    //{
    //    Button button = (Button)sender;
    //    Point position = e.GetPosition(button).Value;

    //    if (ButtonDown)
    //    {
    //        double centerX = Math.Round((position.X - button.Width / 2), 0, MidpointRounding.ToZero);
    //        double centerY = Math.Round((position.Y - button.Height / 2), 0, MidpointRounding.ToZero);

    //        double distanceSquared = centerX * centerX + centerY * centerY;
    //        double thresholdSquared = Threshold * Threshold;

    //        if (distanceSquared > thresholdSquared)
    //        {
    //            double angle = Math.Atan2(centerY, centerX);
    //            double thresholdX = Math.Cos(angle) * Threshold;
    //            double thresholdY = Math.Sin(angle) * Threshold;

    //            centerX = thresholdX;
    //            centerY = thresholdY;
    //        }

    //        button.TranslationX = centerX;
    //        button.TranslationY = centerY;

    //        Debug.WriteLine($"{button.TranslationX}, {button.TranslationY}");
    //    }
    //}

    #endregion
}