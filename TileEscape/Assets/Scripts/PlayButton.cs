using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GameSession gameSession = FindObjectOfType<GameSession>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(gameSession.StartGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
