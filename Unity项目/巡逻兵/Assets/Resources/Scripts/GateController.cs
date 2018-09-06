using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {
	public delegate void AddScore ();
	public static event AddScore addScore;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "player") {
			if (this.transform.parent.GetComponent<CurrentPatrol> ().patrol.GetComponent<PatrolController> ().state == 0)
				this.transform.parent.GetComponent<CurrentPatrol> ().patrol.GetComponent<PatrolController> ().state = 1;
			else {
				this.transform.parent.GetComponent<CurrentPatrol> ().patrol.GetComponent<PatrolController> ().state = 0;
				if (addScore != null)
					addScore ();
			}
		}
	}
}
