using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Force components
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Water : MonoBehaviour {

    // Vector3 containing water data for every point in mesh
    // Wave velocity (Vector3.magnitude), wave direction (Vector3.normalized) and density (Vector3.y)
    private Vector3[] waterData;
    private float[] velocities;

    // Mesh vertexDensity
    public int vertexDistance = 4;
    public int vertexDensity = 25;

    // Components
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start() {

        // Get MeshFilter component, generate mesh and add material to mesh
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = MeshGenerator.GenerateMesh(vertexDistance, vertexDensity);
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("WaterMat");

        // Init array
        velocities = new float[vertexDensity * vertexDensity];
        waterData = new Vector3[vertexDensity * vertexDensity];
    }
    
    // Update is called once per frame
    void Update() {

        // Check for button press
        if (Input.GetKeyDown("space")) {
            waterData[Random.Range(0, vertexDensity) * vertexDensity + Random.Range(0, vertexDensity)].y = 10;
        }
        
        WaterBehaviour.Dispersion(waterData, velocities);
        UpdateMesh();
    }

    // Update mesh based on waterData
    void UpdateMesh() {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int x = 0; x < vertexDensity; x++) {
            for (int z = 0; z < vertexDensity; z++) {
                vertices[x * vertexDensity + z].y = waterData[x * vertexDensity + z].y;
            }
        }
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateBounds();
    }
}