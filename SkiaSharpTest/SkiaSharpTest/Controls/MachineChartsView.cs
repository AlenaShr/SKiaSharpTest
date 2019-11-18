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
    public class MachineChartsView : SKCanvasView
    {
        #region .CTOR
        public MachineChartsView()
        {
            this.PaintSurface += OnPaintCanvas;
        }
        #endregion

        #region Bindable Properties

        public IEnumerable<object> DataSource
        {
            get { return (IEnumerable<object>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly BindableProperty DataSourceProperty = BindableProperty.Create(
            nameof(DataSource), typeof(IEnumerable<object>), typeof(MachineChartsView), default(IEnumerable<object>), propertyChanged: OnDataSourceChanged);

        private static void OnDataSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
           ((MachineChartsView)bindable).InvalidateSurface();
        }

        public Container Container
        {
            get { return (Container)GetValue(ContainerProperty); }
            set { SetValue(ContainerProperty, value); }
        }

        public static readonly BindableProperty ContainerProperty = BindableProperty.Create(
            nameof(Container), typeof(Container), typeof(MachineChartsView), new Container());

        public List<Series> Sections
        {
            get { return (List<Series>)GetValue(SectionsProperty); }
            set { SetValue(SectionsProperty, value); }
        }

        public static readonly BindableProperty SectionsProperty = BindableProperty.Create(
            nameof(Sections), typeof(List<Series>), typeof(MachineChartsView), null, propertyChanged: OnSeriesChanged);

        private static void OnSeriesChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        #endregion

        #region Event Handling
        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            e.Surface.Canvas.Clear();
            DrawChart(e.Surface.Canvas, e.Info.Width, e.Info.Height);

        }

        #endregion

        #region Methods
        private void DrawChart(SKCanvas sKCanvas, int width, int height)
        {
            if (DataSource != default(IEnumerable<object>))
            {
                if (Sections == null)
                {
                    Sections = new List<Series>();
                }
                if (Sections.Count > 0)
                {
                    Sections.Clear();
                }
                Type type = DataSource.ElementAt(0).GetType();
                IEnumerable<PropertyInfo> properties = type.GetTypeInfo().DeclaredProperties;
                foreach (var val in DataSource)
                {
                    IEnumerable<object> xValue;
                    string yValue;
                    xValue = properties.ElementAt(0).GetValue(val, null) as IEnumerable<object>;                   

                    DataEntryCollection entries = new DataEntryCollection();
                    foreach (var nested in xValue)
                    {
                        Type type2 = nested.GetType();
                        IEnumerable<PropertyInfo> properties2 = type2.GetTypeInfo().DeclaredProperties;
                        float xValue2, yValue2;
                        float.TryParse(properties2.ElementAt(0).GetValue(nested, null).ToString(), out xValue2);
                        float.TryParse(properties2.ElementAt(1).GetValue(nested, null).ToString(), out yValue2);
                        entries.Add(new DataEntry(xValue2, yValue2));
                    }
                    yValue = properties.ElementAt(1).GetValue(val, null).ToString();

                    Sections.Add(new Series(entries, yValue));
                }
            }
            if (Sections != null)
            {
                float xOffset = 0;
                float yOffset = 0;
                if (Container != null)
                {

                    Container.Draw(sKCanvas, width, height);
                    xOffset = Container.MarginLeftRightOuter;
                    yOffset = Container.HeaderHeight;

                    int iHeight = Container.InnerHeight / Sections.Count;
                    int iWidth = Container.InnerWidth;


                    foreach (var section in Sections)
                    {
                        section.Draw(sKCanvas, iWidth, iHeight, xOffset, yOffset);
                        yOffset += iHeight;
                    }
                }
            }
        }

        #endregion
    }
}
