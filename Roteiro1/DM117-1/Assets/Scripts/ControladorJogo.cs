using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
// ReSharper disable All


/// <summary>
/// Classe para controlar a parte principal do jogo
/// </summary>
public class ControladorJogo : MonoBehaviour
{
    [Tooltip("Referencia para TileBasico")]
    public Transform tile;

    [Tooltip("Ponto para se colocar o TileBasicoInicial")]
    public Vector3 pontoInicial = new Vector3(0, 0, -5);

    [Tooltip("Quantidade de Tiles iniciais")] [Range(1, 20)]
    public int numSpawnIni;

    [Tooltip("Referencia para o obstaculo")]
    public Transform obstaculo;

    /// <summary>
    /// Local para gerar o proximo tile
    /// </summary>
    private Vector3 proxTilepos;

    /// <summary>
    /// Local para spawn do proximo Tile
    /// </summary>
    private Quaternion proxTileRot;
    
    [Tooltip("Numeros de Tile sem obstaculos")]
    [Range(1,4)]
    public int numTileSemOBS = 4;

    // Start is called before the first frame update

    void Start()
    {
        // Preparando o ponto inicial 
        proxTilepos = pontoInicial;
        proxTileRot = Quaternion.identity;

        for (int i = 0; i < numSpawnIni; i++)
        {
            SpawnProxTile(i >= numTileSemOBS);
        }
    }


    // Update is called once per frame
    public void SpawnProxTile(bool spawnObstaculos = true)
    {
        var novoTile = Instantiate(tile, proxTilepos, proxTileRot);
        // Verifica se ja podemos criar Tiles com obstaculos
        
        print("Aqui constroi");

        // Detectar qual o local do proximo tile
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilepos = proxTile.position;
        proxTileRot = proxTile.rotation;
        
        if (!spawnObstaculos)
            return;

        // Podemos criar obstaculos 
        // Primeiro buscamos todos os locais possiveis 
        var pontosObstaculo = new List<GameObject>();

        // Varrer todos os game objects filhos buscando os pontos spawn (onde gerar)
        foreach (Transform filho in novoTile)
        {
            // Verificar se possui a TAG PontoSpawn
            if (filho.CompareTag("ObsSpawn"))
                // Se for o add a lista como potencial ponto de spawn
                pontosObstaculo.Add(filho.gameObject);
        }

        if (pontosObstaculo.Count > 0)
        {
            // Vamos pegar um ponto aleatorio
            var pontoSpawn = pontosObstaculo[Random.Range(0, pontosObstaculo.Count)];
            
            // Vamos guardar a possição desse ponto de spawn
            var obsSpawnPos = pontoSpawn.transform.position;
            
            // Criar um novo obstaculo
            var novoObs = Instantiate(obstaculo, obsSpawnPos, Quaternion.identity);
            
            // Faz ele ser filho do TileBasico.PontoSpawn (Centro, esq ou direita)
            // Outra forma de fazer isso é no Instanteate. Já existe uma sobrecarga para adicionar 
            // um parent
            novoObs.SetParent(pontoSpawn.transform);
        }

        print("to aqui {}" + pontosObstaculo);
    }


    void Update()
    {

    }
}