using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour {
	private Camera camera;
	public GameManagerScript gmScript;
	public AudioSource source;

	// Use this for initialization
	void Start () {
		camera = this.gameObject.transform.GetChild(0).GetComponent<Camera>();
		camera.cullingMask &=  ~(1 << LayerMask.NameToLayer("Mouse-Walls"));
		camera.cullingMask &=  ~(1 << LayerMask.NameToLayer("PickUp"));

		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}
