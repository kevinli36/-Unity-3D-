using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerControl : MonoBehaviour {
	private ParticleSystem particle;
	private ParticleSystem.Particle[] particleArr;
	private CirclePosition[] circle;

	public float minRadius = 2f;
	public float maxRadius = 3f;
	public bool clockwise = false;
	public int count = 3000;
	public float size = 0.1f;
	public float speed = 0f;
	public float range = 0.02f;

	// Use this for initialization
	void Start () {
		particleArr = new ParticleSystem.Particle[count];
		circle = new CirclePosition[count];
		particle = this.GetComponent<ParticleSystem> ();
		var main = particle.main;
		main.maxParticles = count;
		main.startSpeed = speed;
		main.startSize = size;
		main.loop = false;
		particle.Emit (count);
		particle.GetParticles (particleArr);
		SetPosition ();
	}

	void SetPosition()
	{
		float midRadius = (maxRadius + minRadius) / 2;
		for (int i = 0; i < count; i++) {
			float minRate = Random.Range (1f, midRadius / minRadius);
			float maxRate = Random.Range (midRadius / maxRadius, 1f);
			float radius = Random.Range (minRate * minRadius, maxRate * maxRadius);

			float minAngle = Random.Range (0f, 90f);
			float maxAngle = Random.Range (90f, 180f);
			float angle = Random.Range (minAngle, maxAngle);
			if (Random.Range (0f, 1f) > 0.5)
				angle += 180f;
			float alpha = angle / 180 * Mathf.PI;

			circle [i] = new CirclePosition (radius, angle);
			particleArr [i].position = new Vector3 (circle[i].radius * Mathf.Cos(alpha), 0f, circle[i].radius * Mathf.Sin(alpha));
		}
		particle.SetParticles (particleArr, particleArr.Length);
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < count; i++) {
			if (clockwise)
				circle [i].angle -= Random.Range(0, 1f) / 2;
			else
				circle [i].angle += Random.Range(0, 1f) / 2;

			circle [i].angle = (360f + circle [i].angle) % 360f;
			float alpha = circle [i].angle / 180 * Mathf.PI;

//			var change = Mathf.PingPong (Time.deltaTime * 0.5f, range);
//			circle [i].radius += change - 0.01f;

			particleArr [i].position = new Vector3 (circle[i].radius * Mathf.Cos(alpha), 0f, circle[i].radius * Mathf.Sin(alpha));
		}
		particle.SetParticles (particleArr, particleArr.Length);
	}
}