namespace MauiTestProject.Controls;

public class PanButton : Button
{
    private bool isButtonPressed;
    private bool isWithinThreshold;
    private CancellationTokenSource cancellationTokenSource;

    public static readonly BindableProperty ThresholdProperty =
    BindableProperty.Create(
        nameof(Threshold),
        typeof(int),
        typeof(PanButton),
        defaultValue: 4,
        coerceValue: (bindable, value) => Math.Clamp((int)value, 0, 30));

    public static readonly BindableProperty SensitivityProperty =
    BindableProperty.Create(
        nameof(Sensitivity),
        typeof(double),
        typeof(PanButton),
        defaultValue: 1.0,
        coerceValue: (bindable, value) => Math.Clamp((double)value, 0.01, 5.0));

    public int Threshold
    {
        get => (int)GetValue(ThresholdProperty);
        set => SetValue(ThresholdProperty, value);
    }

    public double Sensitivity
    {
        get => (double)GetValue(SensitivityProperty);
        set => SetValue(SensitivityProperty, value);
    }

    public Color OriginalColor;

    public PanButton()
    {
        PanGestureRecognizer panGestureRecognizer = new();
        panGestureRecognizer.PanUpdated += PanUpdated;
        GestureRecognizers.Add(panGestureRecognizer);

        Pressed += ButtonPressed;
        Released += ButtonReleased;
    }

    private Color SetColour(float darknessFactor = 0.8f)
    {
        if (OriginalColor.Alpha == 0)
            return OriginalColor;

        OriginalColor.ToHsl(out float hue, out float saturation, out float luminosity);
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
            OriginalColor = button.BackgroundColor;
            button.BackgroundColor = SetColour();
            isButtonPressed = true;
        }

        Element parentScrollContainer = GetParentScrollView(button);

        if (parentScrollContainer != null)
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Delay(500, cancellationTokenSource.Token).ContinueWith(_ =>
            {
                if (isButtonPressed)
                {
                    Dispatcher.Dispatch(() =>
                    {
                        ButtonReleased(button, EventArgs.Empty);
                    });
                }
            });
        }
    }

    public static Element GetParentScrollView(Element element)
    {
        Element parent = element.Parent;

        while (parent != null)
        {
            if (parent is ScrollView || parent is CollectionView)
            {
                return parent;
            }

            parent = parent.Parent;
        }

        return null;
    }

    private void ButtonReleased(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        button.ScaleTo(1, 100, Easing.SinInOut);
        isButtonPressed = false;

        if (OriginalColor != null)
            button.BackgroundColor = OriginalColor;

        cancellationTokenSource?.Cancel();
    }

    private void PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        Button button = (Button)sender;
        Point position = new(e.TotalX, e.TotalY);

        if (e.StatusType == GestureStatus.Canceled || e.StatusType == GestureStatus.Completed)
        {
            if (isButtonPressed)
            {
                isButtonPressed = false;
                button.ScaleTo(1, 100, Easing.SinInOut);
                button.BackgroundColor = OriginalColor;

                if (isWithinThreshold)
                {
                    button.SendClicked();
                }
            }

            button.TranslateTo(0, 0);
        }

        if (isButtonPressed)
        {
            double centerX = position.X * Sensitivity;
            double centerY = position.Y * Sensitivity;

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

