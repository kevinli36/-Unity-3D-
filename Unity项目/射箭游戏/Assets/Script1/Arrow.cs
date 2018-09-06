using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	private string cname;
	private SceneController scene;
//	Vector3 old_pos;
//	public float radius = 0.01f;
//	float radian = 0;
//	public float time = 0.8f;

	void OnTriggerEnter(Collider other)
	{
		if(cname == "")
		{
			cname = other.gameObject.name;
			Destroy (this.GetComponent<Rigidbody> ());
			Component[] comp = GetComponentsInChildren<CapsuleCollider> ();
			foreach (CapsuleCollider i in comp)
				i.enabled = false;
		}
		GetComponent<CapsuleCollider> ().isTrigger = false;
		scene.getScoreController().CountScore (cname);
//		tremble ();
	}

/*	void tremble(){
		old_pos = this.transform.position;
		while (time > 0) {
			time -= Time.deltaTime;
			radian += 3f;
			float dy = Mathf.Cos (radian) * radius;
			this.transform.position = old_pos + new Vector3 (0, dy, 0);
		}
		this.transform.position = old_pos;
	}
	*/

	void Awake()
	{
		cname = "";
		scene = SceneController.get_instance ();
	}
}
