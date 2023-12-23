namespace MauiTestProject.MVVM.Views;

public partial class CustomVerticalStackLayoutPage : ContentPage
{
	public CustomVerticalStackLayoutPage()
	{
		InitializeComponent();
	}

    private void AddClicked(object sender, EventArgs e)
    {
        avsl.Add(new Border()
        {
            BackgroundColor = Color.FromArgb("#3D85C6"),
            WidthRequest = avsl.WidthRequest,
            HeightRequest = 100,
        });
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        if (avsl.Count > 0)
            avsl.RemoveAt(avsl.Count - 1);
    }
}