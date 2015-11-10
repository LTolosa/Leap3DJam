using UnityEngine;
using System.Collections;

public class MusicMode : MonoBehaviour {

    public AudioClip normal;
    public AudioClip hard;
    // Use this for initialization
    void Start () {
        int mode = PlayerPrefs.GetInt("Difficulty");
        AudioSource audio = this.GetComponent<AudioSource>();
        if (mode == 0) {
					audio.clip = normal;
			} else if (mode == 1) {
				audio.clip = hard;
        }
				audio.Play();
	}

	// Update is called once per frame
	void Update () {

	}
}
