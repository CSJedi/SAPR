using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace WpfCADDesigner.Model.CutromCads
{
    public sealed class TrialngleCad  : CadModel
    {
        public override void DrawVisualModel()
        {
            var meshBuilder = new MeshBuilder(false,false);
            var point0 = new Point3D( CenterPoint.X + SizeX / 2, CenterPoint.Y + SizeY/2, CenterPoint.Z);
            var point1 = new Point3D(CenterPoint.X - SizeX/2, CenterPoint.Y - SizeY/2, CenterPoint.Z);
            var point2 = new Point3D(CenterPoint.X - SizeX/2 , CenterPoint.Y + SizeY/2 , CenterPoint.Z);
            var point3 = point0 + new Vector3D(0, 0, SizeZ);
            var point4 = point1 + new Vector3D(0, 0, SizeZ);
            var point5 = point2 + new Vector3D(0, 0, SizeZ);

            meshBuilder.AddTriangle(point0,point1,point2);
            meshBuilder.AddTriangle(point3, point4, point5);

            meshBuilder.AddQuad(point1,point4,point3,point0);
            meshBuilder.AddQuad(point1, point4, point5, point2);
            meshBuilder.AddQuad(point2, point5, point3, point0);

            var model = new GeometryModel3D(){Geometry = meshBuilder.ToMesh(), BackMaterial = new DiffuseMaterial(Brushes.GreenYellow), Material = new DiffuseMaterial(Brushes.GreenYellow) };
            VisualModel = model;
        }

        public TrialngleCad(Point3D centrPoint) : base()
        {
            CenterPoint = centrPoint;
           DrawVisualModel();   
        }
        [CanUserSetProperty("Розмір по осі Х")]
        public double SizeX
        {
            get { return _sizeX; }
            set
            {
                _sizeX = value;
                DrawVisualModel();
            }
        }

        [CanUserSetProperty("Розмір по осі Y")]
        public double SizeY
        {
            get { return _sizeY; }
            set
            {
                _sizeY = value;
                DrawVisualModel();
            }
        }

        [CanUserSetProperty("Розмір по осі Z")]
        public double SizeZ
        {
            get { return _sizeZ; }
            set
            {
                _sizeZ = value;
                DrawVisualModel();
            }
        }
        private double _sizeX = 10;
        private double _sizeY = 10;
        private double _sizeZ = 10;

    }
}
