namespace Takenoko.Tech.AugmentedFaces {
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VRM;

    public class Rendering : MonoBehaviour {


        private ARCoreAugmentedFaceMeshFilter filter;

        private readonly float scale = 0.003F;
        private Dictionary<string, GameObject> m_BallDicVertices = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> m_BallDicNormals = new Dictionary<string, GameObject>();

        public GameObject pointObj;
        public GameObject character;

        public GameObject parent;
        public GameObject targetHead;
        public GameObject targetLeftArm;
        public GameObject targetRightArm;
        public GameObject targetLeftLeg;
        public GameObject targetRightLeg;
        public GameObject targetCenter;
        public GameObject faceInclination;

        public GameObject noSignal;

        private CharacterEntity entity = new CharacterEntity();

        const string PREFIX_VER = "m_BallDicVertices-";
        const string PREFIX_DIC = "m_BallDicNormals-";

        void Start() {
            filter = GetComponent<ARCoreAugmentedFaceMeshFilter>();
            entity.blendShapeProxy = character.GetComponent<VRMBlendShapeProxy>();
        }

        void Update() {
            // AppLog.Info("Rendering.Update()"); -10, 180, -60
            try {
                _UpdateMeshVerticesBall();
                _UpdateMeshNormalsBall();
                _CalcFacePosition();
                _CalcMouthPosition();
            }
            catch (Exception e) {
                AppLog.Info(e.ToString());
            }
        }

        private void _CalcFacePosition() {
            Vector3 vec = filter.m_CenterPose.position;
            Vector3 dir = filter.m_CenterPose.forward;
            //vec = new Vector3(0.0F, 0.1F, 0.4F);
            //dir = new Vector3(-0.1F, -0.0F, 0.9F);

            noSignal.SetActive(vec.x == 0 && vec.y == 0 && vec.z == 0);
            // noSignal.SetActive(false);
            // zAppLog.Info(vec.ToString() + dir.ToString());

            targetHead.transform.position = new Vector3(vec.x, vec.y - 0.1F, vec.z);
            targetHead.transform.forward = new Vector3(-dir.x, -dir.y, -dir.z);
            targetHead.transform.Rotate(new Vector3(0, 0, -1), _GetFaceInclination());

            targetLeftArm.transform.position = new Vector3(vec.x + 0.35F, vec.y - 0.8F, vec.z - 0.1F);
            targetRightArm.transform.position = new Vector3(vec.x - 0.35F, vec.y - 0.8F, vec.z - 0.1F);
            targetLeftLeg.transform.position = new Vector3(vec.x + 0.2F, vec.y - 1.5F, vec.z);
            targetRightLeg.transform.position = new Vector3(vec.x - 0.2F, vec.y - 1.5F, vec.z);

            // targetCenter.transform.position = new Vector3(vec.x, vec.y, vec.z);
            // targetCenter.transform.forward = new Vector3(dir.x, dir.y, dir.z);
        }

        /**
         * 顔のZ軸方向の回転を返却
         */
        private float _GetFaceInclination() {
            if (filter.m_MeshVertices.Count == 0) return 0;

            string name1 = PREFIX_VER + 10;
            Vector3 vecBall1 = m_BallDicVertices[name1].transform.TransformPoint(m_BallDicVertices[name1].transform.position);
            Vector3 dirBall1 = m_BallDicVertices[name1].transform.TransformDirection(m_BallDicVertices[name1].transform.forward);
            string name2 = PREFIX_VER + 152;
            Vector3 vecBall2 = m_BallDicVertices[name2].transform.TransformPoint(m_BallDicVertices[name2].transform.position);
            Vector3 dirBall2 = m_BallDicVertices[name2].transform.TransformDirection(m_BallDicVertices[name2].transform.forward);

            float dx = vecBall1.x - vecBall2.x;
            float dy = vecBall1.y - vecBall2.y;
            float z = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg - 90;
            faceInclination.transform.rotation = Quaternion.Euler(0, 0, z);

            return z;
        }

        /**
         * 口の形を計算
         */
        private void _CalcMouthPosition() {
            if (filter.m_MeshVertices.Count == 0) return;

            float minV = 0.015F, maxV = 0.04F;
            Vector3 vecTop = m_BallDicVertices[PREFIX_VER + 12].transform.position;
            Vector3 vecBottom = m_BallDicVertices[PREFIX_VER + 14].transform.position;
            float vertical = Vector3.Distance(vecTop, vecBottom);
            float perV = (vertical - minV) / (maxV - minV);

            float minH = 0.015F, maxH = 0.04F;
            Vector3 vecLeft = m_BallDicVertices[PREFIX_VER + 308].transform.position;
            Vector3 vecRight = m_BallDicVertices[PREFIX_VER + 78].transform.position;
            float horizontal = Vector3.Distance(vecLeft, vecRight);
            float a = horizontal - vertical;
            float perH = 1 - ((a - minH) / (maxH - minH));
            if (perV < 0.3 && perH < 0.3) {
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("A"), Math.Min(Math.Max(0.0F, perV), 1.0F));
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("I"), Math.Min(Math.Max(0.0F, (0.3F - perH) / 100F), 1.0F));
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("O"), 0.0F);
            }
            else if (perV > perH) {
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("A"), Math.Min(Math.Max(0.0F, perV), 1.0F));
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("I"), 0.0F);
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("O"), 0.0F);
            }
            else {
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("A"), 0.0F);
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("I"), 0.0F);
                entity.blendShapeProxy.ImmediatelySetValue(new BlendShapeKey("O"), Math.Min(Math.Max(0.0F, perH), 1.0F));
            }

            // AppLog.Info("perV: " + Math.Min(Math.Max(0.0F, perV), 1.0F));
            // AppLog.Info("perH: " + Math.Min(Math.Max(0.0F, perH), 1.0F));
        }

        /**
         * メッシュを配置
         */
        private void _UpdateMeshVerticesBall() {
            for (int i = 0; i < filter.m_MeshVertices.Count; i++) {
                string name = PREFIX_VER + i;
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
                string name = PREFIX_DIC + i;
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

        private class CharacterEntity {
            public VRM.VRMBlendShapeProxy blendShapeProxy;
        }
    }
}
