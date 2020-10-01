using System;
using Xamarin.Forms;

namespace Pomodoro.Services
{
    public interface IStatusBarColorService
    {
        void ChangeColor(Color color);
    }
}
