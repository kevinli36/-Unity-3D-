using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPatrol : MonoBehaviour {
	public GameObject patrol;
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "patrol") {
			patrol = other.gameObject;
		}
	}
}
