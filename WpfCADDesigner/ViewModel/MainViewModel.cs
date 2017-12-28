using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HelixToolkit.Wpf;
using WpfCADDesigner.Model;
using WpfCADDesigner.Model.CutromCads;

namespace WpfCADDesigner.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private CadModel _selectedObject;

        public ObservableCollection<PropertyViewModel> Properties { get; set; } = new ObservableCollection<PropertyViewModel>();
        public ICommand MouseDownCommand { get; set; }
        public ICommand KeyDownCommand { get; set; }
        public Model3D VisualModel
        {
            get { return cadCollection.ToModel3D(); }
        }
        private CadCollection cadCollection = new CadCollection();
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            cadCollection.CollectionChanged += OnCollectionUpdate;
            InitializeCadCollection();
            InitializeCadCommands();

        }

        private void InitializeCadCommands()
        {
            MouseDownCommand = new RelayCommand<MouseEventArgs>(MouseDownCommandExecute);
        }

        private void InitializeCadCollection()
        {
            cadCollection.Add(new RectangleCad(new Point3D(0,0,0)){SizeA = 34, SizeB = 54, Height = 38, Thickness = 6});
            cadCollection.Add(new RectangleCad(new Point3D(0,13,-23)){Height = 8,Thickness = 32, SizeA = 54,  SizeB = 54});
            cadCollection.Add(new RectangleCad(new Point3D(0,13,-42)){Height = 32, SizeA = 8, SizeB = 8, Thickness = 32});
            cadCollection.Add(new HollowCadCylinder(new Point3D(0,0,0), new Point3D(0,10,0)){BigDiameter = 22, InnerDiameter = 14});
        }

        private void OnCollectionUpdate(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => VisualModel);
        }

        public CadModel SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (_selectedObject != null)
                {
                    _selectedObject.PropertyChanged -= UpdateVisual;
                }
                _selectedObject = value;
                if(_selectedObject != null)
                _selectedObject.PropertyChanged += UpdateVisual;
                UpdateProperties();
                RaisePropertyChanged(() => VisualModel);
            }
        }

        private void MouseDownCommandExecute(MouseEventArgs obj)
        {
            var viewport = obj.Source as HelixViewport3D;
            var cursor = viewport.CursorPosition;
            var pointCursor2D = Viewport3DHelper.Point3DtoPoint2D(viewport.Viewport,cursor.Value);
            var hits = Viewport3DHelper.FindHits(viewport.Viewport, pointCursor2D);
            var hit = hits.FirstOrDefault();
            if (hit == null)
            {
                SelectedObject = null;
                return;
            }
            foreach (var cad in cadCollection)
            {
                if (cad.VisualModel.Equals(hit.Model))
                {
                    SelectedObject = cad;
                }
            }
        }

        private void UpdateVisual(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(()=> VisualModel);
        }

        private void UpdateProperties()
        {
            Properties.Clear();
            if (_selectedObject == null)
            {
                return;
            }
            var fields = _selectedObject.GetType().GetProperties();
            foreach (var propertyInfo in fields)
            {
                if (propertyInfo.CustomAttributes.Any(p => p.AttributeType == typeof(CanUserSetPropertyAttribute)))
                {
                    Properties.Add(new PropertyViewModel(_selectedObject,propertyInfo));
                }
            }
        }

    }
}