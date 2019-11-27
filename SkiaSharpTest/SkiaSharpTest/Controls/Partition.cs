using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharpTest.Controls
{
    public class Partition
    {
        #region Properties

        #region MinValue
        private float? _minValue;
        public float MinValue
        {
            get
            {

                if (this._minValue == null)
                {
                    return Math.Min(0, this.DataEntry.Capacity);
                }

                return Math.Min(this._minValue.Value, this.DataEntry.Capacity);
            }

            set => this._minValue = value;
        }
        #endregion

        #region MaxValue
        private float? _maxValue;
        public float MaxValue
        {
            get
            {

                if (this._maxValue == null)
                {
                    return Math.Max(0, this.DataEntry.Capacity);
                }

                return Math.Max(this._maxValue.Value, this.DataEntry.Capacity);
            }

            set => this._maxValue = value;
        }
        #endregion

        private float ValueRange => this.MaxValue - this.MinValue;
        public SKColor ChartAreaColor { get; set; } = SKColors.LightGray;
        public SKColor ChartAreaBorderColor { get; set; } = SKColors.Green;
        public DataEntry DataEntry { get; set; }
        public double KScale { get; set; } = 1;
        #endregion
        #region .CTOR
        public Partition()
        { }

        public Partition(DataEntry dataEntry)
        {
            DataEntry = dataEntry;
        }
        #endregion

        #region Methods
        public void Draw(SKCanvas canvas, int width, int height, double kScale)
        {
            KScale = kScale;
            this.DrawLayout(canvas, width, height);
        }

        private void DrawLayout(SKCanvas canvas, int width, int height)
        {
            var itemSize = CalculateItemSize(width, height);
            var origin = CalculateXOrigin(itemSize.Width);
            var point = CaclulatePoint(itemSize, origin);

            this.DrawGridAreas(canvas, point, itemSize,origin);
            this.DrawGrid(canvas, point, itemSize, origin);
        }

        private SKSize CalculateItemSize(int width, int height)
        {
            return new SKSize(width, height);
        }

        private float CalculateXOrigin(float itemWidth)
        {
            return (this.MaxValue / this.ValueRange) * itemWidth;
        }

        private SKPoint CaclulatePoint(SKSize itemSize, float origin)
        {
            var percent = (this.MaxValue - this.DataEntry.Value) / this.ValueRange;
            if (this.DataEntry.Value == 0)
                percent = 1;
            var x = percent * itemSize.Width;
            var y = 0;
            return new SKPoint(x, y);
        }

        private void DrawGridAreas(SKCanvas canvas, SKPoint point, SKSize itemSize, float origin)
        {
            using (var paint = new SKPaint 
            {
                Style = SKPaintStyle.Fill,
                Color = ChartAreaBorderColor
            }) 
            {
                var width = origin;
                var x = 0;
                var rect = SKRect.Create(x, point.Y, width, itemSize.Height);
                canvas.DrawRect(rect, paint);
            }

            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = ChartAreaColor
            })
            {
                var x = 0;
                var width = origin;
                var rect = SKRect.Create(x + 2, point.Y + 2, width - 4, itemSize.Height - 4);
                canvas.DrawRect(rect, paint);
            }
        }

        private void DrawGrid(SKCanvas canvas, SKPoint point, SKSize itemSize, float origin)
        {
            
            using (var paint = new SKPaint 
            {
                Style = SKPaintStyle.Fill,
                Color = DataEntry.Color
            }) 
            {
                var x = 0;
                var y = point.Y;
                float width = width = Math.Abs(origin - point.X);

                var rect = SKRect.Create(x, y, width, itemSize.Height);
                canvas.DrawRect(rect, paint); 
            }
        }
        #endregion
    }
}
