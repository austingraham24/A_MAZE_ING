using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
	public float speed = 5.0f;

	private Rigidbody rb;
	private float torque = 100.0f;
	private string role = null;
    private string HorizontalInputAxis;
    private string VerticalInputAxis;
    public AudioSource source;

    void Start ()
    {
        source = GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        if (GetComponent<CatScript>()) {
        	role = "cat";
            HorizontalInputAxis = "Cat-Horizontal";
            VerticalInputAxis = "Cat-Vertical";
        }
        else {
        	role = "mouse";
            HorizontalInputAxis = "Mouse-Horizontal";
            VerticalInputAxis = "Mouse-Vertical";
        }
        print(role);
    }
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis (HorizontalInputAxis);
        float moveVertical = Input.GetAxis (VerticalInputAxis);

        float deltaTime = Time.deltaTime;

        Vector3 movement = new Vector3 (0.0f, 0.0f, moveVertical);
        //rb.AddRelativeForce (movement * speed);
        transform.Translate(Vector3.forward * deltaTime * moveVertical * speed);

        Vector3 turnAmount = Vector3.up * deltaTime * torque * moveHorizontal;
        transform.Rotate(turnAmount);
        //rb.AddRelativeTorque(transform.up * torque * turn);
    }

    void OnCollisionEnter(Collision collision) {
    	// if (collision.gameObject.layer == 8) {
    	// 	print("Ignore");
    	// 	Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
    	// }
    }

}
