using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public delegate void GameOver ();
	public static event GameOver gameOver_message;

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "patrol") {
			if (gameOver_message != null)
				gameOver_message ();
		}
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
	}

	void Start()
	{
		this.GetComponent<Rigidbody> ().freezeRotation = true;
	}
}
