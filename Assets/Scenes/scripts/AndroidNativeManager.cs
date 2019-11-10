using UnityEngine;
using UnityEngine.UI;

public class AndroidNativeManager : MonoBehaviour {
    public static readonly string ANDROID_NATIVE_PLUGIN_CLASS = "tech.takenoko.arcoreunitylibrary.CameraWrapperKt";

    public RawImage image;

    void Start() {
        AppLog.Info("AndroidNativeManager.Start()");
        CallAndroidPlugin();
    }

    void Update() {
        // AppLog.Info("AndroidNativeManager.Update()");
    }

    public void CallAndroidPlugin() {
        // AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        // AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        // AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        using (AndroidJavaObject androidJavaClass = new AndroidJavaObject(ANDROID_NATIVE_PLUGIN_CLASS)) {
            AppLog.Info(androidJavaClass.ToString());
            androidJavaClass.CallStatic("execute" /*, context */);
        }
    }

    public void CallbackMethod(string message) {
        AppLog.Info("CallbackMethod: " + message.Length);
        byte[] bytes = System.Convert.FromBase64String(message);
        var texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        image.texture = texture;
    }
}
