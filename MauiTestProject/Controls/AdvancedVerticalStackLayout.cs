namespace MauiTestProject.Controls;

public class AdvancedVerticalStackLayout : VerticalStackLayout
{
    protected override void OnChildAdded(Element child)
    {
        base.OnChildAdded(child);

        if (child is VisualElement visualChild)
        {
            //var descendants = visualChild.GetVisualTreeDescendants().Where(descendant => descendant is VisualElement);

            //foreach( var descendant in descendants)
            //{
            //    if (descendant is VisualElement visualDescendant)
            //    {
            //        HeightRequest += visualDescendant.HeightRequest;
            //    }
            //}

            HeightRequest += visualChild.HeightRequest;
        }
    }

    protected override void OnChildRemoved(Element child, int oldLogicalIndex)
    {
        base.OnChildRemoved(child, oldLogicalIndex);

        if (child is VisualElement visualChild)
        {
            //var descendants = visualChild.GetVisualTreeDescendants().Where(descendant => descendant is VisualElement);

            //foreach (var descendant in descendants)
            //{
            //    if (descendant is VisualElement visualDescendant)
            //    {
            //        HeightRequest -= visualDescendant.HeightRequest;
            //    }
            //}

            HeightRequest -= visualChild.HeightRequest;
        }
    }
}
