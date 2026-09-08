using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FOV : MonoBehaviour
{
    [Header("Configuraci�n de Visi�n")] 
    
    private float _viewRadius;

    [SerializeField] private float maxRadius = 5f;

    [SerializeField] private int rayCount = 360;
    [SerializeField] private LayerMask obstacleMask;

    private Mesh viewMesh;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "FOV Mesh";
        _viewRadius = maxRadius;
        GetComponent<MeshFilter>().mesh = viewMesh;
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {

        List<Vector3> viewPoints = new List<Vector3>();
        float angleStep = 360f / rayCount;

        // Lanzar rayos en abanico
        for (int i = 0; i <= rayCount; i++)
        {

            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0f);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, _viewRadius, obstacleMask);

            if (hit.collider != null)
            {
                // Si choca con un obstaculo, el vertice se coloca en el punto de impacto (convertido a espacio local)
                viewPoints.Add(transform.InverseTransformPoint(hit.point));
            }
            else
            {
                // Si no choca, llega al radio maximo
                viewPoints.Add(transform.InverseTransformPoint(transform.position + dir * _viewRadius));
            }
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(viewPoints.Count - 1) * 3];

        vertices[0] = Vector3.zero; // El origen es la posici�n del personaje
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

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
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