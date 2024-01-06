namespace Nalu.Maui.Sample.PageModels;

using CommunityToolkit.Mvvm.ComponentModel;

public class TwoPageModel : ObservableObject
{
    private static int _instanceCount;

    public int InstanceCount { get; } = Interlocked.Increment(ref _instanceCount);
}
