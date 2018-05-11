using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour {

    /// <summary>
    /// Alvo onde a camera ira olhar/seguir
    /// </summary>
    [SerializeField]
    private Transform alvo;

    /// <summary>
    /// Offset da camera em relacao ao seu alvo
    /// </summary>
    private Vector3 offset = new Vector3(0, 3, -6);

    void Update() {
        //Verifica se existe um alvo
        if (alvo) {
            //Faz a camera se deslocar ate o alvo
            transform.position = alvo.position + offset;
            //Faz a camera olhar para a posicao do alvo. Observe que estamos olhando para a posicao, e nao o game object
            transform.LookAt(alvo.position);
        }
    }
}
