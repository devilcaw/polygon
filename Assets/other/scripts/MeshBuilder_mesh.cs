using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder_mesh : MonoBehaviour {
	public List<Transform> vertex_tr = new List<Transform>();
	public List<int> arr = new List<int>();
	private int j = 1;
	public int[] trinagles = new int[100];

	public Mesh Quad() {
		//var normal = Vector3.Cross().normalized;
		Vector2[] uvs = new Vector2[vertex_tr.Count];
		for (int i = 0; i < uvs.Length; i++) {
			uvs [i] = new Vector2 (vertex_tr [i].localPosition.x, vertex_tr [i].localPosition.z);
		}

		if (arr.Count < vertex_tr.Count)
			for (int i = 2; i < vertex_tr.Count; i++) {
				if (j == 1) {
					arr.Add (i);
					arr.Add (i - 2);
					arr.Add (i - 1);
					j = 2;
				} else {
					arr.Add (i);
					arr.Add (i - 1);
					arr.Add (i - 2);
					j = 1;
				}
			}
				

		var mesh = new Mesh {
			vertices = new[] {
				vertex_tr [0].localPosition,
				vertex_tr [1].localPosition,
				vertex_tr [2].localPosition,
				vertex_tr [3].localPosition,
				vertex_tr [4].localPosition,
				vertex_tr [5].localPosition
			},
			normals = new[] {
				vertex_tr [0].localPosition,
				vertex_tr [1].localPosition,
				vertex_tr [2].localPosition,
				vertex_tr [3].localPosition,
				vertex_tr [4].localPosition,
				vertex_tr [5].localPosition
			},
			uv = uvs,
			//uv = new[] { new Vector2 (0, 0), new Vector2 (1, 0), new Vector2 (0, 1), new Vector2 (1, 1), new Vector2(0,0), new Vector2(0, 1) },
			/*triangles = new[] { 0, 1, 2,
				1, 3, 2, 
				2, 3, 4,
				3, 5, 4
			}*/
			triangles = trinagles
		};
		return mesh;

	}
	void Update() {
		GetComponent<MeshFilter> ().mesh.Clear ();
		GetComponent<MeshFilter> ().mesh.vertices = Quad ().vertices;
		GetComponent<MeshFilter> ().mesh.triangles = Quad ().triangles;
		GetComponent<MeshFilter> ().mesh.uv = Quad ().uv;
		GetComponent<MeshFilter> ().mesh.RecalculateNormals ();

		arr.CopyTo (trinagles);
	}
}