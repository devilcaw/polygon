using UnityEngine;
using System.Collections;

public class door_system : MonoBehaviour {
	Quaternion Q;
	public Animator animator;
	public key key;
	public int key_number;
	public bool ex;
	public bool door_lock;
	public bool col_forward;
	public bool col_back;
	public bool on_trig;
	public bool revert;

	void Start () {
		Q = transform.rotation;

	}
	

	void Update () {
		if (ex == true) {
			if (transform.rotation != Q) {
				transform.rotation = Quaternion.Slerp(transform.rotation, Q, 2f * Time.deltaTime);

			}
		}
		if (on_trig == true) {
			if (((Input.GetKeyDown (KeyCode.E)) & (key.key_num [key_number] == true) & (door_lock == true))) {
				door_lock = false;
				animator.SetBool ("grab", true);
			} else if (Input.GetKeyDown (KeyCode.E) & (transform.rotation == Q) & (door_lock == false)) {
				if (col_forward == true)
					transform.eulerAngles = new Vector3 (0f, Mathf.Round(transform.eulerAngles.y + 89f), 0f);
				if (col_back == true)
					transform.eulerAngles = new Vector3 (0f, Mathf.Round(transform.eulerAngles.y - 89f), 0f);
			}
		}
	}
}