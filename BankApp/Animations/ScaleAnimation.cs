namespace BankApp.Animations;
public class ScaleAnimation : TriggerAction<VisualElement>
{
    public uint Duration { get; set; } = 400;
    public double Scale { get; set; } = 0.95;

    protected override async void Invoke(VisualElement sender)
    {
        try 
        {
            await sender.ScaleTo(Scale, Duration/2, Easing.SinOut);
            await sender.ScaleTo(1.0, Duration/2, Easing.SinIn);
            await sender.ScaleTo(Scale * 1.05, Duration/4, Easing.SinOut);
            await sender.ScaleTo(1.0, Duration/4, Easing.SinIn);
        }
        catch 
        {
            sender.Scale = 1.0;
        }
    }
}