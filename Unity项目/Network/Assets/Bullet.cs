using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	void OnCollisionEnter(Collision collision)
	{
		var hit = collision.gameObject;
		var hitcombat = hit.GetComponent<combat> ();
		if (hitcombat != null) {
			hitcombat.TakeDamage (10);
			Destroy (gameObject);
		}
	}
}
