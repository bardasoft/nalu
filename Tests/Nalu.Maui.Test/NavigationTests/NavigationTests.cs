using CommunityToolkit.Mvvm.ComponentModel;

namespace Nalu.Maui.Test.NavigationTests;

public class NavigationTests
{
    private class SomePageModel : ObservableObject;

    [Fact(DisplayName = "Relative navigation, equals to another relative navigation with same segments")]
    public void RelativeNavigationEqualsToAnotherRelativeNavigationWithSameSegments()
    {
        var r1 = Navigation.Relative().Pop().Pop().Push<SomePageModel>();
        var r2 = Navigation.Relative().Pop().Pop().Push<SomePageModel>();
        var r3 = Navigation.Relative().Pop().Push<SomePageModel>();

        r1.Matches(r2).Should().BeTrue();
        r1.Matches(r3).Should().BeFalse();
    }

    [Fact(DisplayName = "Relative navigation with intent, equals to another relative navigation with same segments")]
    public void RelativeNavigationWithIntentEqualsToAnotherRelativeNavigationWithSameSegments()
    {
        var intent = "Hello";
        var r1 = Navigation.Relative().Push<SomePageModel>().WithIntent(intent);
        var r2 = Navigation.Relative().Push<SomePageModel>().WithIntent(intent);
        var r3 = Navigation.Relative().Push<SomePageModel>();

        r1.Matches(r2).Should().BeTrue();
        r1.Matches(r3).Should().BeFalse();
    }

    [Fact(DisplayName = "Absolute navigation, equals to another relative navigation with same segments")]
    public void AbsoluteNavigationEqualsToAnotherRelativeNavigationWithSameSegments()
    {
        var a1 = Navigation.Absolute().Root<SomePageModel>();
        var a2 = Navigation.Absolute().Root<SomePageModel>();
        var a3 = Navigation.Absolute().Root<SomePageModel>().Add<SomePageModel>();

        a1.Matches(a2).Should().BeTrue();
        a1.Matches(a3).Should().BeFalse();
    }

    [Fact(DisplayName = "Absolute navigation with intent, equals to another relative navigation with same segments")]
    public void AbsoluteNavigationWithIntentEqualsToAnotherRelativeNavigationWithSameSegments()
    {
        const string intent = "Hello";
        var a1 = Navigation.Absolute().Root<SomePageModel>().WithIntent(intent);
        var a2 = Navigation.Absolute().Root<SomePageModel>().WithIntent(intent);
        var a3 = Navigation.Absolute().Root<SomePageModel>();

        a1.Matches(a2).Should().BeTrue();
        a1.Matches(a3).Should().BeFalse();
    }

    [Fact(DisplayName = "Relative navigation, when pop follows push, throws exception")]
    public void RelativeNavigationWhenPopFollowsPushThrowsException()
    {
        var r = (IRelativeNavigationInitialBuilder) Navigation.Relative().Push<SomePageModel>();

        Action addPop = () => r.Pop();

        addPop.Should().Throw<InvalidOperationException>();
    }

    [Fact(DisplayName = "Relative navigation, when inserting push before pop, throws exception")]
    public void RelativeNavigationWhenInsertingPushBeforePopThrowsException()
    {
        var r = (Navigation) Navigation.Relative().Pop();

        var addPop = () => r.Insert(0, (NavigationSegment) typeof(SomePageModel));

        addPop.Should().Throw<InvalidOperationException>();
    }

    [Fact(DisplayName = "Relative navigation, when inserting pop after pop, throws exception")]
    public void RelativeNavigationWhenInsertingPopAfterPopThrowsException()
    {
        var r = (Navigation) Navigation.Relative().Push<SomePageModel>();

        var addPop = () => r.Insert(1, new NavigationPop());

        addPop.Should().Throw<InvalidOperationException>();
    }

    [Fact(DisplayName = "Absolute navigation, when inserting pop, throws exception")]
    public void AbsoluteNavigationWhenInsertingPopThrowsException()
    {
        var a = (Navigation) Navigation.Absolute();

        var addPop = () => a.Insert(0, new NavigationPop());

        addPop.Should().Throw<InvalidOperationException>();
    }

    [Fact(DisplayName = "Absolute navigation, when adding pop, throws exception")]
    public void AbsoluteNavigationWhenAddingPopThrowsException()
    {
        var a = (Navigation) Navigation.Absolute();

        var addPop = () => a.Add(new NavigationPop());

        addPop.Should().Throw<InvalidOperationException>();
    }
}
