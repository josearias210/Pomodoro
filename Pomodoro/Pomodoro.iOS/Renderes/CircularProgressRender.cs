using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Pomodoro.Controls;
using Pomodoro.iOS.Renderes;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircularProgress), typeof(CircularProgressRender))]

namespace Pomodoro.iOS.Renderes
{
    [Preserve(AllMembers = true)]
    public class CircularProgressRender : ViewRenderer
    {
        private float? _radius;
        private bool _sizeChanged = false;

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (CGContext g = UIGraphics.GetCurrentContext())
            {
                var CircularProgress = (CircularProgress)Element;

                var lineWidth = 5f;
                var radius = (int)GetRadius(lineWidth);
                var progress = (float)((CircularProgress.Progress * (100 / CircularProgress.Max)) / 100);
                var backColor = CircularProgress.ProgressBackgroundColor.ToUIColor();
                var frontColor = CircularProgress.ProgressColor.ToUIColor();

                DrawCircularProgress(g, Bounds.GetMidX(), Bounds.GetMidY(), progress, lineWidth, radius, backColor, frontColor);
            };
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var CircularProgress = (CircularProgress)this.Element;

            if (e.PropertyName == CircularProgress.ProgressProperty.PropertyName ||
                e.PropertyName == CircularProgress.ProgressBackgroundColorProperty.PropertyName ||
                e.PropertyName == CircularProgress.ProgressColorProperty.PropertyName)
            {
                SetNeedsDisplay();
            }

            if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
               e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                _sizeChanged = true;
                SetNeedsDisplay();
            }
        }

        private void DrawCircularProgress(CGContext g, nfloat x0, nfloat y0,
                                     nfloat progress, nfloat lineThickness, nfloat radius,
                                     UIColor backColor, UIColor frontColor)
        {
            g.SetLineWidth(lineThickness);

            // Draw background circle
            CGPath path = new CGPath();

            backColor.SetStroke();

            path.AddArc(x0, y0, radius, 0, 2.0f * (float)Math.PI, true);
            g.AddPath(path);
            g.DrawPath(CGPathDrawingMode.Stroke);

            // Draw progress circle
            var pathStatus = new CGPath();
            frontColor.SetStroke();

            var startingAngle = 1.5f * (float)Math.PI;
            pathStatus.AddArc(x0, y0, radius, startingAngle, startingAngle + progress * 2 * (float)Math.PI, false);

            g.AddPath(pathStatus);
            g.DrawPath(CGPathDrawingMode.Stroke);
        }

        private nfloat GetRadius(nfloat lineWidth)
        {
            if (_radius == null || _sizeChanged)
            {
                _sizeChanged = false;

                nfloat width = Bounds.Width;
                nfloat height = Bounds.Height;
                var size = (float)Math.Min(width, height);

                _radius = (size / 2f) - ((float)lineWidth / 2f);
            }

            return _radius.Value;
        }
    }
}
