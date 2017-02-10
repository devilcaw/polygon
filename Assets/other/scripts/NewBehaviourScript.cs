using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	public Transform[] vertex_tr;

	public Mesh Quad()
	{
		//var normal = Vector3.Cross().normalized;
		Vector2[] uvs = new Vector2[vertex_tr.Length];
		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertex_tr[i].position.x, vertex_tr[i].position.z);
		}
		var mesh = new Mesh {
			vertices = new[] {
				vertex_tr [0].localPosition,
				vertex_tr [1].localPosition,
				vertex_tr [2].localPosition,
				vertex_tr [3].localPosition,
				vertex_tr [4].localPosition
			},
			normals = new[] {
				vertex_tr [0].localPosition,
				vertex_tr [1].localPosition,
				vertex_tr [2].localPosition,
				vertex_tr [3].localPosition,
				vertex_tr [4].localPosition
			},
			//uv = uvs,
			uv = new[] { new Vector2 (0, 0), new Vector2 (1, 0), new Vector2 (0, 1), new Vector2 (1, 1), new Vector2(0,0) },
			triangles = new[] { 0, 2, 1, 2, 3, 1, 3, 4, 1 }
		};
		return mesh;
	}
	void Update() {
		GetComponent<MeshFilter> ().mesh.Clear ();
		GetComponent<MeshFilter> ().mesh.vertices = Quad ().vertices;
		GetComponent<MeshFilter> ().mesh.triangles = Quad ().triangles;
		GetComponent<MeshFilter> ().mesh.uv = Quad ().uv;
		GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
	}
}