using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

    [SerializeField]
    [Tooltip("Referencia para o tile basico")]
    private Transform tileBasico;

    [SerializeField]
    [Tooltip("Referencia para o obstaculo")]
    private Transform obstaculo;

    /// <summary>
    /// Numero de tiles iniciais
    /// </summary>
    [SerializeField]
    [Range(5,20)]
    private int numTilesSpawn;

    [SerializeField]
    [Tooltip("Numero de tiles sem obstaculo")]
    [Range(1,5)]
    private int numTileSemOBS;

    /// <summary>
    /// Posicao do primeiro tile
    /// </summary>
    private Vector3 posInicial = new Vector3(0,0,-5);

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

        //Define a posicao e rotacao do primeiro tile
        proxTilePos = posInicial;
        proxTileRot = Quaternion.identity;

        
        for (int i = 0; i < numTilesSpawn; i++) {
            SpawnTiles(i >= numTileSemOBS);
        }

	}

    
    public void SpawnTiles(bool temObs = true) {
        //Cria novo tile
        var novoTile = Instantiate(tileBasico, proxTilePos, proxTileRot);

        //Prepara a posicao e rotacao do proximo tile
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;

        //Verifica se devemos criar obstaculos
        if (!temObs)
            return;

        //Tratar a criacao de obstaculos
        var pontosObs = new List<GameObject>();

        foreach(Transform filho in novoTile) {
            if (filho.CompareTag("Obstaculo")) {
                pontosObs.Add(filho.gameObject);
            }
        }

        //Garantir que existe pelo menos um ponto disponivel para o obstaculo
        if(pontosObs.Count > 0) {

            //Escolhemos um ponto dos tres possiveis.
            var pontoObs = pontosObs[Random.Range(0, pontosObs.Count)];

            //Obtemos a posicao deste ponto
            var pontoPos = pontoObs.transform.position;

            //Criamos o novo obstaculo e fazemos o ponto de spaw ser o game object pai
            var novoObs = Instantiate(obstaculo, pontoPos, Quaternion.identity, pontoObs.transform);

        }
    }
}
