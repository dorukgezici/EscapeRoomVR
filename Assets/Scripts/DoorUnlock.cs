using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour {
	public GameObject door;
	public Animator anim;
	private bool locked;
	// Use this for initialization
	void Start () {
		locked = true;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

			
		
	}

	void OnTriggerEnter (Collider other){
		if (locked==true && other.tag == "Key 1") {
			//door.GetComponent<Animation>().Play("Door_Close"); //Should play the animation
			anim.Play("Door_Close");
			Debug.Log("Now the animation clip should play");
			locked = false;
		}
	}
}
