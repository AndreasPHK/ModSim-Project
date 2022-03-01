using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaterBehaviour {

    // Simulate dispersion - EXAMPLE
    public static void Dispersion(Vector3[] waterData, float[] velocities) {

        // Get size
        int vertexDensity = (int) Mathf.Sqrt(velocities.Length);

        // Set Accelerations - Source: https://github.com/bughunterstudios/Dif-E-Q_Net
        float[] accelerations = new float[vertexDensity * vertexDensity];
        for (int x = 0; x < vertexDensity; x++) {
            for (int z = 0; z < vertexDensity; z++) {

                // Booleans to check edges
                bool le = x == 0;
                bool re = x == (vertexDensity - 1);
                bool ue = z == 0;
                bool de = z == (vertexDensity - 1);

                float height = waterData[x * vertexDensity + z].y;

                // Turnary operator (?) is used so that I don't try and pull from outside of waterData boundaries. Just an alternative to an if statement
                float changex = re ? 0 : waterData[(x + 1) * vertexDensity + z].y;
                changex += le ? 0 : waterData[(x - 1) * vertexDensity + z].y;
                changex -= le || re ? height : 2 * height;

                float changey = de ? 0 : waterData[x * vertexDensity + (z + 1)].y;
                changey += ue ? 0 : waterData[x * vertexDensity + (z - 1)].y;
                changey -= ue || de ? height : 2 * height;

                accelerations[x * vertexDensity + z] = (1 / 1) * (changex + changey);
            }
        }

        //Set Velocities and Heights
        for (int x = 0; x < vertexDensity; x++) {
            for (int z = 0; z < vertexDensity; z++) {
                
                //Otherwise we need to calculate velocities based on acceleration.
                velocities[x * vertexDensity + z] += accelerations[x * vertexDensity + z] * 0.06f;
                velocities[x * vertexDensity + z] *= 0.99f;
                waterData[x * vertexDensity + z].y += velocities[x * vertexDensity + z] * 0.06f;
            }
        }
    }
}