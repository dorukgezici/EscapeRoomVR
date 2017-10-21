using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour {
	private bool locked;
	// Use this for initialization
	void Start () {
		locked = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (locked == true && other.tag == "Key 1") {
			Vector3 move = new Vector3 (0, -90, 0);
			transform.Rotate(move);
			locked = false;
		}
	
	}
}
