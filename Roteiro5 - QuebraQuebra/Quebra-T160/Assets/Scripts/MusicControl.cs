using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {

    public static MusicControl 
        musicaControle = null;

    private void Awake() {
        if(musicaControle != null) {
            Destroy(gameObject);
        } else {
            musicaControle = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
