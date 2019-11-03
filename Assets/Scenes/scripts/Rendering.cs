namespace Takenoko.Tech.AugmentedFaces {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Rendering : MonoBehaviour {


        private ARCoreAugmentedFaceMeshFilter filter;

        private readonly float scale = 0.003F;
        private Dictionary<string, GameObject> m_BallDicVertices = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> m_BallDicNormals = new Dictionary<string, GameObject>();

        public GameObject pointObj;

        public GameObject parent;
        public GameObject targetHead;
        public GameObject targetLeft;
        public GameObject targetRight;
        public GameObject targetCenter;

        public GameObject targetLook;
        public GameObject faceInclination;

        public GameObject noSignal;

        void Start() {
            filter = GetComponent<ARCoreAugmentedFaceMeshFilter>();
        }

        void Update() {
            AppLog.Info("Rendering.Update()");
            try {
                _UpdateMeshVerticesBall();
                _UpdateMeshNormalsBall();
                _CalcFacePosition();
            }
            catch (Exception e) {
                AppLog.Info(e.ToString());
            }
        }

        private void _CalcFacePosition() {
            Vector3 vec = filter.m_CenterPose.position;
            Vector3 dir = filter.m_CenterPose.forward;

            Debug.Log(filter.m_CenterPose.position.ToString());
            noSignal.SetActive(vec.x == 0 && vec.y == 0 && vec.z == 0);

            targetHead.transform.position = new Vector3(vec.x, vec.y - 0.1F, vec.z);
            targetHead.transform.forward = new Vector3(-dir.x, -dir.y, -dir.z);
            targetHead.transform.Rotate(new Vector3(0, 0, -1), _GetFaceInclination());
            targetLook.transform.position = new Vector3(vec.x, vec.y, vec.z);
            targetLook.transform.forward = new Vector3(dir.x, dir.y, dir.z);
            // targetCenter.transform.position = new Vector3(vec.x, vec.y, vec.z);
            // targetCenter.transform.forward = new Vector3(dir.x, dir.y, dir.z);
            targetLeft.transform.position = new Vector3(vec.x, vec.y - 1.5F, vec.z);
            targetRight.transform.position = new Vector3(vec.x, vec.y - 1.5F, vec.z);
        }

        /**
         * 顔のZ軸方向の回転を返却
         */
        private float _GetFaceInclination() {
            if (filter.m_MeshVertices.Count == 0) return 0;

            string name1 = "m_BallDicVertices-" + 10;
            Vector3 vecBall1 = m_BallDicVertices[name1].transform.TransformPoint(m_BallDicVertices[name1].transform.position);
            Vector3 dirBall1 = m_BallDicVertices[name1].transform.TransformDirection(m_BallDicVertices[name1].transform.forward);
            string name2 = "m_BallDicVertices-" + 152;
            Vector3 vecBall2 = m_BallDicVertices[name2].transform.TransformPoint(m_BallDicVertices[name2].transform.position);
            Vector3 dirBall2 = m_BallDicVertices[name2].transform.TransformDirection(m_BallDicVertices[name2].transform.forward);

            float dx = vecBall1.x - vecBall2.x;
            float dy = vecBall1.y - vecBall2.y;
            float z = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg - 90;
            faceInclination.transform.rotation = Quaternion.Euler(0, 0, z);

            return z;
        }

        /**
         * メッシュを配置
         */
        private void _UpdateMeshVerticesBall() {
            for (int i = 0; i < filter.m_MeshVertices.Count; i++) {
                string name = "m_BallDicVertices-" + i;
                if (!m_BallDicVertices.ContainsKey(name)) {
                    m_BallDicVertices[name] = Instantiate(pointObj) as GameObject; //GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    m_BallDicVertices[name].GetComponent<PointTextScript>().text = i.ToString();
                    m_BallDicVertices[name].SetActive(true);
                    //m_BallDicVertices[name].GetComponent<Renderer>().material.color = Color.red;
                }
                m_BallDicVertices[name].name = name;
                m_BallDicVertices[name].transform.localPosition = filter.m_MeshVertices[i];
                m_BallDicVertices[name].transform.localScale = new Vector3(scale, scale, scale);
                m_BallDicVertices[name].transform.parent = parent.transform;
                m_BallDicVertices[name].layer = gameObject.layer;
            }
        }

        /**
         * メッシュを配置
         */
        private void _UpdateMeshNormalsBall() {
            for (int i = 0; i < filter.m_MeshNormals.Count; i++) {
                string name = "m_BallDicNormals-" + i;
                if (!m_BallDicNormals.ContainsKey(name)) {
                    m_BallDicNormals[name] = Instantiate(pointObj) as GameObject; //GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    m_BallDicNormals[name].GetComponent<PointTextScript>().text = i.ToString();
                    m_BallDicNormals[name].SetActive(true);
                    //m_BallDicNormals[name].GetComponent<Renderer>().material.color = Color.green;
                }
                m_BallDicNormals[name].name = name;
                m_BallDicNormals[name].transform.localPosition = filter.m_MeshNormals[i];
                m_BallDicNormals[name].transform.localScale = new Vector3(scale, scale, scale);
                m_BallDicNormals[name].transform.parent = parent.transform;
                m_BallDicNormals[name].layer = gameObject.layer;
            }
        }
    }
}
