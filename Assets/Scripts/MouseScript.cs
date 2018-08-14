using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseScript : MonoBehaviour {
	private Camera camera;
	public GameManagerScript gmScript;
	private string inventory;
	private ParticleSystem particleSystem;
	public int lives;
	public Transform safeSpawn;
	private GameObject heldItem;
	public Text livesText;
	public AudioSource source;
	public Transform livesDisplay;

	// Use this for initialization
	void Start () {
		camera = this.gameObject.transform.GetChild(0).GetComponent<Camera>();
		camera.cullingMask &=  ~(1 << LayerMask.NameToLayer("Cat-Walls"));
		inventory = null;
		particleSystem = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
		var emission = particleSystem.emission;
		emission.rateOverTime = 0;
    	emission.enabled = false;
    	lives = 3;
    	livesText.text = "Lives: " + lives;
    	source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	void OnCollisionEnter(Collision collision) {
    	print("Mouse collides! " + collision.gameObject.name);
    	var collideObject = collision.gameObject;
    	if (collideObject.layer == gmScript.catLayer) {
    		print("Cat got you!");
    		lives -= 1;
    		livesText.text = "Lives: " + lives;
    		livesDisplay.GetChild(lives).GetComponent<Image>().color = new Color32(99,99,99,200);
    		transform.position = safeSpawn.position;
    		if (inventory != null) {
    			inventory = null;
    			var newPosition = heldItem.transform.position;
				newPosition.y = newPosition.y + 2;
    			heldItem.transform.position = newPosition;
    			heldItem = null;
    			stopParticleEffect();
    		}
    	}
    }

    void OnTriggerEnter(Collider collision) {
    	print("Mouse trigger" + collision.gameObject.name);
    	GameObject collideObject = collision.gameObject;
    	if (collideObject.tag == "Platform" && inventory != null) {
    		if (inventory == collideObject.GetComponent<PickUpPlatformScript>().color) {
    			print("Dropoff Color " + collideObject.GetComponent<PickUpPlatformScript>().color);
    			inventory = null;
    			heldItem = null;

    			collideObject.GetComponent<PickUpPlatformScript>().toggleActive();
    			stopParticleEffect();
    			gameObject.GetComponent<MovementScript>().speed = 5;
    		}
    	}
    	if (collideObject.layer == gmScript.pickupLayer) {
    		print("It's Cheese!");
    		if (inventory == null){
    			print("Pick it up!");
    			source.Play();
    			PickupScript pickupScript = collideObject.GetComponent<PickupScript>();
    			inventory = pickupScript.color;
    			heldItem = collideObject;

    			startParticleEffect(pickupScript.color);
    			gameObject.GetComponent<MovementScript>().speed = 4.8f;

				var newPosition = collideObject.transform.position;
				newPosition.y = newPosition.y - 2;
    			collideObject.transform.position = newPosition;
    		}
    	}
    }

    void startParticleEffect(string color) {
    	var col = particleSystem.colorOverLifetime;
		var emission = particleSystem.emission;
		var colorDict = gmScript.colors;
        //col.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(colorDict[color][0], 0.0f), new GradientColorKey(colorDict[color][1], 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );
		col.color = grad;
		emission.rateOverTime = 50;
		emission.enabled = true;
    }

    void stopParticleEffect() {
		var emission = particleSystem.emission;
		emission.rateOverTime = 0;
		emission.enabled = false;
    }
}
