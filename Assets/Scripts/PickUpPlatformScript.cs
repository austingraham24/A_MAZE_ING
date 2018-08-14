using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpPlatformScript : MonoBehaviour {
	public string color;
	private float speed = 50;
	private bool active = false;
	private GameObject particles;
	private GameObject orb;
	public GameManagerScript gm;
	public Image cheeseImage;

	// Use this for initialization
	void Start () {
		orb = this.gameObject.transform.GetChild(1).gameObject;
		particles = this.gameObject.transform.GetChild(2).gameObject;
		var emission = particles.GetComponent<ParticleSystem>().emission;
		emission.rateOverTime = 0;
		var position = orb.transform.position;
		position.y = position.y - 2;
		orb.transform.position = position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.R)) {
			active = false;
			var emission = particles.GetComponent<ParticleSystem>().emission;
			emission.rateOverTime = 0;
			orb.transform.Translate(new Vector3 (0,-2,0));
		}
		if (active) {
			Vector3 turnAmount = Vector3.forward * Time.deltaTime * speed;
        	particles.transform.Rotate(turnAmount);

        	if (cheeseImage.fillAmount < 1) {
        		cheeseImage.fillAmount = cheeseImage.fillAmount + 1 * Time.deltaTime;
        	}
        	else {
        		cheeseImage.fillAmount = 1;
        	}
		}
	}

	public void toggleActive() {
		active = !active;
		if(active) {
			var emission = particles.GetComponent<ParticleSystem>().emission;
			emission.rateOverTime = 10;
			orb.transform.Translate(new Vector3 (0,2,0));
			gm.deliverItem();
		}
	}
}
