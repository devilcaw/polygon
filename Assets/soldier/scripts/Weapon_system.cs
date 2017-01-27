using UnityEngine;
using System.Collections;

public class Weapon_system : MonoBehaviour {
	private GameObject player;
	public Character_control Ccont;
	public Animator animator;
	public GameObject decal;
	public Shoot shoot;
	public GameObject weapon;
	public string anim_p_shoot;
	public KeyCode key_bind;
	[Header("Ammo")]
	public int ammo_max;
	public int ammo_count;
	[Header("Oboyma")]
	public GameObject oboyma;
	public int oboyma_max;
	public int oboyma_count;
	public GameObject gilza;
	public Transform gilza_t;
	[Header("Time to shoot")]
	public float time_to_shoot;
	public float wait_to_shoot;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	

	void Update () {
		
		if (Input.GetKeyDown (key_bind)) {
			if (weapon.activeSelf == false) {
				weapon.SetActive (true);
				shoot.have_weapon = true;
			} else {
				weapon.SetActive (false);
				shoot.have_weapon = false;
			}
		}

		if (weapon.activeSelf == true) {
			
			if (oboyma_count > 0)
				animator.SetBool ("no_ammo", false);
			else
				animator.SetBool ("no_ammo", true);

			if (time_to_shoot > 0)
				time_to_shoot -= Time.deltaTime;
			if ((Input.GetMouseButtonDown (0)) & (Ccont.celim == true) & (weapon.activeSelf == true) & (oboyma_count > 0) & (time_to_shoot <= 0)) {
				time_to_shoot = wait_to_shoot;

				GameObject go = Instantiate (gilza, gilza_t) as GameObject;
				go.transform.position = gilza_t.position;
				go.transform.rotation = gilza_t.rotation;
				go.transform.parent = null;
				go.AddComponent<Rigidbody> ().AddForce (go.transform.right, ForceMode.Impulse);
				Physics.IgnoreCollision (player.GetComponent<Collider> (), go.GetComponent<Collider> ());

				RaycastHit hit;
				Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
				if (Physics.Raycast (ray, out hit, 100, 1)) {
					GameObject g = Instantiate<GameObject> (decal);
					g.transform.position = hit.point + hit.normal * 0.01f;
					g.transform.rotation = Quaternion.LookRotation (-hit.normal);
					g.transform.SetParent (hit.transform);
					if (hit.rigidbody) {
						hit.rigidbody.AddForceAtPosition (Camera.main.transform.forward, hit.point, ForceMode.Impulse);
					}
				}
				oboyma_count = oboyma_count - 1;
				shoot.shoot = true;
				animator.SetBool ("shoot", true);
				Ccont.animator.SetBool (anim_p_shoot, true);
			} else {
				shoot.shoot = false;
				animator.SetBool ("shoot", false);
				Ccont.animator.SetBool (anim_p_shoot, false);
			}
			if (Input.GetKeyDown (KeyCode.R) & (oboyma_count < oboyma_max)) {
				if (ammo_count > 0) {
					GameObject goo = Instantiate (oboyma, oboyma.transform) as GameObject;
					goo.transform.position = oboyma.transform.position;
					goo.transform.rotation = oboyma.transform.rotation;
					goo.transform.parent = null;
					goo.AddComponent<BoxCollider> ();
					goo.AddComponent<Rigidbody> ().AddForce (-goo.transform.up, ForceMode.Impulse);
					Physics.IgnoreCollision (player.GetComponent<Collider> (), goo.GetComponent<Collider> ());
				}
				if ((ammo_count - (oboyma_max - oboyma_count)) >= 0) {
					ammo_count = (ammo_count - (oboyma_max - oboyma_count));
					oboyma_count = oboyma_max;
				} else {
					oboyma_count = ammo_count % oboyma_max;
					ammo_count = 0;
				}
			}
		}
	}
}