using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory_Base : System.Object {

	private static ArrowFactory_Base instance;
	private static List<GameObject> arrows;
	public GameObject arrow_template;

	public static ArrowFactory_Base get_instance()
	{
		if (instance == null) {
			instance = new ArrowFactory_Base ();
			arrows = new List<GameObject> ();
		}
		return instance;
	}

	public GameObject get_arrow(){
		for (int i = 0; i < arrows.Count; i++) {
			if (!arrows [i].activeInHierarchy) {
				arrows [i].SetActive (true);
				arrows [i].transform.position = new Vector3 (0, 10, -10);
				if (arrows [i].GetComponent<Rigidbody>() == null)  
					arrows [i].AddComponent<Rigidbody>();  
				Component[] comp = arrows [i].GetComponentsInChildren<CapsuleCollider>();  
				foreach (CapsuleCollider one in comp) {  
					one.enabled = true;  
				}  
				arrows [i].GetComponent<CapsuleCollider> ().isTrigger = true;
				return arrows [i];
			}
		}
		GameObject temp = GameObject.Instantiate (arrow_template) as GameObject;
		temp.SetActive (true);
		temp.GetComponent<CapsuleCollider> ().isTrigger = true;
		arrows.Add (temp);
		return arrows[arrows.Count - 1];
	}

	public void judgearrow(){
		for (int i = 0; i < arrows.Count; i++) {
			if (arrows [i].transform.position.y < 0) {
				arrows [i].SetActive (false);
				arrows [i].GetComponent<Rigidbody> ().velocity = Vector3.zero;
				arrows [i].transform.position = new Vector3 (0, 10, -10);
			}
		}
	}

	public void AddOutArrow(GameObject outarrow)
	{
		arrows.Add (outarrow);
	}

	public void freeall()
	{
/*		for (int i = 0; i < arrows.Count; i++) {
			if (arrows [i].activeInHierarchy)
				arrows [i].SetActive (false);
		}
		*/
		arrows.Clear ();
//		GameObject[] Arrow = GameObject.FindGameObjectWithTag ("arrow");

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}

public class ArrowFactory : MonoBehaviour{
	public GameObject arrow_template;

	void Awake(){
		ArrowFactory_Base.get_instance ().arrow_template = arrow_template;
	}
}