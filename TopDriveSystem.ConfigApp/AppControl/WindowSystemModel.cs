using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;
using AlienJust.Support.Wpf;
using TopDriveSystem.ConfigApp.AppControl.ParamLogger;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Oscilloscope;

namespace TopDriveSystem.ConfigApp.AppControl
{
	/// <summary>
	///     ������ ������� ���� ����������
	/// </summary>
	internal class WindowSystemModel : IWindowSystemModel
    {
        private readonly List<Color> _colors;
        private readonly IParamLoggerRegistrationPoint _paramLoggerRegPoint;
        private Action _oscilloscopeWindowCloseFunc;


        public WindowSystemModel()
        {
            var colors = new List<Color>
            {
                Colors.LawnGreen,
                Colors.Red,
                Colors.Cyan,
                Colors.Yellow,
                Colors.Coral,
                Colors.LightGreen,
                Colors.HotPink,
                Colors.DeepSkyBlue,
                Colors.Gold,
                Colors.Orange,
                Colors.Violet,
                Colors.White,
                Colors.Fuchsia,
                Colors.LightSkyBlue,
                Colors.LightGray,
                Colors.Khaki,
                Colors.SpringGreen,
                Colors.Tomato,
                Colors.LightCyan,
                Colors.Goldenrod,
                Colors.SlateBlue,
                Colors.Cornsilk,
                Colors.MediumPurple,
                Colors.RoyalBlue,
                Colors.MediumVioletRed,
                Colors.MediumTurquoise
            };
        }

        public void ShowOscilloscopeWindow()
        {
            var oscilloscopeWindowWaiter = new ManualResetEvent(false);
            var oscilloscopeWindowThread = new Thread(() =>
            {
                var waitableNotifier = new WpfUiNotifier(Dispatcher.CurrentDispatcher);
                var uiRoot = new SimpleUiRoot(new WpfUiNotifierAsync(Dispatcher.CurrentDispatcher));
                var oscilloscopeWindow = new OscilloscopeWindow(_colors) {DataContext = new OscilloscopeWindowSciVm()};
                _paramLoggerRegPoint.RegisterLoggegr(oscilloscopeWindow);
                oscilloscopeWindow.Show();

                _oscilloscopeWindowCloseFunc = () => waitableNotifier.Notify(() => oscilloscopeWindow.Close());
                oscilloscopeWindowWaiter.Set();
                Dispatcher.Run();
            });
            oscilloscopeWindowThread.SetApartmentState(ApartmentState.STA);
            oscilloscopeWindowThread.IsBackground = true;
            oscilloscopeWindowThread.Priority = ThreadPriority.BelowNormal;
            oscilloscopeWindowThread.Start();
            oscilloscopeWindowWaiter.WaitOne();
        }

        public void HideOscilloscopeWindow()
        {
            try
            {
                _oscilloscopeWindowCloseFunc?.Invoke();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                // TODO: log exception
            }

            _oscilloscopeWindowCloseFunc = null;
        }
    }
}