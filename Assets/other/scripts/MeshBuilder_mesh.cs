﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder_mesh : MonoBehaviour {
	
	public List<Transform> vertex_tr = new List<Transform>();
	public List<Vector3> vertex_v = new List<Vector3> ();
	public List<int> trinagles = new List<int>();
	private int j = 1;
	public Vector3[] norman_normals = new Vector3[5];
	//public int[] trinagles = new int[15];


	public Mesh Quad() {

		Vector2[] uvs = new Vector2[vertex_tr.Count];
		for (int i = 0; i < uvs.Length; i++) {
			uvs [i] = new Vector2 (vertex_tr [i].localPosition.x, vertex_tr [i].localPosition.z);
		}
		for (int i = 0; i < (vertex_tr.Count - 2); i++) {
			norman_normals [i] = Vector3.Cross (vertex_v [i + 1] - vertex_v [i], vertex_v [i + 2] - vertex_v [i]).normalized;
		}

	
		if (trinagles.Count < ((vertex_tr.Count - 2) * 3))
			for (int i = 2; i <= vertex_tr.Count - 1; i++) {
				if (j == 1) {
					trinagles.Add (i);
					trinagles.Add (i - 1);
					trinagles.Add (i - 2);
					j = 2;
				} else {
					trinagles.Add (i);
					trinagles.Add (i - 2);
					trinagles.Add (i - 1);
					j = 1;
				}
			}
		//else
			//if (arr.Count > ((vertex_tr.Count - 2) * 3))
			//	arr.Clear();


		var mesh = new Mesh { vertices = vertex_v.ToArray(), normals = norman_normals, uv = uvs, triangles = trinagles.ToArray() };
			//uv = new[] { new Vector2 (0, 0), new Vector2 (1, 0), new Vector2 (0, 1), new Vector2 (1, 1), new Vector2(0,0), new Vector2(0, 1) },
			/*triangles = new[] { 0, 1, 2,
				1, 3, 2, 
				2, 3, 4,
				3, 5, 4
			}*/

		return mesh;

	}
	void Update() {
		/*
		GetComponent<MeshFilter> ().mesh.Clear ();
		GetComponent<MeshFilter> ().mesh.vertices = Quad ().vertices;
		GetComponent<MeshFilter> ().mesh.triangles = Quad ().triangles;
		GetComponent<MeshFilter> ().mesh.uv = Quad ().uv;
		GetComponent<MeshFilter> ().mesh.RecalculateNormals ();

		arr.CopyTo (trinagles); */
	}
}