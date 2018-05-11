using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour {
    
    [SerializeField]
    private Transform alvo;

    [SerializeField]
    private Vector3 offset = new Vector3(0,3,-6);

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        transform.position = alvo.position + offset;

        transform.LookAt(alvo);

	}
}
