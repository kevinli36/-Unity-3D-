using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	public GameObject player;
	private SceneController currentscene;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		currentscene = Director.get_instance ().current_scene;
		player = currentscene.player;
		offset = player.transform.position - this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = player.transform.position - offset;
	}
}
