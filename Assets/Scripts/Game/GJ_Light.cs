using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Game
{
    public class GJ_Light : MonoBehaviour
    {
        [Header("Shadow Puzzle"), Space(10)]
        [SerializeField] private GameObject m_wall;
        [SerializeField] private GameObject[] puzzleObjects;
        [SerializeField] private float m_colliderWidth = 3f;

        [Header("Light Properties"), Space(10)]
        [SerializeField] private Light m_light;
        [SerializeField] private bool m_turnedOn = true;
        [SerializeField] private bool m_puzzleLight = false;

        // Use this for initialization
        void Start()
        {
            m_light.enabled = m_turnedOn;

            if (m_puzzleLight)
            {
                if (m_turnedOn)
                {
                    CalculateShadows();
                }
            }
        }

        public void TurnOnOff()
        {
            m_turnedOn = !m_turnedOn;

            m_light.enabled = m_turnedOn;

            if (m_puzzleLight)
            {
                if (m_turnedOn)
                {
                    CalculateShadows();
                }
                else
                {

                }
            }
        }

        void CalculateShadows()
        {
            // Wall properties
            Vector3 wallPoint = m_wall.transform.position;
            Vector3 wallNormal = m_wall.transform.up;
            // Plane for this wall
            Plane plane = new Plane(wallNormal, wallPoint);

            // Origin of light to cast shadows from
            Vector3 originPoint = this.transform.position;

            // We check every cube in the list (puzzle cubes)
            foreach (GameObject obj in puzzleObjects)
            {
                Rigidbody objRigidbody = obj.GetComponent<Rigidbody>();
                if (objRigidbody)
                {
                    objRigidbody.isKinematic = true;
                }

                // List to store the intersection points
                List<Vector3> intersectionPoints = new List<Vector3>();

                MeshCollider meshCol = obj.GetComponent<MeshCollider>();

                if (!meshCol)
                    continue;

                // We get the vertices of the cube. There could be repeated point, so we filter them out
                List<Vector3> verticesList = new List<Vector3>();
                foreach (Vector3 vertex in meshCol.sharedMesh.vertices)
                {
                    if (!verticesList.Contains(vertex))
                    {
                        verticesList.Add(vertex);
                    }
                }
                Vector3[] vertices = verticesList.ToArray();

                List<Vector3> meshPointsList = new List<Vector3>();
                // The points don't have the transformation applied, so we must apply it ourselves
                foreach (Vector3 vertex in vertices)
                {
                    // First we scale the point
                    vertex.Scale(obj.transform.localScale);
                    // Then we apply the rotation and traslation from the GameObject
                    Vector3 worldVertex = obj.transform.position + obj.transform.rotation * vertex;

                    // We need a Ray object to cast the intersection. For that Ray we create the vector
                    // The normalization will help us later
                    Vector3 rayVector = worldVertex - originPoint;
                    rayVector.Normalize();
                    Ray ray = new Ray(originPoint, rayVector);

                    float dist = 0.0f;
                    if (plane.Raycast(ray, out dist) && dist > 0.0f)
                    {
                        // First part of the condition is True if the ray isn't parallel to the Plane
                        // Second part checks the Ray points to the Plane (it could be a "negative" intersection)

                        Vector3 intersectionPoint = rayVector * dist;
                        meshPointsList.Add(intersectionPoint + wallNormal * m_colliderWidth);
                        meshPointsList.Add(intersectionPoint - wallNormal * m_colliderWidth);
                    }
                }//end foreach vertex

                Vector3[] meshPoints = meshPointsList.ToArray();

                List<int> testTriangles = new List<int>();
                for (int i = 0; i < meshPoints.Length - 2; i++)
                {
                    testTriangles.Add(i);
                    testTriangles.Add(i + 1);
                    testTriangles.Add(i + 2);
                }


                MeshCollider meshFilter = this.gameObject.AddComponent<MeshCollider>();
                meshFilter.sharedMesh = null;
                meshFilter.sharedMesh = new Mesh
                {
                    vertices = meshPoints,
                    triangles = testTriangles.ToArray()
                };
                meshFilter.convex = true;

            }//end foreach cube

        }//end LightOn
    }
}