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
    public partial class MachineStateSkiaLayout : ContentView
    {
        #region .CTOR
        public MachineStateSkiaLayout()
        {
            InitializeComponent();
            SkiaSharpTest.Controls.Container container = new Controls.Container() 
            {
                MarginHeader = 25.0f,
                LabelTextSize = 24.0f,
                MarginInner = Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet ?
                10.0f : 15.0f,
                PaddingOuter = 20.0f
            };
            SkiaSharpTest.Controls.Series series = new Series()
            {
                MarginRightInner = 10.0f,
                MarginLeftInner = 10.0f,
                MarginTopInner = 10.0f,
                MarginBottomInner = 10.0f,
                MarginFooter = 5.0f,
                LabelTextSize = 20.0f,
                WidthItem = Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet ?
                20.0f : 18.0f
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
            nameof(DataSource), typeof(Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>), typeof(MachineStateSkiaLayout), default(Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>), propertyChanged: OnDataSourceChanged);

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
            nameof(WidthContent), typeof(GridLength), typeof(MachineStateSkiaLayout), default(GridLength));

        #endregion

        #region HeightContent
        public GridLength HeightContent
        {
            get { return (GridLength)GetValue(HeightContentProperty); }
            set { SetValue(HeightContentProperty, value); }
        }

        public static readonly BindableProperty HeightContentProperty = BindableProperty.Create(
            nameof(HeightContent), typeof(GridLength), typeof(MachineStateSkiaLayout), default(GridLength));

        #endregion
        #endregion

    }
}