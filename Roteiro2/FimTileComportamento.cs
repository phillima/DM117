using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimTileComportamento : MonoBehaviour {

    [Tooltip("Tempo esperado antes de destruir o TileBasico")]
    public float tempoDestruir = 2.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other) {

        //Vamos ver se foi a bola que passou pelo fim do TileBasico
        if (other.GetComponent<JogadorComportamento>())
        {
            //Como foi a bola, vamos criar um TileBasico no proximo ponto
            //Mas esse proximo ponto esta depois do ultimo TileBasico presente na cena
            GameObject.FindObjectOfType<ControladorJogo>().SpawnProxTile();

            //E agora destroi esse TileBasico. 
            Destroy(transform.parent.gameObject, tempoDestruir);
        }
    }
}
