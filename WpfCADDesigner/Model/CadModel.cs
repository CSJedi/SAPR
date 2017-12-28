using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using GalaSoft.MvvmLight;

namespace WpfCADDesigner.Model
{
    public abstract class CadModel : ObservableObject
    {
        private Model3D _visualModel;

        private Point3D _centerPoint;

        public Model3D VisualModel
        {
            get
            {
                return _visualModel; 
            }
            set
            {
                _visualModel = value;
                RaisePropertyChanged();
            }
        }

        protected CadModel()
        {
            
        }

       
        public Point3D CenterPoint { get; set; }

        public abstract void DrawVisualModel();
    }

    public class CanUserSetPropertyAttribute : Attribute
    {
        public string DisplayName { get; }
        public CanUserSetPropertyAttribute(string displayName)
        {
            DisplayName = displayName;
        }

    }
}
