using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject laser;

	public float speed = 15f;
	public float padding = 1f;
	public float projectileSpeed = 10f;
	public float projectileRepeatRate = 0.2f;
	public float health = 250;

	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1,0,distance)).x - padding;
	}

	void Fire () {
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate (laser, transform.position + offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3 (0, projectileSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.0001f, projectileRepeatRate);
		}
		
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	
		if( Input.GetKey(KeyCode.LeftArrow) ) {		
		
			transform.position += Vector3.left * speed * Time.deltaTime;
			
		} else if( Input.GetKey(KeyCode.RightArrow) ) {	
			
			transform.position += Vector3.right * speed * Time.deltaTime;
		}		
		
		// Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);		
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	

	
	void OnTriggerEnter2D(Collider2D collider) {
		
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		
		if(missile) {
			Debug.Log ("Player hit by missile");
			health -= missile.GetDamage();
			missile.Hit();
			
			if (health <= 0 ) {
				Destroy(gameObject);
			}
		}
	}
}
