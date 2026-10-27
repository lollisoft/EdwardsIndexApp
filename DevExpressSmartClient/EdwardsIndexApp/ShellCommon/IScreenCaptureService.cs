using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShellCommon
{
    public delegate void ImageCaptured(byte[] image);

    public interface IScreenCaptureService
    {
        // Creates a capture from the window by given handle.
        void CaptureWindow(IntPtr Handle);

        // Creates a capture from the whole screen.
        void CaptureScreen();

        event ImageCaptured ImageCaptured; 
    }
}
