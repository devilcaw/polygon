using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(MeshBuilder))]
public class MeshBuilder_gui : Editor {
	public bool bl;
	public bool sbl;
	public GameObject point_mesh;
	public GameObject go;
	public float c;


	void OnEnable() {
		var meshbuilder = target  as MeshBuilder;
	
		go = Resources.Load<GameObject> ("Prefabs/MeshBuilder/mesh_empty");
		point_mesh = Resources.Load<GameObject>("Prefabs/MeshBuilder/point_mesh");
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		var meshbuilder = target  as MeshBuilder;


		if (GUILayout.Button ("Create new mesh")) {

			GameObject obj = Instantiate<GameObject> (go);

			obj.transform.position = meshbuilder.transform.position;
			obj.transform.SetParent(meshbuilder.transform);
			obj.AddComponent<MeshBuilder_mesh> ();
		}

		if (GUILayout.Button ("but")) { 
			if (sbl == false) {
				sbl = true;
				EditorApplication.update = Update;
			} else if (sbl == true) {
				sbl = false;
				EditorApplication.update = null;
			}
		}
	}
		

	static void Update () {
		Debug.Log (1);
	}
}