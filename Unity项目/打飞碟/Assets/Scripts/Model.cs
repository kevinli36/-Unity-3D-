using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class Model : MonoBehaviour{

	private bool shooting = false;
	private bool isphy = true;

	public bool isShooting(){
		return shooting;
	}

	private List<GameObject> disks = new List<GameObject> ();

	private Color disk_color;
	private Vector3 disk_position;
	private Vector3 disk_direction;
	private float disk_speed;
	private int disk_number;
	private bool disk_enable;

	private SceneController scene;

	void Awake(){
		scene = SceneController.get_instance ();
		scene.setModel (this);
	}

	public void setting(Color color, Vector3 position, Vector3 direction, float speed, int num){
		disk_color = color;
		disk_position = position;
		disk_direction = direction;
		disk_speed = speed;
		disk_number = num;
	}

	public void prepareemit(bool isPhy){
		isphy = isPhy;
		if (!shooting) {
			disk_enable = true;
		}
	}

	void emit(){
		for (int i = 0; i < disk_number; i++) {
			disks.Add (DiskFactory.get_instance ().get_disk ());
			disks [i].GetComponent<Renderer> ().material.color = disk_color;
			disks [i].transform.position = disk_position;
			disks [i].SetActive (true);
			disks [i].GetComponent<Rigidbody> ().AddForce (disk_direction * Random.Range (0.5f, 1f) * disk_speed, ForceMode.Impulse);
		}
	}
		
	void freedisk(int id){
		DiskFactory.get_instance ().free_disk (id);
		disks.RemoveAt (id);
	}

	void FixedUpdate(){
		if (disk_enable && isphy) {
			emit ();
			disk_enable = false;
			shooting = true;
		} 
		else if (!isphy) {
			if (disk_enable) {
				for (int i = 0; i < disk_number; i++) {
					disks.Add (DiskFactory.get_instance ().get_disk ());
					disks [i].GetComponent<Renderer> ().material.color = disk_color;
					disks [i].transform.position = disk_position;
					disks [i].SetActive (true);
					disks [i].GetComponent<Rigidbody> ().useGravity = false;
					disks [i].GetComponent<Rigidbody> ().velocity = Vector3.zero;
				}
				disk_enable = false;
				shooting = true;
			}
			else if (disks.Count != 0) {
				for (int i = 0; i < disk_number; i++) {
					Vector3 movedirectionx = disks [i].transform.right;
					Vector3 movedirectiony = disks [i].transform.up;
					Vector3 movedirectionz = disks [i].transform.forward;
					disks [i].transform.position += (movedirectionx * 5 * Time.fixedDeltaTime) + (movedirectiony * (-20) * Time.fixedDeltaTime * Time.fixedDeltaTime + movedirectiony * 5 * Time.fixedDeltaTime) + (movedirectionz * 5 * Time.fixedDeltaTime);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < disks.Count; i++) {
			if (!disks [i].activeInHierarchy) {
				scene.getJudge ().scoreAdd ();
				freedisk (i);
			} else if (disks [i].transform.position.y < 0) {
				scene.getJudge ().scoreSub ();
				freedisk (i);
			}
			if (disks.Count == 0)
				shooting = false;
		}
	}
}
