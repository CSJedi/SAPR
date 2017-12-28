using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace WpfCADDesigner.Model.CutromCads
{
    public sealed class CylinderCad : CadModel
    {
        private readonly Point3D _p1;
        private readonly Point3D _p2;

        public override void DrawVisualModel()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddCylinder(_p1, _p2, Width/2,32,false,false);

            var model = new GeometryModel3D() { Geometry = meshBuilder.ToMesh(), BackMaterial = new DiffuseMaterial(Brushes.Red), Material = new DiffuseMaterial(Brushes.Red) };
            VisualModel = model;
        }
        public CylinderCad(Point3D p1, Point3D p2) : base()
        {
            _p1 = p1;
            _p2 = p2;
            DrawVisualModel();
        }

        [CanUserSetProperty("Діаметер")]
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                DrawVisualModel();
            }
        }

        private double _height = 10;
        private double _width = 10;

    }
}
