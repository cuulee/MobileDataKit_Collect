using Xamarin.Forms;

namespace MobileDataKit_Collect.App.Validations
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        

        public bool Check(object bindableObject, T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }
    }


    public class ComparePropertiesValidation<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public string SecondProperty { get; set; }

        public bool Check(object bindableObject, T value)
        {
            var cd = bindableObject as Xamarin.Forms.BindableObject;
            if (cd != null)
            {
                var cd1 = cd.BindingContext as ViewModels.IBaseViewModel;
                
         
             
            var secondProperty = Csla.Reflection.MethodCaller.CallPropertyGetter(cd1.GetModel(), SecondProperty);
            if ((value == null && secondProperty !=null) || (value != null && secondProperty == null))
            {
                return false;
            }

            var str = value as string;
            var str2 = secondProperty as string;
            if ((string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(str2)) || !string.IsNullOrWhiteSpace(str) && string.IsNullOrWhiteSpace(str2))
                return false;


            if ((str ==null && str2 ==null) ||(str.ToLower() == str2.ToLower()))
                return true;
            }
            return false;
        }
    }





    public class CharactersGreaterThanValidator<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public int NoCharact { get; set; }


        public bool Check(object bindableObject, T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
                return false;

            if (str.Length <= NoCharact)
                return false;
            return true;
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
