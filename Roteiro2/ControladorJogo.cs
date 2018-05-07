using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controlar a parte principal do jogo
/// </summary>
public class ControladorJogo : MonoBehaviour {

    [Tooltip("Referencia para o TileBasico")]
    public Transform tile;

    [Tooltip("Referencia para o Obstaculo")]
    public Transform obstaculo;

    [Tooltip("Ponto para se colocar o TileBasicoInicial")]
    public Vector3 pontoInicial = new Vector3(0, 0, -5);

    [Tooltip("Quantidade de Tiles iniciais")]
    [Range(1, 20)]
    public int numSpawnIni = 15;

    [Tooltip("Numero de Tiles sem obstaculos")]
    [Range(1,4)]
    public int numTileSemOBS = 4;

    /// <summary>
    /// Local para spawn do proximo Tile
    /// </summary>
    private Vector3 proxTilePos;

    /// <summary>
    /// Rotacao do proximo Tile
    /// </summary>
    private Quaternion proxTileRot;

	// Use this for initialization
	void Start () {
        // Preparando o ponto inicial
        proxTilePos = pontoInicial;
        proxTileRot = Quaternion.identity;

        for (int i = 0; i < numSpawnIni; i++)
        {
            SpawnProxTile(i >= numTileSemOBS);
        }
	}

    public void SpawnProxTile(bool spawnObstaculos = true)
    {
        var novoTile = Instantiate(tile, proxTilePos, proxTileRot);

        //Detectar qual o local de spawn do prox tile
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;


        //Verifica se ja podemos criar Tiles com obstaculo.
        if (!spawnObstaculos)
            return;

        //Podemos criar obstaculos

        //Primeiro devemos buscar todos os locais possiveis
        var pontosObstaculo = new List<GameObject>();

        //Varrer os GOs filhos buscando os pontos de spawn
        foreach (Transform filho in novoTile)
        {
            //Vamos verificar se possui a TAG PontoSpawn
            if (filho.CompareTag("PontoSpawn"))
                //Se for o adicionamos na lista como potencial ponto de spawn
                pontosObstaculo.Add(filho.gameObject);
        }
        
        //Garantir que existe pelo menos um spawn point disponível
        if(pontosObstaculo.Count > 0){

            //Vamos pegar um ponto aleatório
            var pontoSpawn = pontosObstaculo[Random.Range(0, pontosObstaculo.Count)];

            //Vamos guardar a posicao desse ponto de spawn
            var obsSpawnPos = pontoSpawn.transform.position;

            //Cria um novo obstaculo
            var novoObs = Instantiate(obstaculo, obsSpawnPos, Quaternion.identity);

            //Faz ele ser filho do TileBasico.PontoSpawn (centro, esq ou direita)
            //Outra forma de fazer isso eh no proprio Instantiate. Ja existe uma sobrecarga para adicionar 
            //um parent.
            novoObs.SetParent(pontoSpawn.transform);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
