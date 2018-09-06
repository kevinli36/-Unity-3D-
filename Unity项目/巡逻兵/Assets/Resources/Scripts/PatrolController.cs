using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour {

	private float x, z;
	public float speed = 0.5f;
	public int state = 0;
	public GameObject player;
	int turn = 0;
	private bool flag = true;
	private float dis = 0;

	public SceneController currentsceneController;
	// Use this for initialization
	void Start () {
		currentsceneController = Director.get_instance ().current_scene;
		player = currentsceneController.player;
		x = this.transform.position.x;
		z = this.transform.position.z;
	}

	void FixedUpdate()
	{
		if (state == 0) {
			patrol ();
		} else if (state == 1) {
			chase (player);
		}
	}

	void patrol()
	{
		if (flag) {
			switch (turn) {
			case 0:
				x++;
				z--;
				break;
			case 1:
				x = x + 2;
				break;
			case 2:
				x++;
				z++;
				break;
			case 3:
				x--;
				z++;
				break;
			case 4:
				x = x - 2;
				break;
			case 5:
				x--;
				z--;
				break;
			}
			flag = false;
		}
		dis = Vector3.Distance (this.transform.position, new Vector3 (x, 1f, z));
		if (dis > 0.7) {
			transform.position = Vector3.MoveTowards (this.transform.position, new Vector3 (x, 1f, z), speed * Time.deltaTime);
		} else {
			turn = (turn + 1) % 6;
			flag = true;
		}
	}

	void chase(GameObject other)
	{
		transform.position = Vector3.MoveTowards (this.transform.position, player.transform.position, 1.5f * speed * Time.deltaTime);
	}
		
}
