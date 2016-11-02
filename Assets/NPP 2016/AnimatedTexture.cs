using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour {
	public Texture[] textures;
	public float changeInterval = 0.33f;
	float index;
	int indexInt;
	// Update is called once per frame
	void Update () {
		if( textures.Length == 0 ) // nothing if no textures
			return;
		// we want this texture index now
		index = Time.time / changeInterval;
		indexInt = (int)index;
		// take a modulo with size so that animation repeats
		indexInt = indexInt % textures.Length;
		// assign it
		GetComponent<Renderer>().material.mainTexture = textures[indexInt];
	}
}
	
