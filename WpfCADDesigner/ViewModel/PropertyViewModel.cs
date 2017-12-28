using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using WpfCADDesigner.Model;

namespace WpfCADDesigner.ViewModel
{
    public class PropertyViewModel : ObservableObject
    {
        private object _value;
        public string Name { get; set; }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                var v = Convert.ChangeType(value, Property.PropertyType);
                Property.SetValue(CadModel,v);
            }
        }

        public PropertyInfo Property { get; }
        public PropertyViewModel(CadModel cadModel, PropertyInfo field)
        {
            CadModel = cadModel;
            
            Property = field;
           Name =  (field.GetCustomAttributes().First(a=> a.GetType() == typeof(CanUserSetPropertyAttribute)) as CanUserSetPropertyAttribute).DisplayName;         
           Value = field.GetValue(cadModel);
        }
        public CadModel CadModel { get; }
    }
}
