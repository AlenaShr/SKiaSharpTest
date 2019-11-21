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
        public float MarginTopBottomOuter  => 25 * (float)KScale;
        public float PaddingBottomOuter =>  20 * (float)KScale;
        public float MarginLeftRightOuter => Device.Idiom == TargetIdiom.Tablet ? 10 * (float)KScale : 15 * (float)KScale;
        public SKColor BackgroundColorOuter => SKColors.LightGray;
        public SKColor BackgroundColorInner => SKColors.White;
        public SKColor TextColor => SKColors.Black;
        public string HeaderLabel => "APPLE";
        public float HeaderLabelTextSize => 24.0f * (float)KScale;
        public int InnerWidth { get; set; } 
        public int InnerHeight { get; set; } 
        public int OuterWidth { get; set; }
        public float HeaderHeight { get; set; }
        public double KScale { get; set; } = 1;
        #endregion

        #region Methods
        public void Draw(SKCanvas canvas, int width, int height, double kScale, SKColor color, string label)
        {
            KScale = kScale;
            this.DrawLayout(canvas, width, height, color, label);
        }

        private void DrawLayout(SKCanvas canvas, int width, int height, SKColor color, string label)
        {
            OuterWidth = width;
            HeaderHeight = CalculateHeaderHeight(label);
            InnerWidth = width - (int)(2 * MarginLeftRightOuter);
            InnerHeight = height - (int)(HeaderHeight + PaddingBottomOuter);
            this.DrawOuterGrid(canvas, width, height, color);
            this.DrawLabel(canvas, label);
            this.DrawInnerGrid(canvas);
        }

        private float CalculateHeaderHeight(string label)
        {
            var result = this.MarginTopBottomOuter;
            if (!string.IsNullOrEmpty(label))
            {
                result += this.HeaderLabelTextSize + this.MarginTopBottomOuter;
            }

            return result;
        }

        private void DrawOuterGrid(SKCanvas canvas, float width, float height, SKColor color)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = color
            })
            {
                var rect = SKRect.Create(0, 0, width, height);
                canvas.DrawRoundRect(new SKRoundRect(rect, 8, 8), paint);
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

        private void DrawLabel(SKCanvas canvas, string label)
        {
            if (!string.IsNullOrEmpty(label))
            {
                using (var paint = new SKPaint())
                {
                    paint.TextSize = this.HeaderLabelTextSize;
                    paint.IsAntialias = true;
                    paint.Color = TextColor;
                    paint.IsStroke = false;
                    paint.Typeface = Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android ?
                        SKTypeface.FromFile("KlavikaCHLightCond.otf")
                        : SKTypeface.FromFile("KlavikaCHLightCond.otf");
                    var bounds = new SKRect();
                    var text = label;
                    paint.MeasureText(text, ref bounds);

                    canvas.DrawText(text, (this.OuterWidth - bounds.Width) / 2, this.HeaderHeight / 2 + this.HeaderLabelTextSize / 2, paint);
                }
            }
        }
        #endregion
    }
}
