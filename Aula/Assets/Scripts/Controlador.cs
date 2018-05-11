using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

    [Tooltip("Uma referencia para o tile basico")]
    [SerializeField]
    private Transform tile;
    
    [Tooltip("Uma referencia para o osbstaculo")]
    [SerializeField]
    private Transform obstaculo;

    /// <summary>
    /// Ponto inicial do primeiro tile
    /// </summary>
    private Vector3 pontoInicial = new Vector3(0, 0, 5);

    [SerializeField]
    [Range(10,50)]
    private int numInitSpawn;

    [SerializeField]
    [Range(1,4)]
    private int numTileSemObs = 4;

    /// <summary>
    /// Posicao do proximo tile
    /// </summary>
    private Vector3 proxTilePos;

    /// <summary>
    /// Rotacao do proximo tile
    /// </summary>
    private Quaternion proxTileRot;

	// Use this for initialization
	void Start () {

        //Preparando o ponto inicial
        proxTilePos = pontoInicial;
        proxTileRot = Quaternion.identity;

        for(int i = 0; i < numInitSpawn; i++)
            SpawnProxTile(i >= numTileSemObs);

	}

    public void SpawnProxTile(bool spawnObs = true) {

        var novoTile = Instantiate(tile, proxTilePos, 
            proxTileRot);

        //Detectar qual o local do proximo
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;

        //Verifica se podemos criar obstaculos
        if (!spawnObs)
            return;

        //Iniciar o tratamento da criacao de osbtaculos
        var pontosObs = new List<GameObject>();

        //Varrer o tile basico para buscarmos os pontos obs
        foreach(Transform filho in novoTile) {
            if (filho.CompareTag("Obstaculo"))
                pontosObs.Add(filho.gameObject);
        }

        //Garantir que existe pelo menos um ponto de obs
        if(pontosObs.Count > 0) {

            //Pegar uma ponto spawn aleatorio
            var pontoSpawn = pontosObs
                [Random.Range(0, pontosObs.Count)];

            //Pegamos a posicao do ponto de spawn
            var pontoSpawnPos = pontoSpawn.transform.position;

            var novoObs = Instantiate(obstaculo,
                            pontoSpawnPos, 
                            Quaternion.identity);

            //Faz o obstaculo ser filho do seu ponto de spawn
            novoObs.SetParent(pontoSpawn.transform);
        }
    }
	
}
