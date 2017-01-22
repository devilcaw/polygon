using UnityEngine;
using System.Collections;

public class Ammo_system : MonoBehaviour {
	public GameObject box_ammo;
	public Weapon_system weapon;
	public int ammo_count;
	void Start () {
	
	}
	

	void Update () {
		
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player") {
			if (Input.GetKeyDown(KeyCode.E)) {
				if (weapon.ammo_count + ammo_count <= weapon.ammo_max) {
					weapon.ammo_count += ammo_count;
					Destroy (box_ammo);
				} else {
					ammo_count -= weapon.ammo_max - weapon.ammo_count;
					weapon.ammo_count += weapon.ammo_max - weapon.ammo_count;

				}
			}
		}
	}
}
