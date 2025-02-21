namespace BankApp.Behaviors;

public class TapToBounceBehavior : Behavior<View>
{
    protected override void OnAttachedTo(View bindable)
    {
        base.OnAttachedTo(bindable);
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnTapped;
        bindable.GestureRecognizers.Add(tapGesture);
    }

    protected override void OnDetachingFrom(View bindable)
    {
        base.OnDetachingFrom(bindable);
        // Remove the gesture recognizer if needed
    }

    private async void OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is View view)
        {
            await view.ScaleTo(1.1, 100, Easing.BounceIn);
            await view.ScaleTo(1.0, 100, Easing.BounceOut);
        }
    }
}

