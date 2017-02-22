using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder_mesh : MonoBehaviour {
	
	public List<Transform> vertex_tr = new List<Transform>();
	public List<Vector3> vertex_v = new List<Vector3> ();
	public List<int> trinagles = new List<int>();
	private int j;
	public List<Vector3> norman_normals = new List<Vector3>();
	//public int[] trinagles = new int[15];


	public Mesh Quad() {

		Vector2[] uvs = new Vector2[vertex_tr.Count];
		for (int i = 0; i < uvs.Length; i++) {
			uvs [i] = new Vector2 (vertex_tr [i].localPosition.x, vertex_tr [i].localPosition.z);
		}
		if (norman_normals.Count < vertex_tr.Count)
			for (int i = 0; i < (vertex_tr.Count); i++) {
				if (norman_normals.Count < vertex_tr.Count)
					norman_normals.Add (Vector3.Cross (vertex_v [i + 1] - vertex_v [i], vertex_v [i + 2] - vertex_v [i]).normalized);
			}
		else if (norman_normals.Count > vertex_tr.Count)
			norman_normals.Clear ();

	
		if (trinagles.Count < ((vertex_tr.Count - 2) * 3)) {
			trinagles.Clear ();
			j = 1;
			for (int i = 2; i <= vertex_tr.Count - 1; i++) {
				if (j == 1) {
					if (trinagles.Count < ((vertex_tr.Count - 2) * 3))
						trinagles.Add (i);
					if (trinagles.Count < ((vertex_tr.Count - 2) * 3))
						trinagles.Add (i - 1);
					if (trinagles.Count < ((vertex_tr.Count - 2) * 3))
						trinagles.Add (i - 2);
					j = 2;
				} else {
					if (trinagles.Count < ((vertex_tr.Count - 2) * 3))
						trinagles.Add (i);
					if (trinagles.Count < ((vertex_tr.Count - 2) * 3))
						trinagles.Add (i - 2);
					if (trinagles.Count < ((vertex_tr.Count - 2) * 3))
						trinagles.Add (i - 1);
					j = 1;
				}
			}
		}

		var mesh = new Mesh { vertices = vertex_v.ToArray(), normals = norman_normals.ToArray(), uv = uvs, triangles = trinagles.ToArray() };

		return mesh;

	}
}