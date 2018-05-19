using Xamarin.Forms;

namespace MobileDataKit_Collect.App.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(object bindableObject,T value);
    }
}
