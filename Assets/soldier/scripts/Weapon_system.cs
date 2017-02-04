using UnityEngine;
using System.Collections;
using UnityEngine.AI;

internal enum Weapon_type {
	pistol,
	shotgun,
	auto_rifle
}

public class Weapon_system : MonoBehaviour {
	[SerializeField] private Weapon_type weapon_type = Weapon_type.pistol;

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

	private GameObject npc;


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		Ccont = player.GetComponent<Character_control> ();
	}
	

	void Update () {
		
		if (Input.GetKeyDown (key_bind)) {
			weapon.SetActive (!weapon.activeSelf);
			shoot.have_weapon = weapon.activeSelf;
			if ((shoot.weapon_in_hands != null) &(weapon != shoot.weapon_in_hands))
				shoot.weapon_in_hands.SetActive (false);
			shoot.weapon_in_hands = weapon;

		}

		if (weapon.activeSelf == true) {
			
			if (oboyma_count > 0)
				animator.SetBool ("no_ammo", false);
			else
				animator.SetBool ("no_ammo", true);
			
				if (time_to_shoot > 0)
					time_to_shoot -= Time.deltaTime;
			
			if (weapon_type != Weapon_type.auto_rifle) {
				if ((Input.GetMouseButtonDown (0)) & (Ccont.celim == true) & (weapon.activeSelf == true) & (oboyma_count > 0) & (time_to_shoot <= 0)) {
					time_to_shoot = wait_to_shoot;

					GameObject go = Instantiate (gilza, gilza_t) as GameObject;
					go.transform.position = gilza_t.position;
					go.transform.rotation = gilza_t.rotation;
					go.transform.parent = null;
					go.AddComponent<Rigidbody> ().AddForce (go.transform.right, ForceMode.Impulse);
					Physics.IgnoreCollision (player.GetComponent<Collider> (), go.GetComponent<Collider> ());

					switch (weapon_type) {
					case Weapon_type.pistol:
						pistol ();
						break;
					case Weapon_type.shotgun:
						shotgun ();
						break;
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
			} else {
				if ((Input.GetMouseButton (0)) & (Ccont.celim == true) & (weapon.activeSelf == true) & (oboyma_count > 0) & (time_to_shoot <= 0)) {
					time_to_shoot = wait_to_shoot;

					GameObject go = Instantiate (gilza, gilza_t) as GameObject;
					go.transform.position = gilza_t.position;
					go.transform.rotation = gilza_t.rotation;
					go.transform.parent = null;
					go.AddComponent<Rigidbody> ().AddForce (go.transform.right, ForceMode.Impulse);
					Physics.IgnoreCollision (player.GetComponent<Collider> (), go.GetComponent<Collider> ());

					pistol ();

					oboyma_count = oboyma_count - 1;
					shoot.shoot = true;
					animator.SetBool ("shoot", true);
					Ccont.animator.SetBool (anim_p_shoot, true);
				} else {
					shoot.shoot = false;
					animator.SetBool ("shoot", false);
					Ccont.animator.SetBool (anim_p_shoot, false);
				}
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

	void pistol() {
		RaycastHit hit;
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		if (Physics.Raycast (ray, out hit, 100, 1)) {
			GameObject g = Instantiate<GameObject> (decal);
			g.transform.position = hit.point + hit.normal * 0.005f;
			g.transform.rotation = Quaternion.LookRotation (-hit.normal);
			g.transform.SetParent (hit.transform);

			if (hit.transform.tag == "NPC_child")
				npc = hit.transform.gameObject;
			if (npc != null) {
				while (npc.transform.parent != null) {
					npc = npc.transform.parent.gameObject;
				}
				if (npc.tag == "NPC") {
					npc.GetComponent<NPC_system> ().fight.damage_get = true;
					npc.GetComponent<NPC_system> ().fight.health -= 1;
					if (npc.GetComponent<NPC_system> ().fight.health == 0) {
						npc.GetComponent<NPC_system> ().enabled = false;
						npc.GetComponent<NavMeshAgent> ().enabled = false;
						npc.GetComponent<Animator> ().enabled = false;
						for (int i = 0; i < npc.GetComponent<NPC_system> ().Npc_body.ragdoll.Length; i++) {
							npc.GetComponent<NPC_system> ().Npc_body.ragdoll [i].isTrigger = false;
							npc.GetComponent<NPC_system> ().Npc_body.ragdoll [i].gameObject.GetComponent<Rigidbody> ().isKinematic = false;
							npc.GetComponent<CapsuleCollider> ().enabled = false;
						}
					}
				}
			}

			if (hit.rigidbody) {
				hit.rigidbody.AddForceAtPosition (Camera.main.transform.forward * 50, hit.point, ForceMode.Impulse);
			}
		}
	}
	void shotgun ()	{
		RaycastHit hit;
		for (int j = 0; j < 7; j++) {
			Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward + new Vector3 (Random.Range (-0.02f, 0.02f), Random.Range (-0.02f, 0.02f), 0));
			if (Physics.Raycast (ray, out hit, 100, 1)) {
				GameObject g = Instantiate<GameObject> (decal);
				g.transform.position = hit.point + hit.normal * 0.01f;
				g.transform.rotation = Quaternion.LookRotation (-hit.normal);
				g.transform.SetParent (hit.transform);

				if (hit.transform.tag == "NPC_child")
					npc = hit.transform.gameObject;
				if (npc != null) {
					while (npc.transform.parent != null) {
						npc = npc.transform.parent.gameObject;
					}
					if (npc.tag == "NPC") {
						npc.GetComponent<NPC_system> ().fight.damage_get = true;
						npc.GetComponent<NPC_system> ().fight.health -= 1;
						if (npc.GetComponent<NPC_system> ().fight.health == 0) {
							npc.GetComponent<NPC_system> ().enabled = false;
							npc.GetComponent<NavMeshAgent> ().enabled = false;
							npc.GetComponent<Animator> ().enabled = false;
							for (int i = 0; i < npc.GetComponent<NPC_system> ().Npc_body.ragdoll.Length; i++) {
								npc.GetComponent<NPC_system> ().Npc_body.ragdoll [i].isTrigger = false;
								npc.GetComponent<NPC_system> ().Npc_body.ragdoll [i].gameObject.GetComponent<Rigidbody> ().isKinematic = false;
								npc.GetComponent<CapsuleCollider> ().enabled = false;
							}
						}
					}
				}

				if (hit.rigidbody) {
					hit.rigidbody.AddForceAtPosition (Camera.main.transform.forward * 50, hit.point, ForceMode.Impulse);
				}

			}
		}
	}
}