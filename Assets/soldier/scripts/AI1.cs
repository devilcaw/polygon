using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class AI1 : MonoBehaviour {
	public GameObject player;
	public Transform look_track;
	public Transform player_look_track;
	[Header("Look distantion")]
	public float look_dist;
	public float look_ugolV;
	[Header("Stels")]
	public NavMeshAgent na;
	public bool spalil;

	void Start () {
		na = GetComponent<NavMeshAgent> ();
	}
	

	void Update () {
		if (spalil == true) {
			na.SetDestination (player.transform.position);
		}

		if ((spalil == true) & (Vector3.Distance (transform.position, player.transform.position) <= 2.5f)) {
			player.GetComponent<Character_control>().enabled = false;
		}
		Debug.DrawRay (look_track.position + Vector3.up, player_look_track.transform.position - look_track.position - Vector3.up, Color.red);
		RaycastHit hit;
		Ray rays = new Ray (look_track.position + Vector3.up, player_look_track.transform.position - look_track.position - Vector3.up);
		Quaternion look = Quaternion.LookRotation (player_look_track.transform.position - transform.position);
		float ugol = Quaternion.Angle (transform.rotation, look);

		if (ugol < look_ugolV) {
			if (Physics.Raycast (rays, out hit, look_dist)) {
				//Debug.Log (hit.transform.gameObject.name);
				if (hit.transform.gameObject.tag == "Player") {
					Debug.Log (2);
					spalil = true;
					Vector3 rot = transform.eulerAngles;
					look_track.LookAt(player_look_track.transform.position);
					float bodyY = Mathf.LerpAngle(rot.y, transform.eulerAngles.y, Time.deltaTime * 5);
					transform.eulerAngles = new Vector3(rot.x, bodyY, rot.z);

				}
			}
		}
	}
}