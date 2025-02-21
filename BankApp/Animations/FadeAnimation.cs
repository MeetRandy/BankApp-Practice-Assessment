namespace BankApp.Animations;
public class FadeAnimation : TriggerAction<VisualElement>
{
    protected override async void Invoke(VisualElement sender)
    {
        await sender.FadeTo(0, 200);
        await sender.FadeTo(1, 400);
    }
}