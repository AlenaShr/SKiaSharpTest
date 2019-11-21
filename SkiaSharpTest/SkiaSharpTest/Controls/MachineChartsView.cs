using CHBackOffice.ApiServices.ChsProxy;
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
        #region Private Fields
        KioskState state = KioskState.Offline;
        KioskStatus status = KioskStatus.Normal;
        SKColor color = SKColors.LightGray;
        string id;
        #endregion

        #region .CTOR
        public MachineChartsView()
        {
            this.PaintSurface += OnPaintCanvas;
        }
        #endregion

        #region Bindable Properties

        public float Scale => Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet ? (float)2 : (float)3;

        public Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> DataSource
        {
            get { return (Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly BindableProperty DataSourceProperty = BindableProperty.Create(
            nameof(DataSource), typeof(Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>), typeof(MachineChartsView), default(Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>), propertyChanged: OnDataSourceChanged);

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

        public SeriesOrientation SeriesOrientation
        {
            get { return (SeriesOrientation)GetValue(SeriesOrientationProperty); }
            set { SetValue(SeriesOrientationProperty, value); }
        }

        public static readonly BindableProperty SeriesOrientationProperty = BindableProperty.Create(
            nameof(SeriesOrientation), typeof(SeriesOrientation), typeof(MachineChartsView), SeriesOrientation.Vertical);


        #endregion

        #region Event Handling
        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            e.Surface.Canvas.Clear();
            var scale = CanvasSize.Width / Width;
            var kscale = scale / Scale;
            DrawChart(e.Surface.Canvas, e.Info.Width, e.Info.Height, kscale);
        }

        #endregion

        #region Methods
        private void DrawChart(SKCanvas sKCanvas, int width, int height, double kscale)
        {
            if (DataSource != default(Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>))
            {
                if (Sections == null)
                {
                    Sections = new List<Series>();
                }
                if (Sections.Count > 0)
                {
                    Sections.Clear();
                }
                Type dataSourceType = DataSource.GetType();
                IEnumerable<PropertyInfo> dataSourceProperties = dataSourceType.GetTypeInfo().DeclaredProperties;
                IEnumerable<object> collections = dataSourceProperties.ElementAt(0).GetValue(DataSource, null) as IEnumerable<object>;

                Enum.TryParse(dataSourceProperties.ElementAt(1).GetValue(DataSource, null).ToString(), out state);
                Enum.TryParse(dataSourceProperties.ElementAt(2).GetValue(DataSource, null).ToString(), out status);
                id = dataSourceProperties.ElementAt(3).GetValue(DataSource, null).ToString();

                Type collType = collections.ElementAt(0).GetType();
                IEnumerable<PropertyInfo> properties = collType.GetTypeInfo().DeclaredProperties;
                int sectionCount = 0;
                foreach (var val in collections)
                {
                    IEnumerable<object> entities;
                    string label;
                    entities = properties.ElementAt(0).GetValue(val, null) as IEnumerable<object>;

                    DataEntryCollection entries = new DataEntryCollection();
                    foreach (var entity in entities)
                    {
                        Type entityType = entity.GetType();
                        IEnumerable<PropertyInfo> properties2 = entityType.GetTypeInfo().DeclaredProperties;
                        float count, capacity;
                        float.TryParse(properties2.ElementAt(0).GetValue(entity, null).ToString(), out count);
                        float.TryParse(properties2.ElementAt(1).GetValue(entity, null).ToString(), out capacity);
                        Func<float, float, Color> func = properties2.ElementAt(2).GetValue(entity, null) as Func<float, float, Color>;
                        if (func != null)
                        {
                            color = func.Invoke(count, capacity).ToSKColor();
                        }
                        entries.Add(new DataEntry(count, capacity, color));
                    }
                    label = properties.ElementAt(1).GetValue(val, null).ToString();
                    if (entities.Count() == 0)
                        label = string.Empty;
                    Sections.Add(new Series(entries, label));
                    sectionCount++;
                }
            }
            if (Sections != null)
            {
                float xOffset = 0;
                float yOffset = 0;
                if (Container != null)
                {
                    SKColor color = MatchStateStatusToColor(state, status);
                    Container.Draw(sKCanvas, width, height, kscale, color, id);
                    xOffset = Container.MarginLeftRightOuter;
                    yOffset = Container.HeaderHeight;

                    if (SeriesOrientation == SeriesOrientation.Vertical)
                    {
                        int iHeight = Container.InnerHeight / Sections.Count;
                        int iWidth = Container.InnerWidth;

                        foreach (var section in Sections)
                        {
                            section.Draw(sKCanvas, iWidth, iHeight, kscale, xOffset, yOffset);
                            yOffset += iHeight;
                        }
                    }
                    else
                    {
                        int iHeight = Container.InnerHeight;
                        int iWidth = Container.InnerWidth / Sections.Count;

                        foreach (var section in Sections)
                        {
                            section.Draw(sKCanvas, iWidth, iHeight, kscale, xOffset, yOffset);
                            xOffset += iWidth;
                        }
                    }
                }
            }
        }

        #region Color Graphs

        private SKColor MatchStateStatusToColor(KioskState state, KioskStatus status)
        {
            if (state == CHBackOffice.ApiServices.ChsProxy.KioskState.InService ||
                   state == CHBackOffice.ApiServices.ChsProxy.KioskState.ONLINE)
            {
                if (status == CHBackOffice.ApiServices.ChsProxy.KioskStatus.Normal)
                {
                    return Color.FromHex("#63EEB0").ToSKColor();
                }
                else if (status == CHBackOffice.ApiServices.ChsProxy.KioskStatus.Warning)
                {
                    return Color.FromHex("#FEDE76").ToSKColor();
                }
                else
                {
                    return Color.FromHex("#FCA35A").ToSKColor();
                }
            }
            else if (state == CHBackOffice.ApiServices.ChsProxy.KioskState.Offline)
            {
                return Color.FromHex("#CDCDCD").ToSKColor();
            }
            else if (state == CHBackOffice.ApiServices.ChsProxy.KioskState.OutOfServiceSOP)
            {
                return Color.FromHex("#62C8F5").ToSKColor();
            }
            else
            {
                return Color.FromHex("#FD6F82").ToSKColor();
            }
        }
        #endregion

        #endregion
    }

    public enum SeriesOrientation
    {
        Vertical,
        Horizontal
    }
}
