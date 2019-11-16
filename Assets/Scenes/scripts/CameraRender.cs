

using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;

public class CameraRender : MonoBehaviour {

    public ARCoreBackgroundRenderer backgroundRenderer;
    public Camera renderCamera;
    public GameObject cube;

    public RectTransform rect;
    public RawImage rawImage;

    void Start() {

    }

    void Update() {
        //cube.GetComponent<Renderer>().material.SetTexture("tex", renderCamera.targetTexture);
        //cube.GetComponent<Renderer>().material = backgroundRenderer.BackgroundMaterial;
        // rawImage.material = backgroundRenderer.BackgroundMaterial;
        // rawImage.texture = backgroundRenderer.BackgroundMaterial; // Frame.CameraImage.Texture; 
        // Material mat = backgroundRenderer.BackgroundMaterial;

        // AppLog.Info(Screen.width + ", " + Screen.height);
        rect.sizeDelta = new Vector2(Screen.width / 4, Screen.height / 4);
        rect.localPosition = new Vector3(Screen.width * 3 / 8, Screen.height * 3 / 8, 0);
    }
}
