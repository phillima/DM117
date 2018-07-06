using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour {

    const string PONTO_SPAWN = "PontoSpawn";
    const string TAG_OBS_SPAWN = "Obstaculo";

    [SerializeField]
    [Tooltip("Referencia para nosso Tile. Usar o prefab TileBasico")]
    Transform tilebasico;

    [SerializeField]
    [Tooltip("Referencia para o obstaculo. Usar o prefab")]
    Transform obstaculo;

    [SerializeField]
    [Tooltip("Numero inicial de Tiles")]
    [Range(1,20)]
    int numIniSpawn = 15;

    [SerializeField]
    [Tooltip("Numero de tiles sem obstaculo no inicio do jogo")]
    [Range(1,4)]
    int numIniSemOBS = 4;

    /// <summary>
    /// Posicao onde será instanciado o primeiro TileBasico
    /// </summary>
    Vector3 posicaoPrimeiroTile = new Vector3(0, 0, -5);

    /// <summary>
    /// Posicao do proximo Tile
    /// </summary>
    Vector3 proxTilePos;

    /// <summary>
    /// Rotacao do proximo Tile
    /// </summary>
    Quaternion proxTileRot;

    // Use this for initialization
	void Start () {
        //Definindo os valores iniciais
        proxTilePos = posicaoPrimeiroTile;
        proxTileRot = Quaternion.identity;
        //Criar um Tile baseado no numero inicial
        for (int i = 0; i < numIniSpawn; i++) {
            SpawnProxTile(i >= numIniSemOBS);
        }

	}

    public void SpawnProxTile(bool spawnObstaculo = true) {

        //Cria novo tile
        var novoTile = Instantiate(tilebasico, proxTilePos, proxTileRot);

        //Buscando o PontoSpawn. Usaremos para os proximos tiles
        var pontoSpawn = novoTile.Find(PONTO_SPAWN);

        //Pegando a posicao e rotacao
        proxTilePos = pontoSpawn.position;
        proxTileRot = pontoSpawn.rotation;

        //Parte responsavel pelo spawnObstaculo
        if (!spawnObstaculo)
            return;

        //Buscar pontos de spawn de obstaculo.
        var pontosSpawnOBS = new List<GameObject>();
        foreach (Transform filho in novoTile) {
            if (filho.CompareTag(TAG_OBS_SPAWN))
                pontosSpawnOBS.Add(filho.gameObject);
        }
        //Verifica se foi encontrado ao menos um ponto de spawn
        if(pontosSpawnOBS.Count > 0) {
            //Buscar um ponto spawn aleatorio
            var pontoSpawnOBS = pontosSpawnOBS[Random.Range(0, pontosSpawnOBS.Count)];

            //Buscar a posicao do ponto
            var pontoSpawnOBSPos = pontoSpawnOBS.transform.position;

            //Instancia do obstaculo
            var novoObs = Instantiate(obstaculo, pontoSpawnOBSPos, Quaternion.identity, pontoSpawnOBS.transform);

        }
    }
}
