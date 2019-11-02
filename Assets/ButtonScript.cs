using UnityEngine;

enum AppStatus { face, Xtuber }

public class ButtonScript : MonoBehaviour {

    public GameObject faceTexture;
    public GameObject maker;

    public GameObject xtuber;

    private AppStatus status = AppStatus.Xtuber;

    void Start() {
        faceTexture.GetComponent<MeshRenderer>().enabled = false;
    }

    public void OnClick() {
        switch (status) {
            case AppStatus.face:
                status = AppStatus.Xtuber;
                xtuber.SetActive(true);
                faceTexture.GetComponent<MeshRenderer>().enabled = false;
                maker.SetActive(false);
                return;

            case AppStatus.Xtuber:
                status = AppStatus.face;
                xtuber.SetActive(false);
                faceTexture.GetComponent<MeshRenderer>().enabled = true;
                maker.SetActive(true);
                return;
        }
    }
}
