using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Light2D))]
public class FOV : MonoBehaviour
{
    [SerializeField] private float viewRadius;
    [SerializeField] private int rayCount = 360;
    [SerializeField] private LayerMask obstacleMask;
    private Mesh _viewMesh;
    private Light2D _viewLight;
    
    protected virtual void Start()
    {
        _viewMesh = new Mesh();
        _viewMesh.name = "FOV Mesh";
        GetComponent<MeshFilter>().mesh = _viewMesh;
        _viewLight = GetComponent<Light2D>();
    }
    
    
    protected void CreatFieldOfView()
    {

        List<Vector3> viewPoints = new List<Vector3>();
        float angleStep = 360f / rayCount;

        // Raycast in fan
        for (int i = 0; i <= rayCount; i++)
        {

            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0f);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);

            if (hit.collider != null)
            {
                //If it collides the vertex is set at the impact point
                viewPoints.Add(transform.InverseTransformPoint(hit.point));
            }
            else
            {
                //If it doesn't collide its set at max range
                viewPoints.Add(transform.InverseTransformPoint(transform.position + dir * viewRadius));
            }
            
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(viewPoints.Count - 1) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < viewPoints.Count; i++)
        {
            vertices[i + 1] = viewPoints[i];

            if (i < viewPoints.Count - 1)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
        _viewLight.pointLightOuterRadius =  viewRadius;
    }

    protected void SetViewRadius(float radius)
    {
        viewRadius = radius;
    }
    
/*public void SetRadius(float maxHP, float HP)
    {
        _viewRadius = ((float)HP / maxHP) * maxRadius;
    }*/
    
/*
 Este codigo comentado es para un fov que no sea de 360

 [SerializeField] private float viewAngle = 90f;

 Remplazar en angleStep 360 por viewAngle

 angle por este angle

 /*float angle = transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * i;
            Vector3 dir = DirFromAngle(angle);* /

igualar dir a esta funcion
Vector3 DirFromAngle(float angleInDegrees)
    {
        // En 2D con rotaci�n en Z, usamos seno y coseno invertidos para el eje Z de Unity
        float rad = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0f);
    }
*/    
    
}
