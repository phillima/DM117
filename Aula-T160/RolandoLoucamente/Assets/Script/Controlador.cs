using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

    [SerializeField]
    Transform tileBasico;
    [SerializeField]
    Transform obstaculo;

    Vector3 pontoInicial = new Vector3(0, 0, -5);

    [SerializeField]
    [Tooltip("Numero Inicial de tiles")]
    [Range(4, 20)]
    int numIniTiles;

    int numTilesSemObs = 4;

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

    public void SpawnProxTile(bool temObs = true) {

        var novoTile = Instantiate(tileBasico
                        , proxTilePos,
                        proxTileRot);

        //Detectar o local do prox spawn
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;

        //Criacao de obstaculos
        if (!temObs)//Verifica se deve criar obstaculos
            return;
        //Lista com os pontos de spawn
        var pontosObstaculos = new List<GameObject>();

        //Buscar os filhos para encontrar os pts obstaculo
        foreach (Transform filho in novoTile) {
            if (filho.CompareTag("Obstaculo")) {
                pontosObstaculos.Add(filho.gameObject);
            }
        }

        if(pontosObstaculos.Count > 0) {

            //Buscando o GO que representa o ponto spaw obs
            var pontoSpawn = pontosObstaculos[Random.Range(0, pontosObstaculos.Count)];                

            //Buscando a posicao
            var obsSpawnPos = pontoSpawn.transform.position;

            //Criar um novo obstaculo
            Instantiate(obstaculo, obsSpawnPos, 
                Quaternion.identity,pontoSpawn.transform);

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
