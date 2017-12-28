using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfCADDesigner.Model
{
    public class CadCollection : ObservableCollection<CadModel>
    {
        public Model3D ToModel3D()
        {
            var model = new Model3DGroup();
            foreach (var cadModel in Items)
            {
                if(cadModel.VisualModel != null)
                model.Children.Add(cadModel.VisualModel);
            }
            return model;
        }
    }
}
