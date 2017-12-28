using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace WpfCADDesigner.Model.CutromCads
{
    public sealed class RectangleCad : CadModel
    {
        private double _sizeA = 10;
        private double _sizeB = 10;
        private double _height = 10;
        private double _thickness = 10;

        public override void DrawVisualModel()
        {
            var meshBuilder = new MeshBuilder(false, false);
            var point0 = new Point3D(CenterPoint.X + SizeA / 2, CenterPoint.Y + Thickness/2, CenterPoint.Z + Height/2);
            var point1 = new Point3D(CenterPoint.X - SizeA / 2, CenterPoint.Y + Thickness / 2, CenterPoint.Z + Height / 2);
            var point2 = new Point3D(CenterPoint.X - SizeB / 2, CenterPoint.Y + Thickness / 2, CenterPoint.Z - Height / 2);
            var point3 = new Point3D(CenterPoint.X + SizeB / 2, CenterPoint.Y + Thickness / 2, CenterPoint.Z - Height / 2);

            var point4 = new Point3D(CenterPoint.X + SizeA / 2, CenterPoint.Y - Thickness/2, CenterPoint.Z + Height / 2);
            var point5 = new Point3D(CenterPoint.X - SizeA / 2, CenterPoint.Y - Thickness/2, CenterPoint.Z + Height / 2);
            var point6 = new Point3D(CenterPoint.X - SizeB / 2, CenterPoint.Y - Thickness/2, CenterPoint.Z - Height / 2);
            var point7 = new Point3D(CenterPoint.X + SizeB / 2, CenterPoint.Y - Thickness/2, CenterPoint.Z - Height / 2);

            meshBuilder.AddQuad(point0,point1,point2,point3);
            meshBuilder.AddQuad(point4, point5, point6, point7);

            meshBuilder.AddQuad(point1, point2, point6, point5);
            meshBuilder.AddQuad(point2, point3, point7, point6);

            meshBuilder.AddQuad(point0, point4, point7, point3);
            meshBuilder.AddQuad(point0, point1, point5, point4);
            
            var model = new GeometryModel3D() { Geometry = meshBuilder.ToMesh(), BackMaterial = new DiffuseMaterial(Brushes.Red), Material = new DiffuseMaterial(Brushes.Red) };
            VisualModel = model;
        }
        public RectangleCad(Point3D centrPoint) : base()
        {
            CenterPoint = centrPoint;
            DrawVisualModel();
        }

        [CanUserSetProperty("Розмір верху")]
        public double SizeA
        {
            get { return _sizeA; }
            set
            {
                _sizeA = value; 
                DrawVisualModel();
            }
        }

        [CanUserSetProperty("Розмір низу")]
        public double SizeB
        {
            get { return _sizeB; }
            set
            {
                _sizeB = value;
                DrawVisualModel();
            }
        }

        [CanUserSetProperty("Висота")]
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                DrawVisualModel();
            }
        }

        [CanUserSetProperty("Товщина")]
        public double Thickness
        {
            get { return _thickness; }
            set
            {
                _thickness = value;
                DrawVisualModel();
            }
        }
    }
}
