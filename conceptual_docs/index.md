<h2 id="nalumaui">Nalu.Maui<span></span></h2>

`Nalu.Maui` provides a set of classes to help you with everyday challenges encountered while working with .NET MAUI.

If `Nalu.Maui` is valuable to your work, consider supporting its continued development and maintenance through a donation:

<a target="_blank" href="https://buymeacoffee.com/albyrock87">
    <img src="assets/images/donate.png" style="height:44px">
</a>

---

### Core [![Nalu.Maui.Core NuGet Package](https://img.shields.io/nuget/v/Nalu.Maui.Core.svg)](https://www.nuget.org/packages/Nalu.Maui.Core/) [![Nalu.Maui NuGet Package Downloads](https://img.shields.io/nuget/dt/Nalu.Maui.Core)](https://www.nuget.org/packages/Nalu.Maui.Core/)

The core library is intended to provide a set of common use utilities.

Have you ever noticed that when the user backgrounds the app on iOS, the app is suspended, and the network requests will fail due to `The network connection was lost`?

This is really annoying: it forces us to implement complex retry logic, especially considering that the request may have already hit the server.

To solve this issue, we provide a `NSUrlBackgroundSessionHttpMessageHandler` to be used in your `HttpClient` to allow http request to continue even when the app is in the background.

```csharp
#if IOS
    var client = new HttpClient(new NSUrlBackgroundSessionHttpMessageHandler());
#else
    var client = new HttpClient();
#endif
```

To make this work, you need to change your `AppDelegate` as follows:
```csharp
[Export("application:handleEventsForBackgroundURLSession:completionHandler:")]
public virtual void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
    => NSUrlBackgroundSessionHttpMessageHandler.HandleEventsForBackgroundUrl(application, sessionIdentifier, completionHandler);
```

**Check out the [Core Wiki](core.md) for more information**.

---

### Navigation [![Nalu.Maui.Navigation NuGet Package](https://img.shields.io/nuget/v/Nalu.Maui.Navigation.svg)](https://www.nuget.org/packages/Nalu.Maui.Navigation/) [![Nalu.Maui NuGet Package Downloads](https://img.shields.io/nuget/dt/Nalu.Maui.Navigation)](https://www.nuget.org/packages/Nalu.Maui.Navigation/)

The MVVM navigation service offers a straightforward and robust method for navigating between pages and passing parameters.

The navigation system utilizes `Shell` under the hood, allowing you to easily define the flyout menu, tabs, and root pages.

We use a **fluent API** instead of strings to define navigations, supporting both `Relative` and `Absolute` navigation, including navigation guards to prompt the user before leaving a page.

```csharp
// Push the page registered with the DetailPageModel
await _navigationService.GoToAsync(Navigation.Relative().Push<DetailPageModel>());
// Navigate to the `SettingsPageModel` root page
await _navigationService.GoToAsync(Navigation.Absolute().Root<SettingsPageModel>());
```

Passing parameters is simple and type-safe.

```csharp
// Pop the page and pass a parameter to the previous page model
await _navigationService.GoToAsync(Navigation.Relative().Pop().WithIntent(new MyPopIntent()));
// which should implement `IAppearingAware<MyPopIntent>`
Task OnAppearingAsync(MyPopIntent intent) { ... }
```

You can also define navigation guards to prevent navigation from occurring.

```csharp
ValueTask<bool> CanLeaveAsync() => { ... ask the user };
```

There is an embedded **leak-detector** to help you identify memory leaks in your application.

**See more on the [Navigation Wiki](navigation.md)**.

---

### Layouts [![Nalu.Maui.Layouts NuGet Package](https://img.shields.io/nuget/v/Nalu.Maui.Layouts.svg)](https://www.nuget.org/packages/Nalu.Maui.Layouts/) [![Nalu.Maui NuGet Package Downloads](https://img.shields.io/nuget/dt/Nalu.Maui.Layouts)](https://www.nuget.org/packages/Nalu.Maui.Layouts/)

Cross-platform layouts and utilities for MAUI applications simplify dealing with templates and `BindinginContext` in XAML.

- Have you ever dreamed of having an `if` statement in XAML?
  ```csharp
    <nalu:ToggleTemplate Value="{Binding HasPermission}"
                         WhenTrue="{StaticResource AdminFormTemplate}"
                         WhenFalse="{StaticResource PermissionRequestTemplate}" />
  ```
- Do you want to scope the binding context of a content?
  ```csharp
    <nalu:ViewBox ContentBindingContext="{Binding SelectedAnimal}"
                  IsVisible="{Binding IsSelected}">
        <views:AnimalView x:DataType="models:Animal" />
    </nalu:ViewBox>
  ```
- And what about rendering a `TemplateSelector` directly like we do on a `CollectionView`?
  ```csharp
    <nalu:TemplateBox ContentTemplateSelector="{StaticResource AnimalTemplateSelector}"
                      ContentBindingContext="{Binding CurrentAnimal}" />
  ```

**Find out more on the [Layouts Wiki](layouts.md)**.

---

### Controls [![Nalu.Maui.Controls NuGet Package](https://img.shields.io/nuget/v/Nalu.Maui.Controls.svg)](https://www.nuget.org/packages/Nalu.Maui.Controls/) [![Nalu.Maui NuGet Package Downloads](https://img.shields.io/nuget/dt/Nalu.Maui.Controls)](https://www.nuget.org/packages/Nalu.Maui.Controls/)

The controls library provides a set of cross-platform controls to simplify your development.

- A `InteractableCanvasView` which is a `SkiaSharp` `SKCanvasView` with touch-events support where you can choose to stop touch event propagation to avoid interaction with ancestors (like `ScrollView`)
- A `TimeSpan?` edit control named `DurationWheel` which allows the user to enter a duration by spinning a wheel!!

**Find out more on the [Controls Wiki](controls.md)**.