using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SkiaSharpTest.Controls
{
    public class Container
    {
        #region Properties
        public float MarginTopBottomOuter { get; set; } = 25;
        public float MarginLeftRightOuter { get; set; } = 10;
        public SKColor BackgroundColorOuter { get; set; } = SKColors.LightGray;
        public SKColor BackgroundColorInner { get; set; } = SKColors.White;
        public SKColor TextColor { get; set; } = SKColors.Black;
        public string HeaderLabel { get; set; } = "APPLE";
        public float HeaderLabelTextSize { get; set; } = 20.0f;
        public int InnerWidth { get; set; } = 180;
        public int InnerHeight { get; set; } = 408;
        public int OuterWidth { get; set; }
        public float HeaderHeight { get; set; }
        #endregion

        #region Methods
        public void Draw(SKCanvas canvas, int width, int height)
        {
            this.DrawLayout(canvas, width, height);
        }

        private void DrawLayout(SKCanvas canvas, int width, int height)
        {
            OuterWidth = width;
            HeaderHeight = CalculateHeaderHeight();
            this.DrawOuterGrid(canvas, width, height);
            this.DrawLabel(canvas);
            this.DrawInnerGrid(canvas);
        }

        private float CalculateHeaderHeight()
        {
            var result = this.MarginTopBottomOuter;
            if (!string.IsNullOrEmpty(HeaderLabel))
            {
                result += this.HeaderLabelTextSize + this.MarginTopBottomOuter;
            }

            return result;
        }

        private void DrawOuterGrid(SKCanvas canvas, float width, float height)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = BackgroundColorOuter
            })
            {
                var rect = SKRect.Create(0, 0, width, height);
                canvas.DrawRoundRect(new SKRoundRect(rect, 4, 4), paint);
            }
        }

        private void DrawInnerGrid(SKCanvas canvas)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = BackgroundColorInner
            })
            {
                var rect = SKRect.Create(MarginLeftRightOuter, HeaderHeight, InnerWidth, InnerHeight);
                canvas.DrawRect(rect, paint);
            }
        }

        private void DrawLabel(SKCanvas canvas)
        {
            if (!string.IsNullOrEmpty(HeaderLabel))
            {
                using (var paint = new SKPaint())
                {
                    paint.TextSize = this.HeaderLabelTextSize;
                    paint.IsAntialias = true;
                    paint.Color = TextColor;
                    paint.IsStroke = false;

                    var bounds = new SKRect();
                    var text = HeaderLabel;
                    paint.MeasureText(text, ref bounds);

                    canvas.DrawText(text, (this.OuterWidth - bounds.Width) / 2, this.HeaderHeight / 2 + this.HeaderLabelTextSize / 2, paint);
                }
            }
        }
        #endregion
    }
}
