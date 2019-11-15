using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace SkiaSharpTest.Controls
{
    public class ChartView : SKCanvasView
    {
        public ChartView()
        {
            Series = new Series();
            this.BackgroundColor = Color.Transparent;
            this.PaintSurface += OnPaintCanvas;
        }

        public IEnumerable<object> DataSource
        {
            get { return (IEnumerable<object>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly BindableProperty DataSourceProperty = BindableProperty.Create(
            nameof(DataSource), typeof(IEnumerable<object>), typeof(Char), default(IEnumerable<object>), propertyChanged: OnDataSourceChanged);

        private static void OnDataSourceChanged(BindableObject bindable,object oldValue, object newValue)
        {
            ((ChartView)bindable).InvalidateSurface();
        }

        public Series Series
        {
            get { return (Series)GetValue(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        public static readonly BindableProperty SeriesProperty = BindableProperty.Create(
            nameof(Series), typeof(Series), typeof(ChartView), null);

        public string Label 
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label), typeof(string), typeof(ChartView), string.Empty, propertyChanged: OnLabelChanged);

        private static void OnLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ChartView)bindable).InvalidateSurface();
        }


        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            DrawChart(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }

        private void DrawChart(SKCanvas canvas, int width, int height)
        {
            if (DataSource != default(IEnumerable<object>))
            {
                if (Series == null)
                {
                    Series = new Series();
                }
                if (Series.DataEntries == null)
                {
                    Series.DataEntries = new DataEntryCollection();
                }
                if (Series.DataEntries.Count > 0)
                {
                    Series.DataEntries.Clear();
                }
                
                Type type = DataSource.ElementAt(0).GetType();
                IEnumerable<PropertyInfo> properties = type.GetTypeInfo().DeclaredProperties;
                foreach (var val in DataSource)
                {
                    float xValue, yValue; 
                    float.TryParse(properties.ElementAt(0).GetValue(val, null).ToString(), out xValue);
                    float.TryParse(properties.ElementAt(1).GetValue(val, null).ToString(), out yValue);
                    Series.DataEntries.Add(new DataEntry(xValue, yValue));
                }
                Series.FooterLabel = Label;
            }
            if (this.Series != null)
            {
                //this.Series.Draw(canvas, width, height);
            }
        }
    }
}
