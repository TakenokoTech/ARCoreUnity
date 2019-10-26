namespace Takenoko.Tech.AugmentedFaces {

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

        public void Awake() {
            filter = GetComponent<ARCoreAugmentedFaceMeshFilter>();
        }

        void Start() {
            // targetHead.transform.position = transform.TransformPoint(new Vector3(0, 1.6F, 0));
            // targetCenter.transform.position = transform.TransformPoint(new Vector3(0, 0.3F, 0));
        }

        void Update() {
            _UpdateBall();
        }

        GameObject obj;
        private void _UpdateBall() {
            Debug.Log("_UpdateBall");

            /*
            obj = Instantiate(pointObj) as GameObject;
            obj.name = name;
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.transform.parent = gameObject.transform;
            obj.layer = gameObject.layer;
            */

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
                m_BallDicVertices[name].transform.parent = gameObject.transform;
                m_BallDicVertices[name].layer = gameObject.layer;

                if (i == 0) {
                    Vector3 vec = m_BallDicVertices[name].transform.TransformPoint(m_BallDicVertices[name].transform.localPosition);
                    targetHead.transform.position = new Vector3(vec.x, vec.y, vec.z);
                    targetCenter.transform.position = new Vector3(vec.x, vec.y - 1.3F, vec.z);
                    targetLeft.transform.position = new Vector3(vec.x + 0.5F, vec.y - 0.7F, vec.z);
                    targetRight.transform.position = new Vector3(vec.x - 0.5F, vec.y - 0.7F, vec.z);
                }
            }

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
                m_BallDicNormals[name].transform.parent = gameObject.transform;
                m_BallDicNormals[name].layer = gameObject.layer;
            }
        }
    }
}