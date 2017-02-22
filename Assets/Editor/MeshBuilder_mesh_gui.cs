using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
[CustomEditor(typeof(MeshBuilder_mesh))]
public class MeshBuilder_mesh_gui : Editor {
	public bool bl;
	public bool sbl;
	public GameObject point_mesh;
	public GameObject new_mesh;
	public float c;
	static MeshBuilder_mesh meshbuilder;

	void OnEnable() {
		meshbuilder = target  as MeshBuilder_mesh;

		new_mesh = Resources.Load<GameObject> ("Prefabs/MeshBuilder/mesh_empty");
		point_mesh = Resources.Load<GameObject>("Prefabs/MeshBuilder/point_mesh");
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		var meshbuilder = target  as MeshBuilder_mesh;


		if (GUILayout.Button ("Update")) { 
			if (sbl == false) {
				sbl = true;
				EditorApplication.update = Update;
			} else if (sbl == true) {
				sbl = false;
				EditorApplication.update = null;
			}
		}
	}


	void OnSceneGUI() {

		Event e = Event.current;
		c += Time.deltaTime;

		for (int i = 0; i < meshbuilder.vertex_tr.Count; i++) {
			if (meshbuilder.vertex_tr [i] == null)
				meshbuilder.vertex_tr.RemoveAt (i);
			meshbuilder.vertex_tr [i].name = i.ToString ();
			if (meshbuilder.norman_normals [i] == null)
				meshbuilder.norman_normals.RemoveAt (i);
		}
		if ((e.keyCode == KeyCode.Space) & (c >= 1)) {
			RaycastHit hit;
			Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000, 1))
				CreatePoint (hit.point);
			c = 0;		
		}
	}

	void CreatePoint(Vector3 point_pos) {

		GameObject obj = Instantiate(point_mesh);
		obj.transform.position = point_pos;
		obj.transform.SetParent(meshbuilder.gameObject.transform);

		meshbuilder.vertex_tr.Add (obj.transform);
	}
		

	static void Update () {
		Debug.Log ("Scene Update ON");

		if (meshbuilder.vertex_tr.Count != meshbuilder.vertex_v.Count) {
			for (int i = 0; i < meshbuilder.vertex_tr.Count; i++)
				if (meshbuilder.vertex_tr.Count > meshbuilder.vertex_v.Count)
					meshbuilder.vertex_v.Add (new Vector3 (meshbuilder.vertex_tr [i].localPosition.x, meshbuilder.vertex_tr [i].localPosition.y, meshbuilder.vertex_tr [i].localPosition.z));
		} else
			for (int i = 0; i < meshbuilder.vertex_tr.Count; i++)
				meshbuilder.vertex_v [i] = meshbuilder.vertex_tr [i].localPosition;

		meshbuilder.GetComponent<MeshFilter> ().mesh.Clear ();
		meshbuilder.GetComponent<MeshFilter> ().mesh.vertices = meshbuilder.Quad ().vertices;
		meshbuilder.GetComponent<MeshFilter> ().mesh.triangles = meshbuilder.Quad ().triangles;
		meshbuilder.GetComponent<MeshFilter> ().mesh.uv = meshbuilder.Quad ().uv;
		meshbuilder.GetComponent<MeshFilter> ().mesh.RecalculateNormals ();

		//meshbuilder.vertex_tr.ToArray ().CopyTo (meshbuilder.vertex_v.ToArray(), 0);
	}
}