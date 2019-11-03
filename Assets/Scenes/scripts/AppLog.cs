using UnityEngine;

public sealed class AppLog : MonoBehaviour {
    private static readonly AndroidJavaClass ajc = new AndroidJavaClass("android.util.Log");

    public static void Info(string msg) {
        AppLog.ajc.CallStatic<int>("i", "ARCoreUnity", msg);
    }
}
