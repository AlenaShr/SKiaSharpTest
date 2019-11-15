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
        public float Margin { get; set; } = 10;
        public string Label { get; set; }
        public float LabelTextSize { get; set; } = 16.0f;
        public SKColor ChartColor { get; set; } = SKColors.Green;
        public SKColor ChartAreaColor { get; set; } = SKColors.LightGray;
        public SKColor TextColor { get; set; } = SKColors.Gray;
        public SKColor BackgroundColor { get; set; } = SKColors.White;
        public DataEntryCollection DataEntries { get; set; }
        public float HeightItem { get; set; } = Device.Idiom == TargetIdiom.Phone ? 20 : 
                                                       (Device.RuntimePlatform == Device.Android ? 40 : 45);
        public float WidthItem { get; set; } = Device.Idiom == TargetIdiom.Phone? 10 : 20;

        #endregion

        #region Methods

        public void Draw(SKCanvas canvas, int width, int height)
        {
            canvas.Clear();
            this.DrawLayout(canvas, width, height);
        }
        private void DrawLayout(SKCanvas canvas, int width, int height)
        {
            var headerHeight = CalculateHeaderHeight();
            var footerHeight = CalculateFooterHeight();
            var itemSize = CalculateItemSize(width, height, footerHeight, headerHeight);
            var origin = CalculateYOrigin(itemSize.Height, headerHeight);
            var points = this.CalculatePoints(itemSize, origin, headerHeight);

            this.DrawGridAreas(canvas, points, itemSize, headerHeight);
            this.DrawGrids(canvas, points, itemSize, origin, headerHeight);
            this.DrawFooter(canvas, points, height);
        }

        private float CalculateHeaderHeight()
        {
            var result = this.Margin;
            return result;
        }

        private float CalculateFooterHeight()
        {
            var result = this.Margin;
            if (!string.IsNullOrEmpty(Label))
            {
                result += this.LabelTextSize + this.Margin;
            }

            return result;
        }
                
        private SKSize CalculateItemSize(int width, int height, float footerHeight, float headerHeight)
        {
            var total = this.DataEntries.Count();
            var w = WidthItem; //(width - ((total + 1) * this.Margin)) / total;
            var h = height - this.Margin - footerHeight - headerHeight;
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

                var x = this.Margin + (itemSize.Width / 2) + (i * (itemSize.Width + this.Margin));
                var y = headerHeight + (((this.MaxValue - entry.Value) / this.ValueRange) * itemSize.Height);
                var point = new SKPoint(x, y);
                result.Add(point);
            }

            return result.ToArray();
        }

        private void DrawGridAreas(SKCanvas canvas, SKPoint[] points, SKSize itemSize, float headerHeight)
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
                        var rect = SKRect.Create(point.X - (itemSize.Width / 2), y, itemSize.Width, height);
                        canvas.DrawRoundRect(new SKRoundRect(rect,4,4), paint);
                    }
                }
            }
        }

        private void DrawGrids(SKCanvas canvas, SKPoint[] points, SKSize itemSize, float origin, float headerHeight)
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
                                if (y + height > this.Margin + itemSize.Height)
                                {
                                    y = headerHeight + itemSize.Height - height;
                                }
                            }
                        }
                        else
                        {
                            height = Math.Abs(origin - point.Y);
                        }
                        
                        var rect = SKRect.Create(x, y, itemSize.Width, height);
                        canvas.DrawRect(rect, paint);
                    }
                }
            }
        }

        private void DrawFooter(SKCanvas canvas, SKPoint[] points, int height)
        {
            this.DrawLabel(canvas, points, height);
        }

        private void DrawLabel(SKCanvas canvas, SKPoint[] points, int height)
        {
            var point = points[0];

            if (!string.IsNullOrEmpty(Label))
            {
                using (var paint = new SKPaint())
                {
                    paint.TextSize = this.LabelTextSize;
                    paint.IsAntialias = true;
                    paint.Color = TextColor;
                    paint.IsStroke = false;

                    var bounds = new SKRect();
                    var text = Label;
                    paint.MeasureText(text, ref bounds);

                    
                    canvas.DrawText(text, this.Margin, height - (this.Margin + (this.LabelTextSize / 2)), paint);
                }
            }
        }

        #endregion
    }

}
