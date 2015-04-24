using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15f;
	public float padding = 1f;

	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1,0,distance)).x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKey(KeyCode.LeftArrow) ) {		
		
			transform.position += Vector3.left * speed * Time.deltaTime;
			
		} else if( Input.GetKey(KeyCode.RightArrow) ) {	
			
			transform.position += Vector3.right * speed * Time.deltaTime;
		}		
		
		// Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);		
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
