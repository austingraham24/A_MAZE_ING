using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {
	public string color;
	private Vector3 spawnPosition;

	// Use this for initialization
	void Start () {
		spawnPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reset() {
		gameObject.transform.position = spawnPosition;
	}
}
