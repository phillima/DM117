using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

    [SerializeField]
    Transform tileBasico;

    Vector3 pontoInicial = new Vector3(0, 0, -5);

    [SerializeField]
    [Tooltip("Numero Inicial de tiles")]
    [Range(4, 20)]
    int numIniTiles;

    Vector3 proxTilePos;

    Quaternion proxTileRot;

	// Use this for initialization
	void Start () {

        //Preparando o tile inicial
        proxTilePos = pontoInicial;
        proxTileRot = Quaternion.identity;

        for (int i = 0; i < numIniTiles; i++) {
            SpawnProxTile();
        }

	}

    public void SpawnProxTile() {

        var novoTile = Instantiate(tileBasico
                        , proxTilePos,
                        proxTileRot);

        //Detectar o local do prox spawn
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
