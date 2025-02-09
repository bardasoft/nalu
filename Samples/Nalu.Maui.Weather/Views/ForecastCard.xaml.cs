using Nalu.Maui.Weather.Resources;

namespace Nalu.Maui.Weather.Views;

public partial class ForecastCard
{
    public ForecastCard()
    {
        InitializeComponent();
    }

    private void ToggleExpander(object? sender, EventArgs e)
    {
        Expander.IsExpanded = !Expander.IsExpanded;
        ExpanderButton.Text = Expander.IsExpanded ? Texts.ShowLess : Texts.ShowMore;
    }
}
