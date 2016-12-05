using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CollisionSounds : MonoBehaviour {
	private AudioSource audioSource;
	[SerializeField]
	private List<AudioClip> soundClips = new List<AudioClip> ();
	[SerializeField]
	private List<AudioClip> grabberClips = new List<AudioClip>();
	[SerializeField]
	private float timerDelay;
	private float timer = 0f;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		timer += Time.deltaTime;
	}

	// Update is called once per frame
	void OnCollisionEnter () {
		if (timer > timerDelay)
		{
			AudioClip randomSound = soundClips[Random.Range(0, soundClips.Count)];
			timerDelay = randomSound.length + timer;
			audioSource.PlayOneShot(randomSound, 0.5f);
		}

	}

	public void PlayGrabSound (AudioSource controllerAudioSource)
	{
		Debug.Log("playing grab sound?");
		AudioClip randomSound = grabberClips[Random.Range(0, grabberClips.Count)];
		timerDelay = randomSound.length + timer;
		controllerAudioSource.PlayOneShot(randomSound, 0.5f);
	}
}