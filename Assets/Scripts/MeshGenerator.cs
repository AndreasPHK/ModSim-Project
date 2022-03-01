using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {

    // Generates a new mesh - SOURCE: https://gist.github.com/runewake2/b382ecd3abc3a32b096d08cc69c541fb, modified
    public static Mesh GenerateMesh(int vertexDistance, int vertexDensity) {

        // Mesh data
        Mesh mesh = new Mesh();
        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        // Vertices, normals and uv generation
        for (int x = 0; x <= vertexDensity; ++x) {
            for (int z = 0; z <= vertexDensity; ++z) {
                float xCord = vertexDistance * (x - vertexDensity * 0.5f);
                float zCord = vertexDistance * (z - vertexDensity * 0.5f);
                float yCord = 0;
                
                vertices.Add(new Vector3(xCord, yCord, zCord));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)vertexDensity, z / (float)vertexDensity));
            }
        }

        // Triangle generation
        var triangles = new List<int>();
        var vertCount = vertexDensity + 1;
        for (int i = 0; i < vertCount * vertCount - vertCount; ++i) {
            if ((i + 1) % vertCount == 0) {
                continue;
            }
            triangles.AddRange(new List<int>() {
                i + 1 + vertCount, i + vertCount, i,
                i, i + 1, i + vertCount + 1
            });
        }

        // Move data to mesh
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }
}