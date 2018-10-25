using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Pomodoro.iOS.Renderes;
using Pomodoro.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(MasterDetailRenderer))]

namespace Pomodoro.iOS.Renderes
{
    internal class MasterDetailRenderer : PhoneMasterDetailRenderer
    {
        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            if (!(Element is PomodoroMasterDetailPage mdp)) return;
            if (!(Platform.GetRenderer(mdp.Detail) is UINavigationController nc)) return;

            UIButton btn = new UIButton(UIButtonType.Custom);
            btn.Frame = new CGRect(0, 0, 50, 44);
            var img = UIImage.FromFile("menu.png");
            btn.SetTitle(string.Empty, UIControlState.Normal);
            btn.SetImage(img, UIControlState.Normal);
            btn.TouchUpInside += (sender, e) => mdp.IsPresented = true;
            nc.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                ForegroundColor = UIColor.White
            };

            var lbbi = new UIBarButtonItem(btn);
            nc.NavigationBar.TopItem.LeftBarButtonItem = lbbi;
        }
    }
}
