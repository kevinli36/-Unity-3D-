using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour {
	private ParticleSystem particle;
	private ParticleSystem.Particle[] particleArr;
	private CirclePosition[] circle;

	public float minRadius = 2f;
	public float maxRadius = 4.5f;
	public bool clockwise = true;
	public int count = 1000;
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
			float angle = Random.Range (0f, 360f);
			float alpha = angle / 180 * Mathf.PI;
			circle [i] = new CirclePosition (radius, angle);
			particleArr [i].position = new Vector3 (circle[i].radius * Mathf.Cos(alpha), 0f, circle[i].radius * Mathf.Sin(alpha));
		}
		particle.SetParticles (particleArr, particleArr.Length);
	}

	// Update is called once per frame
	private int tier = 10;
	void Update () {
		for (int i = 0; i < count; i++) {
			if (clockwise)
				circle [i].angle -= (i % tier + 1) / circle [i].radius;
			else
				circle [i].angle += (i % tier + 1) / circle [i].radius;
			
			circle [i].angle = (360f + circle [i].angle) % 360f;
			float alpha = circle [i].angle / 180 * Mathf.PI;

//			var change = Mathf.PingPong (Time.deltaTime * 0.5f, range);
//			circle [i].radius += change - 0.01f;

			particleArr [i].position = new Vector3 (circle[i].radius * Mathf.Cos(alpha), 0f, circle[i].radius * Mathf.Sin(alpha));
		}
		particle.SetParticles (particleArr, particleArr.Length);
	}
}

public class CirclePosition
{
	public float radius = 0f;
	public float angle = 0f;
	public CirclePosition(float radius, float angle)
	{
		this.radius = radius;
		this.angle = angle;
	}
}