using UnityEngine;

enum AppStatus { face, Xtuber }

public class ButtonScript : MonoBehaviour {

    public GameObject faceTexture;
    public GameObject maker;

    public GameObject xtuber;

    private AppStatus status = AppStatus.Xtuber;

    void Start() {
        _setStatusXtuber();
    }

    public void OnClick() {
        switch (status) {
            case AppStatus.face:
                _setStatusXtuber();
                return;

            case AppStatus.Xtuber:
                _setStatusFace();
                return;
        }
    }

    private void _setStatusXtuber() {
        AppLog.Info("Change state Xtuber.");
        status = AppStatus.Xtuber;
        xtuber.SetActive(true);
        faceTexture.GetComponent<MeshRenderer>().enabled = false;
        maker.SetActive(false);
    }

    private void _setStatusFace() {
        AppLog.Info("Change state Face.");
        status = AppStatus.face;
        xtuber.SetActive(false);
        faceTexture.GetComponent<MeshRenderer>().enabled = true;
        maker.SetActive(true);
    }
}
