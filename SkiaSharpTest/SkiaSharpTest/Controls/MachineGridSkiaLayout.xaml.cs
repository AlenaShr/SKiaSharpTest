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
    public partial class MachineGridSkiaLayout : ContentView
    {
        #region .CTOR
        public MachineGridSkiaLayout()
        {
            InitializeComponent();
            
        }
        #endregion

        #region DataSource
        public Tuple<float, float, Func<float, float, Color>> DataSource
        {
            get { return (Tuple<float, float, Func<float, float, Color>>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly BindableProperty DataSourceProperty = BindableProperty.Create(
            nameof(DataSource), typeof(Tuple<float, float, Func<float, float, Color>>), typeof(MachineGridSkiaLayout), default(Tuple<float, float, Func<float, float, Color>>), propertyChanged: OnDataSourceChanged);

        private static void OnDataSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        #region WidthContent

        public GridLength WidthContent
        {
            get { return (GridLength)GetValue(WidthContentProperty); }
            set { SetValue(WidthContentProperty, value); }
        }

        public static readonly BindableProperty WidthContentProperty = BindableProperty.Create(
            nameof(WidthContent), typeof(GridLength), typeof(MachineGridSkiaLayout), default(GridLength));

        #endregion

        #region HeightContent
        public GridLength HeightContent
        {
            get { return (GridLength)GetValue(HeightContentProperty); }
            set { SetValue(HeightContentProperty, value); }
        }

        public static readonly BindableProperty HeightContentProperty = BindableProperty.Create(
            nameof(HeightContent), typeof(GridLength), typeof(MachineGridSkiaLayout), default(GridLength));

        #endregion
        #endregion

    }
}