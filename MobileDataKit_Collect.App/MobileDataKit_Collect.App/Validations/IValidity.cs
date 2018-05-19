namespace MobileDataKit_Collect.App.Validations
{
    public interface IValidity
    {
        bool IsValid { get; set; }

        bool Validate();
    }
}
