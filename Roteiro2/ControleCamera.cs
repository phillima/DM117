using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour {

    [Tooltip("O alvo a ser acompanhdo pela camera")]
    public Transform alvo;

    [Tooltip("Offset da camera em relação ao alvo")]
    public Vector3 offset = new Vector3(0,3,-6);
	
	// Update is called once per frame
	void Update () {
        if (alvo != null) {
            //Altera a posicao da camera
            transform.position = alvo.position + offset;

            //Altera a rotacao da camera em relacao 
            transform.LookAt(alvo);
        }

	}
}
