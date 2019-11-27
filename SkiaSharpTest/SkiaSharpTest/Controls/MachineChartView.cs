﻿using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace SkiaSharpTest.Controls
{
    public class MachineChartView : SKCanvasView
    {
        #region Private Fields
        SKColor color = SKColors.LightGray;
        #endregion

        #region .CTOR
        public MachineChartView()
        {
            this.PaintSurface += OnPaintCanvas;
        }
        #endregion

        #region Bindable Properties
        public new float Scale => Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Tablet ? (float)2 : (float)3;

        public Tuple<float, float, Func<float, float, Color>> DataSource
        {
            get { return (Tuple<float, float, Func<float, float, Color>>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly BindableProperty DataSourceProperty = BindableProperty.Create(
            nameof(DataSource), typeof(Tuple<float, float, Func<float, float, Color>>), typeof(MachineChartView), default(Tuple<float, float, Func<float, float, Color>>), propertyChanged: OnDataSourceChanged);

        private static void OnDataSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MachineChartView)bindable).InvalidateSurface();
        }
        #endregion

        #region Chart
        public Partition Chart { get; set; }
        #endregion


        #region Event Handling
        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            e.Surface.Canvas.Clear();
            var scale = CanvasSize.Width / Width;
            var kscale = scale / Scale;

            DrawChart(e.Surface.Canvas,e.Info.Width,e.Info.Height,kscale);
        }

        #endregion

        #region Methods
        private void DrawChart(SKCanvas sKCanvas, int width, int height, double kScale)
        {
            if (DataSource != default(Tuple<float, float, Func<float, float, Color>>))
            {
                Type dataSourceType = DataSource.GetType();
                IEnumerable<PropertyInfo> dataSourceProperties = dataSourceType.GetTypeInfo().DeclaredProperties;
                float count, capacity;
                float.TryParse(dataSourceProperties.ElementAt(0).GetValue(DataSource, null).ToString(), out count);
                float.TryParse(dataSourceProperties.ElementAt(1).GetValue(DataSource, null).ToString(), out capacity);
                Func<float, float, Color> func = dataSourceProperties.ElementAt(2).GetValue(DataSource, null) as Func<float, float, Color>;
                if (func != null)
                {
                    color = func.Invoke(count, capacity).ToSKColor();
                }
                var dataEntry = new DataEntry(count, capacity, color);
                if (Chart == null)
                    Chart = new Partition(dataEntry);
                else
                {
                    Chart.DataEntry = dataEntry;
                }
            }
            if (Chart != null)
            {
                Chart.Draw(sKCanvas, width, height, kScale);
            }
        }
        #endregion
    }
}
