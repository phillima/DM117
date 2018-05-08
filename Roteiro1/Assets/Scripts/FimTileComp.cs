using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimTileComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo para destruir o tile anterior")]
    private float tempoDestruir = 2.0f;

    private void OnTriggerEnter(Collider other) {

        //Verifica se colidiu com a bola
        if (other.GetComponent<JogadorComp>()) {

            //Cria um novo tile
            GameObject.FindObjectOfType<Controlador>().SpawnTiles();
            //Destroi o tile atual
            Destroy(transform.parent.gameObject, tempoDestruir);

        }
    }
}
