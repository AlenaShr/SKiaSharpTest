using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SkiaSharpTest.Controls
{
    public class MachineChartsView : SKCanvasView
    {
        public MachineChartsView()
        {
            this.BackgroundColor = Color.Yellow;
            Container = new Container();
            Sections = new List<Series>
            {
                    new Series
                    {
                        DataEntries = new DataEntryCollection
                        {
                           new DataEntry(0,2250),
                           new DataEntry(1940,2250),
                           new DataEntry(1980,2250),
                           new DataEntry(1905,2250),
                           new DataEntry(1999,2250),
                        },
                        FooterLabel = "CASH DIS."
                    },
                    new Series
                    {
                        DataEntries = new DataEntryCollection
                        {
                           new DataEntry(1,2250),
                           new DataEntry(1800,2250),
                           new DataEntry(1979,2250)
                        },
                        FooterLabel = "COINS DIS."
                    },
                    new Series
                    {
                        DataEntries = new DataEntryCollection
                        {
                           new DataEntry(2,990)
                        },
                        FooterLabel = "BILL VAL."
                    }
            };
            this.PaintSurface += OnPaintCanvas;
            InvalidateSurface();
        }

        public Container Container
        {
            get { return (Container)GetValue(ContainerProperty); }
            set { SetValue(ContainerProperty, value); }
        }

        public static readonly BindableProperty ContainerProperty = BindableProperty.Create(
            nameof(Container), typeof(Container), typeof(MachineChartsView), null);

        public List<Series> Sections
        {
            get { return (List<Series>)GetValue(SectionsProperty); }
            set { SetValue(SectionsProperty, value); }
        }

        public static readonly BindableProperty SectionsProperty = BindableProperty.Create(
            nameof(Sections), typeof(List<Series>), typeof(MachineChartsView), null, propertyChanged: OnSeriesChanged);

        private static void OnSeriesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //((MachineChartsView)bindable).InvalidateSurface();
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            if (Sections != null)
            {
                e.Surface.Canvas.Clear();

                float xOffset = 0;
                float yOffset = 0;
                if (Container != null)
                {
                    
                    Container.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
                    xOffset = Container.MarginLeftRightOuter;
                    yOffset = Container.HeaderHeight;

                    int height = Container.InnerHeight / Sections.Count;
                    int width = Container.InnerWidth;


                    foreach (var section in Sections)
                    {
                        section.Draw(e.Surface.Canvas, width, height, xOffset, yOffset);
                        yOffset += height;
                    }
                }
            }
        }
    }
}
