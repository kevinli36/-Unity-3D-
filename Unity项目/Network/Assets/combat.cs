using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class combat : NetworkBehaviour {
	public const int maxHealth = 100;
	public bool isplayer;
	[SyncVar]
	public bool gamestate = true;
	[SyncVar]
	public int health = maxHealth;
	EnemySpawner enemyspawner;
	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;
		health -= amount;
		if (health <= 0) {
			if (isplayer) {
				gamestate = false;
			}
			Debug.Log (gamestate);
			Destroy (gameObject);
		}
	}

	void OnGUI()
	{
		if (gamestate) {
			if (GUI.RepeatButton (new Rect (100, 200, 60, 60), "Rule")) {				
				GUI.TextArea (new Rect (200, 200, 100, 80), "键盘移动，空格射击");
			}
		} else {
			Debug.Log ("over");
			GUI.Label (new Rect (500, 500, 60, 60), "游戏结束");
		}
	}
}
