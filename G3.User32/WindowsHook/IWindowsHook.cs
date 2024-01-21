namespace G3.User32.WindowsHook;

public interface IWindowsHook
{
    bool IsDisabledNextHook { get; set; }
    bool IsDisabled { get; set; }
    nint HookHandle { get; }
    event EventHandler<WindowsHookEventArgs> HookProcessing;
    void Hook();
    void UnHook();
}