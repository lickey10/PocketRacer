/*
	This script is placed in public domain. The author takes no responsibility for any possible harm.
	Contributed by Jonathan Czeck
*/
using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour
{
	public Transform target;
	public int zigs = 100;
	public float speed = 1f;
	public float scale = 1f;
	public Light endLight;
	[HideInInspector] public bool emits = false;
	
	Perlin noise;
	float oneOverZigs;
	
	//private Particle[] particles;
	
	void Start()
	{
		oneOverZigs = 1f / (float)zigs;
		//particleEmitter.emit = false;

		//particleEmitter.Emit(zigs);
		//particles = particleEmitter.particles;
	}
	
	void Update ()
	{
		if(target == null)
			return;
		if(!emits){
			return;
		}
		endLight.intensity = 1;
		if (noise == null)
			noise = new Perlin();
			
		float timex = Time.time * speed * 0.1365143f;
		float timey = Time.time * speed * 1.21688f;
		float timez = Time.time * speed * 2.5564f;
		
		//for (int i=0; i < particles.Length; i++)
		//{
		//	Vector3 position = Vector3.Lerp(transform.position, target.position, oneOverZigs * (float)i);
		//	Vector3 offset = new Vector3(noise.Noise(timex + position.x, timex + position.y, timex + position.z),
		//								noise.Noise(timey + position.x, timey + position.y, timey + position.z),
		//								noise.Noise(timez + position.x, timez + position.y, timez + position.z));
		//	position += (offset * scale * ((float)i * oneOverZigs));
			
		//	particles[i].position = position;
		//	particles[i].color = Color.white;
		//	particles[i].energy = 1f;
		//}
		
		//particleEmitter.particles = particles;
		
		//if (particleEmitter.particleCount >= 2)
		//{
		//	if (endLight)
		//		endLight.transform.position = particles[particles.Length - 1].position;
		//}
	}	
	
	void EmitCharge (bool s) {
		emits = s;
		endLight.intensity = 0;
	}
	
	/* C
	 */
	void Target (Transform t) {
		target = t;
	}
}