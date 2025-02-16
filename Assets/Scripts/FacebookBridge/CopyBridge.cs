using System.Runtime.InteropServices;

public static class CopyBridge
{
    [DllImport("__Internal")]
    public static extern void CopyText(string str);
}
