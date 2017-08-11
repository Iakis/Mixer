using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waves : MonoBehaviour {

	public float waveHeight = 0.31f;
	public float speed = 1.18f;
	public float waveLength = 2f;
	public float randomHeight = 0.01f;
	public float randomSpeed = 9f;
    public int verts;

	private Vector3[] baseHeight;
    private Vector3[] vertices;
	private List<float> perVertexRandoms = new List<float>();
	private Mesh mesh;


	void Awake() {
		mesh = GetComponent<MeshFilter>().mesh;
		if (baseHeight == null) {
			baseHeight = mesh.vertices;
		}

		for(int i=0; i < baseHeight.Length; i++) {
			perVertexRandoms.Add(Random.value * randomHeight);
		}
	}

	void Update () {
		if (vertices == null) {
			vertices = new Vector3[baseHeight.Length];
		}

		for (int i=0;i<verts;i++) {
			Vector3 vertex = baseHeight[i];
			Random.InitState((int)((vertex.x) * (vertex.x) + (vertex.z) * (vertex.z)));
			vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x * waveLength + baseHeight[i].y * waveLength) * waveHeight;
			vertex.y += Mathf.Sin(Mathf.Cos(Random.value * 1.0f) * randomHeight * Mathf.Cos (Time.time * randomSpeed * Mathf.Sin(Random.value * 1.0f)));
			vertices[i] = vertex;
		}
        for (int i = verts; i < baseHeight.Length; i++)
        {
            Vector3 vertex1 = baseHeight[i];
            vertices[i] = vertex1;
        }
        mesh.vertices = vertices;
		mesh.RecalculateNormals();
	}
}