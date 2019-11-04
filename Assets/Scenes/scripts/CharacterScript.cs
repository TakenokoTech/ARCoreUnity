using RootMotion.FinalIK;
using UnityEngine;
using VRM;

public class CharacterScript : MonoBehaviour {

    public GameObject targetHead;
    public GameObject targetLeftArm;
    public GameObject targetRightArm;
    public GameObject targetLeftLeg;
    public GameObject targetRightLeg;
    public GameObject targetLooking;

    private VRMLookAtHead vrmLookAtHead;
    private VRIK vrIK;

    void Start() {
        vrmLookAtHead = GetComponent<VRMLookAtHead>();
        vrIK = gameObject.AddComponent<VRIK>();

        _SetupVRIK();
        _SetupLockAtHead();
    }

    void Update() {

    }

    protected virtual void _SetupLockAtHead() {
        vrmLookAtHead.Target = targetLooking.transform;
        vrIK.solver.OnPostUpdate += vrmLookAtHead.LookWorldPosition;
    }

    protected virtual void _SetupVRIK() {
        vrIK.AutoDetectReferences();
        vrIK.solver.plantFeet = false;

        vrIK.solver.spine.headTarget = targetHead.transform;
        vrIK.solver.spine.positionWeight = 1.0F;
        vrIK.solver.spine.rotationWeight = 1.0F;

        //vrIK.solver.leftArm.positionWeight = 0.0F;
        //vrIK.solver.leftArm.rotationWeight = 0.0F;
        //vrIK.solver.rightArm.positionWeight = 0.0F;
        //vrIK.solver.rightArm.rotationWeight = 0.0F;

        vrIK.solver.leftArm.target = targetLeftArm.transform;
        vrIK.solver.leftArm.stretchCurve = new AnimationCurve();
        vrIK.solver.leftArm.positionWeight = 1.0F;
        vrIK.solver.leftArm.rotationWeight = 1.0F;

        vrIK.solver.rightArm.target = targetRightArm.transform;
        vrIK.solver.rightArm.stretchCurve = new AnimationCurve();
        vrIK.solver.rightArm.positionWeight = 1.0F;
        vrIK.solver.rightArm.rotationWeight = 1.0F;


        vrIK.solver.leftLeg.target = targetLeftLeg.transform;
        vrIK.solver.leftLeg.stretchCurve = new AnimationCurve();
        vrIK.solver.leftLeg.positionWeight = 1.0F;
        vrIK.solver.leftLeg.rotationWeight = 0.0F;

        vrIK.solver.rightLeg.target = targetRightLeg.transform;
        vrIK.solver.rightLeg.stretchCurve = new AnimationCurve();
        vrIK.solver.rightLeg.positionWeight = 1.0F;
        vrIK.solver.rightLeg.rotationWeight = 0.0F;

        vrIK.solver.locomotion.weight = 1.0F;
    }
}
