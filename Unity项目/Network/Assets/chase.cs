using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour {

	int turn = 0;
	private bool flag = true;
	private float dis = 0;
	public float speed;
	private float x, z;
	
	void FixedUpdate()
	{
			patrol ();
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
		dis = Vector3.Distance (this.transform.position, new Vector3 (x, 0f, z));
		if (dis > 0.7) {
			transform.position = Vector3.MoveTowards (this.transform.position, new Vector3 (x, 0f, z), speed * Time.deltaTime);
		} else {
			turn = (turn + 1) % 6;
			flag = true;
		}
	}
}
