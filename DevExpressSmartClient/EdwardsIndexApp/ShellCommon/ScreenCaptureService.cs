using System;
using System.IO;
using ShellCommon;
using System.Drawing;

namespace ShellCommon
{
    public class ScreenCaptureService : IScreenCaptureService
    {
        System.Windows.Forms.Timer t1;
        IntPtr handle;

        byte[] CapturedImage;

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            t1.Stop();

            try
            {
                ScreenCapture sc = new ScreenCapture();
                // capture entire screen, and save it to a file
                Image img = sc.CaptureWindow(handle);

                var imageBuffer = imageToByteArray(img);

                if (ImageCaptured != null)
                    ImageCaptured(imageBuffer);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CaptureWindow(IntPtr Handle)
        {
            handle = Handle;

            t1 = new System.Windows.Forms.Timer();

            t1.Tick += TimerEventProcessor;

            t1.Interval = 500;

            t1.Start();
        }

        public void CaptureScreen()
        {
            throw new NotImplementedException();
        }

        public event ImageCaptured ImageCaptured;
    }
}
