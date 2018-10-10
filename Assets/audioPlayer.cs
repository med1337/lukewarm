using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayer : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] clips; 
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update () {
         
	}
    public void PlaySound(int soundNum)
    {
        audioSource.clip = clips[soundNum];
        audioSource.Play();
    }
}
