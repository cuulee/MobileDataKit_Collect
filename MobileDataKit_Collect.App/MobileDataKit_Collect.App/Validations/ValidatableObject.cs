
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MobileDataKit_Collect.App.Validations
{
    public class ValidatableObject<T> : ViewModels.ExtendedBindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
		private List<string> _errors;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

		public List<string> Errors
		{
			get
			{
				return _errors;
			}
			set
			{
				_errors = value;
				RaisePropertyChanged(() => Errors);
			}
		}

        

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }
        BindableObject BindableObject;
        string PropertyName;
        public ValidatableObject(BindableObject bindableObject,string propertyName)
        {
           
          
            this.PropertyName = propertyName;
            this.BindableObject = bindableObject;
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();
            var obj = this.BindableObject.BindingContext as ViewModels.IBaseViewModel;
            if(obj !=null)
            {
                var sds = Csla.Reflection.MethodCaller.CallPropertyGetter(obj.GetModel(), this.PropertyName);
                IEnumerable<string> errors = _validations.Where(v => !v.Check(this.BindableObject, (T)sds))
                    .Select(v => v.ValidationMessage);

                Errors = errors.ToList();
                IsValid = !Errors.Any();
            }
          

            return this.IsValid;
        }
    }
}
