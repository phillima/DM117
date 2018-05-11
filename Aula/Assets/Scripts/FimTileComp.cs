using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimTileComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo para destruir o tile basico")]
    private float tempoDestruir = 2.0f;
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {

        //Verifica se foi a bola que colodiu
        if (other.GetComponent<JogadorComp>()) {

            //Cria um novo tile novo, fazendo o jogo ficar inifinito
            GameObject.FindObjectOfType<Controlador>().
                SpawnProxTile();

            //Destroi o pai deste objeto. Destroi o TileBasico
            Destroy(transform.parent.gameObject, tempoDestruir);
        }

    }

}
