namespace MauiTestProject.Controls;

public class PanButton : Button
{
    private bool isButtonPressed;
    private bool isWithinThreshold;
    private Color originalColor;

    public static readonly BindableProperty ThresholdProperty =
    BindableProperty.Create(
        nameof(Threshold),
        typeof(int),
        typeof(PanButton),
        defaultValue: 4,
        coerceValue: (bindable, value) => Math.Clamp((int)value, 0, 30));

    //public static readonly BindableProperty SensitivityProperty =
    //BindableProperty.Create(
    //    nameof(Sensitivity),
    //    typeof(double),
    //    typeof(PanButton),
    //    defaultValue: 1,
    //    coerceValue: (bindable, value) => Math.Clamp((double)value, 0.1, 5));

    public int Threshold
    {
        get => (int)GetValue(ThresholdProperty);
        set => SetValue(ThresholdProperty, value);
    }

    //public double Sensitivity
    //{
    //    get => (double)GetValue(SensitivityProperty);
    //    set => SetValue(SensitivityProperty, value);
    //}

    public PanButton()
    {
        PanGestureRecognizer panGestureRecognizer = new();
        panGestureRecognizer.PanUpdated += PanUpdated;
        GestureRecognizers.Add(panGestureRecognizer);

        Pressed += ButtonPressed;
        Released += ButtonReleased;
    }

    private static Color SetColour(Color originalColour, float darknessFactor = 0.8f)
    {
        if (originalColour.Alpha == 0)
            return originalColour;

        originalColour.ToHsl(out float hue, out float saturation, out float luminosity);
        Color darkenedColour = Color.FromHsla(hue, saturation, luminosity * darknessFactor);
        return darkenedColour;
    }

    private void ButtonPressed(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        if (!isButtonPressed)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            button.ScaleTo(0.9, 100, Easing.SinInOut);
            originalColor = button.BackgroundColor;
            button.BackgroundColor = SetColour(button.BackgroundColor);
            isButtonPressed = true;
        }
    }

    private void ButtonReleased(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        button.ScaleTo(1, 100, Easing.SinInOut);
        button.BackgroundColor = originalColor;
        isButtonPressed = false;
    }

    private void PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        Button button = (Button)sender;
        Point position = new(e.TotalX, e.TotalY);

        if (e.StatusType == GestureStatus.Canceled || e.StatusType == GestureStatus.Completed)
        {
            if (isButtonPressed)
            {
                button.ScaleTo(1, 100, Easing.SinInOut);
                button.BackgroundColor = originalColor;

                isButtonPressed = false;

                if (isWithinThreshold)
                {
                    button.SendClicked();
                }
            }

            button.TranslateTo(0, 0);
        }

        if (isButtonPressed)
        {
            double centerX = position.X;
            double centerY = position.Y;

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

            if (distanceSquared <= thresholdSquared)
                isWithinThreshold = true;
            else
                isWithinThreshold = false;

            button.TranslationX = centerX;
            button.TranslationY = centerY;
        }


    }
}

