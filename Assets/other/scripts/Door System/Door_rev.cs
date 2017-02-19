using UnityEngine;
using System.Collections;

public class Door_rev : MonoBehaviour {
	Quaternion i;
	bool ex = false;
	public GameObject petel;
	public Collider player;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody>();
		i = petel.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (ex == true) {
			if (petel.transform.rotation != i) {
				if (petel.transform.rotation.y > i.y) {
					petel.transform.Rotate (new Vector3 (0, -1, 0) * 25 * Time.deltaTime);
				} else if (petel.transform.rotation.y < i.y) {
					petel.transform.Rotate (new Vector3 (0, 1, 0) * 25 * Time.deltaTime);
				}
			} else
				ex = false;
		}



		/*if (ex == true) {
			i = i + Time.deltaTime * 1;
			Debug.Log (i);
		} else {
			i = 0;
		}

		Debug.Log (ex);*/
	}

	void OnTriggerExit(Collider col) {
		if (col == player) {
			ex = true;
		}

	}
}
