using RootMotion.FinalIK;
using UnityEngine;
using VRM;

public class CharacterScript : MonoBehaviour {

    public GameObject head;
    public GameObject headTarget;

    void Start() {
        VRMLookAtHead vrmLookAtHead = GetComponent<VRMLookAtHead>();
        LookAtIK lookAtIk = GetComponent<LookAtIK>();
        Animator animator = GetComponent<Animator>();

        lookAtIk.solver.OnPostUpdate += vrmLookAtHead.LookWorldPosition;
        lookAtIk.solver.OnPostUpdate += this.Move;
    }

    void Update() {

    }

    private void Move() {
        //head.transform.forward = new Vector3(head.transform.forward.x, head.transform.forward.y, headTarget.transform.forward.z);
    }
}
