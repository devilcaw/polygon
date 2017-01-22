using UnityEngine;
using System.Collections;

public class key_trig : MonoBehaviour {
	public key key;
	public int i;
	public Animator animator;
	public bool destroy;
	void Start () {
		
	}
	

	void LateUpdate () {
		if ((destroy == true) & (key.key_num [i] == true))
			Destroy (gameObject);
	}

	void OnTriggerStay(Collider col){
		if ((col.gameObject.tag == "Player") & (Input.GetKeyDown (KeyCode.E))) {
			key.key_num[i] = true;
			animator.SetBool ("grab", true);
		}	
	}
}
