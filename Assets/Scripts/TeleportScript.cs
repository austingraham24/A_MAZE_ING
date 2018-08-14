using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {
	public Transform destination;
	public AudioClip teleport;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision) {
		var collideObject = collision.gameObject;
		if (collideObject.layer == 12 || collideObject.layer == 13) {
			collideObject.transform.position = destination.position;
			collideObject.transform.rotation = destination.rotation;
			collideObject.GetComponent<MovementScript>().source.PlayOneShot(teleport, 1f);
		}
	}
}
