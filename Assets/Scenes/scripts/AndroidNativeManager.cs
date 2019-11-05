using UnityEngine;

public class AndroidNativeManager : MonoBehaviour {
    public static readonly string ANDROID_NATIVE_PLUGIN_CLASS = "tech.takenoko.arcoreunitylibrary.AndroidNativePlugin";

    void Start() {
        AppLog.Info("AndroidNativeManager.Start()");
    }

    void Update() {
        AppLog.Info("AndroidNativeManager.Update()");
        CallAndroidPlugin();
    }

    public void CallAndroidPlugin() {
        using (AndroidJavaObject androidJavaClass = new AndroidJavaObject(ANDROID_NATIVE_PLUGIN_CLASS)) {
            AppLog.Info(androidJavaClass.ToString());
            androidJavaClass.Call("execute");
        }
    }

    public void CallbackMethod(string message) {
        AppLog.Info("CallbackMethod: " + message);
    }
}
