using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
	public bool active;

	// Use this for initialization
	void Start () {
		active = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			transform.Rotate(Vector3.up * 5 * Time.deltaTime);
		}
	}

	void toggleActive() {
		active = !active;
	}
}
