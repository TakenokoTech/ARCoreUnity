using RootMotion.FinalIK;
using UnityEngine;
using VRM;

public class CharacterScript : MonoBehaviour {

    public GameObject head;
    public GameObject headTarget;

    private VRMLookAtHead vrmLookAtHead;
    private VRIK vrik;
    private LookAtIK lookAtIk;
    private Animator animator;


    void Start() {
        vrmLookAtHead = GetComponent<VRMLookAtHead>();
        vrik = GetComponent<VRIK>();
        lookAtIk = GetComponent<LookAtIK>();
        animator = GetComponent<Animator>();

        vrik.solver.OnPostUpdate += vrmLookAtHead.LookWorldPosition;

        lookAtIk.solver.OnPostUpdate += vrmLookAtHead.LookWorldPosition;
        lookAtIk.solver.OnPostUpdate += this.Move;
    }

    void Update() {

    }

    private void Move() {
        //head.transform.forward = new Vector3(head.transform.forward.x, head.transform.forward.y, headTarget.transform.forward.z);
    }
}
