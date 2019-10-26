using TMPro;
using UnityEngine;

public class PointTextScript : MonoBehaviour {

    public TextMeshProUGUI textMesh;
    public string text;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        textMesh.text = text;
    }
}
