using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerMove : NetworkBehaviour {
	public GameObject bulletPrefab;
	[SyncVar]
	public Vector3 pos3;

	void Awake()
	{
		pos3 = gameObject.transform.position;
	}

	public override void OnStartLocalPlayer()
	{
		gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
		GetComponent<MeshRenderer>().material.color = Color.red;
	}

	void Update()
	{
		if (!isLocalPlayer)
			return;

		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}

	[Command]
	void CmdFire()
	{
		// This [Command] code is run on the server!
		if (pos3.z == 3) {
			// create the bullet object locally
			var bullet = (GameObject)Instantiate (
				             bulletPrefab,
				             transform.position - transform.forward,
				             Quaternion.identity);
			bullet.GetComponent<Rigidbody> ().velocity = -transform.forward * 4;
		

			NetworkServer.Spawn (bullet);

			// when the bullet is destroyed on the server it will automaticaly be destroyed on clients
			Destroy (bullet, 2.0f);

		} else {
			var bullet = (GameObject)Instantiate (
				             bulletPrefab,
				             transform.position + transform.forward,
				             Quaternion.identity);
			bullet.GetComponent<Rigidbody> ().velocity = transform.forward * 4;
			NetworkServer.Spawn (bullet);

			// when the bullet is destroyed on the server it will automaticaly be destroyed on clients
			Destroy (bullet, 2.0f);
		}
	}

}
