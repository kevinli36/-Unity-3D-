using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public GameObject origin;
	public float speed;
	float y,z;

	// Use this for initialization
	void Start () {
		y = Random.Range (1, 360);
		z = Random.Range (1, 360);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 axis = new Vector3 (0, y, z);
		this.transform.RotateAround (origin.transform.position, axis, speed * Time.deltaTime);
	}
}
