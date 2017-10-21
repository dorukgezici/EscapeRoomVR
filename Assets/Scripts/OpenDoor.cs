using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
    private bool locked;
	// Use this for initialization
	void Start () {
        locked = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(locked==true && other.tag== "Key 1")
        {
            float speed = 10F;
            transform.Rotate(0, -90, 0); 
            locked = false;
        }
    }
}
