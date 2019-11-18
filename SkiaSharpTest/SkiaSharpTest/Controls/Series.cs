using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SkiaSharpTest.Controls
{
    public class Series 
    {
        #region Properties

        private float? _minValue;
        public float MinValue
        {
            get
            {
                if (!this.DataEntries.Any())
                {
                    return 0;
                }

                if (this._minValue == null)
                {
                    return Math.Min(0, this.DataEntries.Min(x => x.Capacity));
                }

                return Math.Min(this._minValue.Value, this.DataEntries.Min(x => x.Capacity));
            }

            set => this._minValue = value;
        }

        private float? _maxValue;
        public float MaxValue
        {
            get
            {
                if (!this.DataEntries.Any())
                {
                    return 0;
                }

                if (this._maxValue == null)
                {
                    return Math.Max(0, this.DataEntries.Max(x => x.Capacity));
                }

                return Math.Max(this._maxValue.Value, this.DataEntries.Max(x => x.Capacity));
            }

            set => this._maxValue = value;
        }
        private float ValueRange => this.MaxValue - this.MinValue;
        public float MarginInner { get; set; } = 10;
        public float MarginLabel { get; set; } = 5;
        public string FooterLabel { get; set; }
        public float FooterLabelTextSize { get; set; } = 20.0f;
        public SKColor ChartColor { get; set; } = SKColors.Green;
        public SKColor ChartAreaColor { get; set; } = SKColors.LightGray;
        public SKColor TextColor { get; set; } = SKColors.Black;
        public DataEntryCollection DataEntries { get; set; }
        public float WidthItem { get; set; } = Device.Idiom == TargetIdiom.Phone? 10 : 20;

        #endregion

        #region .CTOR
        public Series()
        {
        }

        public Series(DataEntryCollection dataEntries, string label)
        {
            DataEntries = dataEntries;
            FooterLabel = label;
        }
        #endregion

        #region Methods

        public void Draw(SKCanvas canvas, int width, int height, float xOffset = 0, float yOffset = 0)
        {
            this.DrawLayout(canvas, width, height, xOffset, yOffset);
        }

        private void DrawLayout(SKCanvas canvas, int width, int height, float xOffset, float yOffset)
        {

            var headerHeight = CalculateHeaderHeight();
            var footerHeight = CalculateFooterHeight();
            var itemSize = CalculateItemSize(width, height, footerHeight, headerHeight);
            var origin = CalculateYOrigin(itemSize.Height, headerHeight);
            var points = this.CalculatePoints(itemSize, origin, headerHeight);
            
            this.DrawGridAreas(canvas, points, itemSize, headerHeight, xOffset, yOffset);
            this.DrawGrids(canvas, points, itemSize, origin, headerHeight, xOffset, yOffset);
            this.DrawFooter(canvas, height, xOffset, yOffset);
        }

        private float CalculateHeaderHeight()
        {
            var result = this.MarginInner;
            return result;
        }

        private float CalculateFooterHeight()
        {
            var result = this.MarginInner;
            if (!string.IsNullOrEmpty(FooterLabel))
            {
                result += this.FooterLabelTextSize + this.MarginLabel;
            }

            return result;
        }
                
        private SKSize CalculateItemSize(int width, int height, float footerHeight, float headerHeight)
        {
            var total = this.DataEntries.Count();
            var w = WidthItem; //(width - ((total + 1) * this.Margin)) / total;
            var h = height - this.MarginInner - footerHeight - headerHeight;
            return new SKSize(w, h);
        }

        private float CalculateYOrigin(float itemHeight, float headerHeight)
        {
            return headerHeight + ((this.MaxValue / this.ValueRange) * itemHeight);
        }

        private SKPoint[] CalculatePoints(SKSize itemSize, float origin, float headerHeight)
        {
            var result = new List<SKPoint>();

            for (int i = 0; i < this.DataEntries.Count(); i++)
            {
                var entry = this.DataEntries.ElementAt(i);

                var x = this.MarginInner + (itemSize.Width / 2) + (i * (itemSize.Width + this.MarginInner));
                var y = headerHeight + (((this.MaxValue - entry.Value) / this.ValueRange) * itemSize.Height);
                var point = new SKPoint(x, y);
                result.Add(point);
            }

            return result.ToArray();
        }

        private void DrawGridAreas(SKCanvas canvas, SKPoint[] points, SKSize itemSize, float headerHeight, float xOffset, float yOffset)
        {
            if (points.Length > 0 )
            {
                for (int i = 0; i < points.Length; i++)
                {
                    var entry = this.DataEntries.ElementAt(i);
                    var point = points[i];

                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = ChartAreaColor,
                    })
                    {
                        var max = headerHeight; //entry.Value > 0 ? headerHeight : headerHeight + itemSize.Height;
                        var height = Math.Abs(max - point.Y);
                        var y = Math.Min(max, point.Y);
                        var rect = SKRect.Create(point.X - (itemSize.Width / 2) + xOffset, y + yOffset, itemSize.Width, height);
                        canvas.DrawRoundRect(new SKRoundRect(rect, 4, 4), paint);
                    }
                }
            }
        }

        private void DrawGrids(SKCanvas canvas, SKPoint[] points, SKSize itemSize, float origin, float headerHeight, float xOffset, float yOffset)
        {
            const float MinBarHeight = 1;
            if (points.Length > 0)
            {
                for (int i = 0; i < this.DataEntries.Count(); i++)
                {
                    var entry = this.DataEntries.ElementAt(i);
                    var point = points[i];

                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = ChartColor,
                    })
                    {
                        var x = point.X - (itemSize.Width / 2);
                        var y = Math.Min(origin, point.Y);
                        float height;
                        if (DataEntries[i].Value > 0)
                        {
                            height = Math.Max(MinBarHeight, Math.Abs(origin - point.Y));
                            if (height < MinBarHeight)
                            {
                                height = MinBarHeight;
                                if (y + height > this.MarginInner + itemSize.Height)
                                {
                                    y = headerHeight + itemSize.Height - height;
                                }
                            }
                        }
                        else
                        {
                            height = Math.Abs(origin - point.Y);
                        }
                        
                        var rect = SKRect.Create(x + xOffset, y + yOffset, itemSize.Width, height);
                        canvas.DrawRect(rect, paint);
                    }
                }
            }
        }

        private void DrawFooter(SKCanvas canvas, int height, float xOffset, float yOffset)
        {
            this.DrawLabel(canvas, height, xOffset, yOffset);
        }

        private void DrawLabel(SKCanvas canvas, int height, float xOffset, float yOffset)
        {
            if (!string.IsNullOrEmpty(FooterLabel))
            {
                using (var paint = new SKPaint())
                {
                    paint.TextSize = this.FooterLabelTextSize;
                    paint.IsAntialias = true;
                    paint.Color = TextColor;
                    paint.IsStroke = false;

                    var bounds = new SKRect();
                    var text = FooterLabel;
                    paint.MeasureText(text, ref bounds);

                    
                    canvas.DrawText(text, this.MarginInner + xOffset, height - (this.MarginInner + (this.FooterLabelTextSize / 2)) + this.MarginLabel + yOffset, paint);
                }
            }
        }

        #endregion
    }

}
