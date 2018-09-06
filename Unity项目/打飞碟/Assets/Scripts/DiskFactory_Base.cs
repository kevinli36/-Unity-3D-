using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

namespace mygame{
	public class DiskFactory : System.Object {
		
		private static DiskFactory instance;
		private static List<GameObject> disks;
		public GameObject disk_template;

		public static DiskFactory get_instance(){
			if (instance == null){
				instance = new DiskFactory ();
				disks = new List<GameObject> ();
			}
			return instance;
		}
	
		public GameObject get_disk(){
			for (int i = 0; i < disks.Count; i++) {
				if (!disks [i].activeInHierarchy)
					return disks[i];
			}
			disks.Add (GameObject.Instantiate (disk_template) as GameObject);
			return disks [disks.Count - 1];
		}

		public void free_disk(int id)
		{
			if (id >= 0 && id <= disks.Count - 1) {
				disks [id].GetComponent<Rigidbody>().velocity = Vector3.zero;
				disks [id].SetActive (false);
			}
		}
	}

	public class DiskFactory_Base : MonoBehaviour{
		public GameObject disk_template;

		void Awake(){
			DiskFactory.get_instance ().disk_template = disk_template;
		}
	}
}
