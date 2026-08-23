using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    private Resolution currentRes;
    // private float TargetAspectRatio = (float)Screen.width / Screen.height;
    private float TargetAspectRatio = 192f / 108f;

    // WinAPI 
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private IntPtr _windowHandle;
    private RECT _windowRect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRes = Screen.currentResolution;
        // TargetAspectRatio = (float)Screen.width / Screen.height;

        // get window handle
        _windowHandle = GetActiveWindow();
        GetWindowRect(_windowHandle, ref _windowRect);
    }

    // Update is called once per frame
    void Update()
    {
        GetWindowRect(_windowHandle, ref _windowRect);
        int currentWidth = _windowRect.Right - _windowRect.Left;
        int currentHeight = _windowRect.Bottom - _windowRect.Top;

        // cal ratio
        float currentAspectRatio = (float)currentWidth / currentHeight;

        // if the ratio has problem, reset ratio
        if (Mathf.Abs(currentAspectRatio - TargetAspectRatio) > 0.01f)
        {
            // accorrding width to cal height
            int targetHeight = Mathf.RoundToInt(currentWidth / TargetAspectRatio);
            MoveWindow(_windowHandle, _windowRect.Left, _windowRect.Top, currentWidth, targetHeight, true);
        }
    }
}
