using CHBackOffice.ApiServices.ChsProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkiaSharpTest.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MachineInfoSkiaLayout : ContentView
    {
        #region .CTOR
        public MachineInfoSkiaLayout()
        {
            InitializeComponent();
            SkiaSharpTest.Controls.Container container = new Controls.Container()
            {
                MarginHeader = 20.0f,
                LabelTextSize = 24.0f,
                MarginInner = Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet ?
                20.0f : 20.0f,
                PaddingOuter = 20.0f
            };
            SkiaSharpTest.Controls.Series series = new Series()
            {
                MarginRightInner = 10.0f,
                MarginLeftInner = 50.0f,
                MarginTopInner = 30.0f,
                MarginBottomInner = 10.0f,
                MarginFooter = 0.0f,
                LabelTextSize = 35.0f,
                WidthItem = Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet ?
                40.0f : 38.0f
            };

            machineCharts.Container = container;
            machineCharts.Series = series;

        }
        #endregion

        #region Bindable Properties

        #region DataSource
        public Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> DataSource
        {
            get { return (Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly BindableProperty DataSourceProperty = BindableProperty.Create(
            nameof(DataSource), typeof(Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>), typeof(MachineInfoSkiaLayout), default(Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>), propertyChanged: OnDataSourceChanged);

        private static void OnDataSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        #endregion

        #region WidthContent

        public GridLength WidthContent
        {
            get { return (GridLength)GetValue(WidthContentProperty); }
            set { SetValue(WidthContentProperty, value); }
        }

        public static readonly BindableProperty WidthContentProperty = BindableProperty.Create(
            nameof(WidthContent), typeof(GridLength), typeof(MachineInfoSkiaLayout), default(GridLength));

        #endregion

        #region HeightContent
        public GridLength HeightContent
        {
            get { return (GridLength)GetValue(HeightContentProperty); }
            set { SetValue(HeightContentProperty, value); }
        }

        public static readonly BindableProperty HeightContentProperty = BindableProperty.Create(
            nameof(HeightContent), typeof(GridLength), typeof(MachineInfoSkiaLayout), default(GridLength));

        #endregion
        #endregion
    }
}