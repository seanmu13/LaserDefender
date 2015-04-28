using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float health = 150f;
	public GameObject laser;
	public float projectileSpeed = 10;
	public float shotsPerSecond = 0.5f;
	
	void Update() {
		float probability = shotsPerSecond * Time.deltaTime;
		
		if( Random.value < probability ) {
			Fire ();
		}
	}

	void Fire () {
		Vector3 startPosition = transform.position + new Vector3 (0, -1, 0);
		GameObject beam = Instantiate (laser, startPosition, Quaternion.identity) as GameObject;
		//beam.rigidbody2D.velocity = new Vector3 (0, projectileSpeed,0);
		beam.rigidbody2D.velocity = new Vector2 (0, -projectileSpeed);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		
		if(missile) {
			health -= missile.GetDamage();
			missile.Hit();
			
			if (health <= 0 ) {
				Destroy(gameObject);
			}
			
			Debug.Log("Hit by a projectile");
		}
	}
}
