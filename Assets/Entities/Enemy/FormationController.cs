using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float padding = 1f;

	private int direction = 1;
	private float formationRightEdge;
	private float formationLeftEdge;
	private float boundaryRightEdge;
	private float boundaryLeftEdge;
			

	// Use this for initialization
	void Start () {	
	
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
		boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1,0,distance)).x - padding;		
			
		foreach(Transform child in transform) {
			// Perhaps child.transform.position
			GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void OnDrawGizmos() {
		float xmin = transform.position.x - 0.5f * width;
		float xmax = transform.position.x + 0.5f * width;
		float ymin = transform.position.y - 0.5f * height;
		float ymax = transform.position.y + 0.5f * height;	
		Gizmos.DrawLine( new Vector3(xmin,ymin,0), new Vector3(xmin,ymax,0) );
		Gizmos.DrawLine( new Vector3(xmin,ymax,0), new Vector3(xmax,ymax,0) );
		Gizmos.DrawLine( new Vector3(xmax,ymax,0), new Vector3(xmax,ymin,0) );
		Gizmos.DrawLine( new Vector3(xmax,ymin,0), new Vector3(xmin,ymin,0) );
	}
	
	// Update is called once per frame
	void Update () {
		float formationRightEdge = transform.position.x + 0.5f * width;
		float formationLeftEdge  = transform.position.x - 0.5f * width;
	
		if (formationRightEdge > boundaryRightEdge) {
			direction = -1;
		}
		
		if(formationLeftEdge < boundaryLeftEdge) {
			direction = 1;
		}
		transform.position += new Vector3(direction * speed * Time.deltaTime,0,0);
	}
}
