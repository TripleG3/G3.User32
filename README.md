# G3.User32
## Project for hooking libraries in Windows applications such as keyboard and mouse.

Example hooking / listening to the keyboard:
```csharp
public class Keyboard
{
    private readonly KeyHook keyHook;

    public Keyboard()
    {
        keyHook = new KeyHook();
        keyHook.KeyEvent += KeyHookKeyEvent;
    }

    public void Hook() => keyHook.Hook();
    public void UnHook() => keyHook.UnHook();

    private void KeyHookKeyEvent(object? sender, KeyEventArgs e)
    {
        // Arguments derived from KBDLLHOOKSTRUCT using SetWindowsHookExA hooking WH_KEYBOARD_LL and parsing KBDLLHOOKSTRUCTFlags
        // e.IsExtendedKey
        // e.IsInjected
        // e.IsAltDown
        // e.IsKeyUp
        // e.VirtualKeyCode
        // e.ScanCode
        // e.Time
        // e.ExtraInfo
        // and more
    }
}
```

More practical example injecting ```IKeyboardHook``` and properly disposing:
```csharp
public sealed class KeyboardListener(IKeyboardHook keyboardHook) : IDisposable
{
    public event EventHandler<KeyboardHookEventArgs> KeyboardEvent = (sender, e) => { };

    public void Start()
    {
        keyboardHook.KeyEvent += KeyboardHookKeyEvent;
        keyboardHook.Hook();
    }

    public void Stop()
    {
        keyboardHook.UnHook();
        keyboardHook.KeyEvent -= KeyboardHookKeyEvent;
    }

    private void KeyboardHookKeyEvent(object? sender, KeyboardHookEventArgs e) => KeyEvent(this, e);

    public void Dispose()
    {
        Stop();
        GC.SuppressFinalize(this);
    }

    ~KeyboardListener() => Dispose();
}
```

Follow the examples for keyboard similarly for the mouse.