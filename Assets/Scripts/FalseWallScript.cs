using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseWallScript : MonoBehaviour {
	private Vector3 initialPosition;
	private Vector3 secondPosition;
	private bool moving;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		secondPosition = initialPosition;
		secondPosition.y = -3;
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			transform.Translate(Vector3.up * -1 * Time.deltaTime);
			if (transform.position.y < secondPosition.y) {
				moving = false;
				transform.position = secondPosition;
			}
		}
	}

	public void move () {
		moving = true;
	}

	public void reset () {
		transform.position = initialPosition;
	}
}
