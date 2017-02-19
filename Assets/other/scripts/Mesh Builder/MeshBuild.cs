using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuild : MonoBehaviour {


	public static Mesh Quad(Vector3 origin, Vector3 width, Vector3 length)
	{
		var normal = Vector3.Cross(length, width).normalized;
		var mesh = new Mesh
		{
			vertices = new[] { origin, origin + length, origin + length + width, origin + width },
			normals = new[] { normal, normal, normal, normal },
			uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) },
			triangles = new[] { 0, 1, 2, 0, 2, 3}
		};
		return mesh;
	}
	void Start() {
		GetComponent<MeshFilter> ().mesh.Clear ();
		GetComponent<MeshFilter> ().mesh.vertices = Quad (new Vector3(0,0,0), new Vector3(1,0,0), new Vector3(0,1,0)).vertices;
		GetComponent<MeshFilter> ().mesh.triangles = Quad (new Vector3(0,0,0), new Vector3(1,0,0), new Vector3(0,1,0)).triangles;
		GetComponent<MeshFilter> ().mesh.uv = Quad (new Vector3(0,0,0), new Vector3(1,0,0), new Vector3(0,1,0)).uv;
		GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
	}
}
