namespace com.github.UnityWorkshop.furious_poultry.application.interfaces
{
    public interface IInputProvider
    {
        bool PressedKeySwitchPoultryPrevious { get; }
        bool PressedKeySwitchPoultryNext { get; }
        bool PressedKeyResetPoultry { get; }
        bool PressedKeyShoot { get; }
    }
}