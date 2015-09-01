using UnityEngine;
using System.Collections;

public class Ouch : MonoBehaviour {
	public int damage = 3;
	public Transform spawner;
	public bool destroyOnTouch = false;
	public float knockback = 5f;
	public float force = 1f;

	private Collider2D collided;
	private Vector2 oldPosition;
	private Vector2 newPosition;
	private float animationTime;
	private float transitionDuration = 1f;

	private bool animating = false;
	// Use this for initialization
	void Start () {
		BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D> ();
		if (collider == null) {
			collider = this.gameObject.AddComponent<BoxCollider2D> ();
			collider.isTrigger = true;
		}
	}

	void Update () {
		if (this.animating) {
			Knockback();
		}
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		this.collided = collider;
		if (!collider.isTrigger && collider.transform != this.spawner) {
			Health target = collider.GetComponent<Health> ();
			if (target != null) {
				target.ChangeHealth (-damage);
			}
			
			Vector2 heading = collider.transform.position - this.transform.position;
			
			Rigidbody2D rb2dThis = collider.GetComponent<Rigidbody2D> ();
			Rigidbody2D rb2dCol = collider.GetComponent<Rigidbody2D> (); 
			
			print ("this velocity" + rb2dCol.velocity);
			print ("normalized heading" + heading.normalized);
			this.oldPosition = collider.transform.position;
			this.newPosition = this.oldPosition + heading * this.knockback * this.force; 
			this.animationTime = 0;
			this.animating = true;
			//collider.attachedRigidbody.AddForce (heading.normalized * this.knockback * this.force);
			//rb2dCol.velocity =  heading.normalized * this.knockback * this.force;

			//collider.transform.position = Vector2.SmoothDamp (collider.transform.position, new Vector2(1, 1), heading*this.knockback, 1000);
			//collider.transform.position += (collider.transform.position - this.transform.position).normalized * this.knockback;
			collider.transform.position += (collider.transform.position - this.transform.position).normalized * this.knockback;
			if (this.destroyOnTouch) {
				Destroy (this.gameObject);
			}
		}
	}
	
	void Knockback (){
		this.animationTime += Time.deltaTime;
		if (this.animationTime > this.transitionDuration) {
			this.animationTime = this.transitionDuration;
			this.animating = false;
		}
		
		float completed = this.animationTime / this.transitionDuration;
		this.collided.transform.position = Vector3.Lerp (this.collided.transform.position, this.newPosition, completed);
	}
}
