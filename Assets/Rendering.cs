namespace Takenoko.Tech.AugmentedFaces {

    using System.Collections.Generic;
    using UnityEngine;

    public class Rendering : MonoBehaviour {


        private ARCoreAugmentedFaceMeshFilter filter;
        private float scale = 0.003F;
        private Dictionary<string, GameObject> m_BallDicVertices = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> m_BallDicNormals = new Dictionary<string, GameObject>();

        public void Awake() {
            this.filter = GetComponent<ARCoreAugmentedFaceMeshFilter>();
        }

        void Start() {
        }

        void Update() {
            _UpdateBall();
        }


        private void _UpdateBall() {
            Debug.Log("_UpdateBall");

            for (int i = 0; i < filter.m_MeshVertices.Count; i++) {
                string name = "m_BallDicVertices-" + i;
                if (!m_BallDicVertices.ContainsKey(name)) {
                    m_BallDicVertices[name] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    m_BallDicVertices[name].GetComponent<Renderer>().material.color = Color.red;
                }
                m_BallDicVertices[name].name = name;
                m_BallDicVertices[name].transform.localPosition = filter.m_MeshVertices[i];
                m_BallDicVertices[name].transform.localScale = new Vector3(scale, scale, scale);
                m_BallDicVertices[name].transform.parent = gameObject.transform;
                m_BallDicVertices[name].layer = gameObject.layer;
            }

            for (int i = 0; i < filter.m_MeshNormals.Count; i++) {
                string name = "m_BallDicNormals-" + i;
                if (!m_BallDicNormals.ContainsKey(name)) {
                    m_BallDicNormals[name] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    m_BallDicNormals[name].GetComponent<Renderer>().material.color = Color.green;
                }
                m_BallDicNormals[name].name = name;
                m_BallDicNormals[name].transform.localPosition = filter.m_MeshNormals[i];
                m_BallDicNormals[name].transform.localScale = new Vector3(scale, scale, scale);
                m_BallDicNormals[name].transform.parent = gameObject.transform;
                m_BallDicNormals[name].layer = gameObject.layer;
            }
        }
    }
}