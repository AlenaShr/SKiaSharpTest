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

        #region MarginHeader
        private float _marginHeader; 
        public float MarginHeader
        {
            set { _marginHeader = value; }
            get { return _marginHeader * (float)KScale;}
            //=> 25 * (float)KScale;
        }
        #endregion

        #region LabelTextSize
        private float _labelTextSize;
        public float LabelTextSize
        {
            set { _labelTextSize = value; }
            get { return _labelTextSize * (float)KScale; }
            //=> 24.0f * (float)KScale;
        }
        #endregion

        #region PaddingInner
        private float _paddingOuter;
        public float PaddingOuter
        {
            set { _paddingOuter = value; }
            get { return _paddingOuter * (float)KScale; }
            //=> 20 * (float)KScale;
        }
        #endregion

        #region MarginInner
        private float _marginInner;
        public float MarginInner
        {
            set { _marginInner = value; }
            get { return _marginInner * (float)KScale; }
            //=> Device.Idiom == TargetIdiom.Tablet ? 10 * (float)KScale : 15 * (float)KScale;
        }
        #endregion
        public SKColor TextColor => SKColors.Black;
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
            InnerWidth = width - (int)(2 * MarginInner);
            InnerHeight = height - (int)(HeaderHeight + PaddingOuter);
            this.DrawOuterGrid(canvas, width, height, color);
            this.DrawLabel(canvas, label);
            this.DrawInnerGrid(canvas);
        }

        private float CalculateHeaderHeight(string label)
        {
            var result = this.MarginHeader;
            if (!string.IsNullOrEmpty(label))
            {
                result += this.LabelTextSize + this.MarginHeader;
            }

            return result;
        }

        private void DrawOuterGrid(SKCanvas canvas, float width, float height, SKColor color)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.LightGray //color
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
                Color = SKColors.White
            })
            {
                var rect = SKRect.Create(MarginInner, HeaderHeight, InnerWidth, InnerHeight);
                canvas.DrawRect(rect, paint);
            }
        }

        private void DrawLabel(SKCanvas canvas, string label)
        {
            if (!string.IsNullOrEmpty(label))
            {
                using (var paint = new SKPaint())
                {
                    paint.TextSize = this.LabelTextSize;
                    paint.IsAntialias = true;
                    paint.Color = TextColor;
                    paint.IsStroke = false;
                    paint.Typeface = Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android ?
                        SKTypeface.FromFile("KlavikaCHLightCond.otf")
                        : SKTypeface.FromFile("KlavikaCHLightCond.otf");
                    var bounds = new SKRect();
                    var text = label;
                    paint.MeasureText(text, ref bounds);

                    canvas.DrawText(text, (this.OuterWidth - bounds.Width) / 2, this.HeaderHeight / 2 + this.LabelTextSize / 2, paint);
                }
            }
        }
        #endregion
    }
}
