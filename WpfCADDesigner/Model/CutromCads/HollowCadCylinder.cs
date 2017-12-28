using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace WpfCADDesigner.Model.CutromCads
{
    public sealed class HollowCadCylinder : CadModel
    {
        private readonly Point3D _p1;
        private readonly Point3D _p2;

        public override void DrawVisualModel()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddCylinder(_p1, _p2, BigDiameter / 2, 32, false, false);
            meshBuilder.AddCylinder(_p1, _p2, InnerDiameter / 2, 32, false, false);
            BuildCap(meshBuilder);
            var model = new GeometryModel3D() { Geometry = meshBuilder.ToMesh(), BackMaterial = new DiffuseMaterial(Brushes.Red), Material = new DiffuseMaterial(Brushes.Red) };
            VisualModel = model;
        }

        private void BuildCap(MeshBuilder meshBuilder)
        {
            var bigCircle = CreatePoints(CenterPoint, 40, BigDiameter / 2, (_p1 - _p2).Length);
            var innerCircle = CreatePoints(CenterPoint, 40, InnerDiameter / 2, (_p1 - _p2).Length);

            for (int i = 0; i < bigCircle.Count; i++)
            {
                if (i != bigCircle.Count - 1)
                {
                    meshBuilder.AddTriangle(bigCircle[i], innerCircle[i], innerCircle[i + 1]);
                    meshBuilder.AddTriangle(bigCircle[i], bigCircle[i + 1], innerCircle[i + 1]);
                }
                else
                {
                    meshBuilder.AddTriangle(bigCircle[i], innerCircle[i], innerCircle[0]);
                    meshBuilder.AddTriangle(bigCircle[0], bigCircle[i], innerCircle[0]);
                }
            }
        }
        private IList<Point3D> CreatePoints(Point3D center, int div, double radius, double height)
        {
            var list = new List<Point3D>();
            double x;
            double z;

            double angle = 0;

            for (int i = 0; i < (div + 1); i++)
            {
                x = Math.Sin(angle) * radius;
                z = Math.Cos(angle) * radius;

                list.Add(new Point3D(x, center.Y + height, z));

                angle += 2 * Math.PI / div;
            }
            return list;
        }
        public HollowCadCylinder(Point3D p1, Point3D p2) : base()
        {
            _p1 = p1;
            _p2 = p2;
            DrawVisualModel();
        }


        [CanUserSetProperty("Зовнішній діаметер")]
        public double BigDiameter
        {
            get { return _bigDiameter; }
            set
            {
                _bigDiameter = value;
                DrawVisualModel();
            }
        }
        [CanUserSetProperty("Внутрішній діаметер")]
        public double InnerDiameter
        {
            get { return _innerDiameter; }
            set
            {
                _innerDiameter = value;
                DrawVisualModel();
            }
        }
        private double _innerDiameter = 10;
        private double _bigDiameter = 10;

    }
}