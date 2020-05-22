# KeyToProcess
Small piece of code that sends a key stroke to a process.


```Csharp
[DllImport("user32.dll")]
public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);        

static void Main(string[] args)
{
    string processName = "notepad";
    IntPtr keyToSend = (IntPtr)(Keys.F1);
    SendKeyTo(processName,keyToSend);
}

static void SendKeyTo(string processName, IntPtr keyToPress)
{
    const uint wMsg_KEY_DOWN = 0x0100;
    const uint wMsg_KEY_UP = 0x0101;

    Process process = Process.GetProcessesByName(processName).FirstOrDefault();
    if(process != null){
        IntPtr hWnd = process.MainWindowHandle;
        PostMessage(hWnd, wMsg_KEY_DOWN,keyToPress, IntPtr.Zero);
        PostMessage(hWnd, wMsg_KEY_UP, keyToPress, IntPtr.Zero);
    }
    else
    {
        MessageBox.Show("The process name did not found any process.");
    }            
}
```

---

This code import the user32.dll and inject an method from it.

```Csharp
[DllImport("user32.dll")]
public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
```
It works like this
```Csharp
[DllImport("the dll you want to import")]
public static extern (Header of the method);
```

>When a method declaration includes an extern modifier, that method is said to be an external method. External methods are implemented externally, typically using a language other than C#. Because an external method declaration provides no actual implementation, the method-body of an external method simply consists of a semicolon. An external method may not be generic. The extern modifier is typically used in conjunction with a DllImport attribute, allowing external methods to be implemented by DLLs (Dynamic Link Libraries). The execution environment may support other mechanisms whereby implementations of external methods can be provided. When an external method includes a DllImport attribute, the method declaration must also include a static modifier.
<!---
Quoted from : https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#external-methods
-->

---

For PostMessage, the syntax is 
```BOOL PostMessageA(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);```
 - hWnd: The handle to the window which you want to send message to.
 - Msg: The message you want to send to the window.
 - wParam: Additional message-specific information.
 - lParam: Additional message specific information.

In this program, KEY_DOWN and KEY_UP message are used. Other message can be find [here](https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues)

For [KEY_DOWN](https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keydown) and [KEY_UP](https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keyup) message, their wParam is the [virtual-key code](https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes) of the nonsystem key. lParam refer [here](https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keydown#parameters).

Quick note: [Keys Enum](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=netcore-3.1) converted to IntPtr can be use as wParam of KEY_UP and KEY_DOWN. (For example Keys.F1 is 112(10 base) virtual key code of F1 is 0x70(16 base))