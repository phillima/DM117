using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FimTileComportamento : MonoBehaviour
{
    [Tooltip("Tempo esperado antes de destruir o TileBasico")]
    public float tempoDestruir = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Verificar se a bola passou pelo fim do tileBasico
        if (other.GetComponent<JogadorComportamento>())
        {
            // Como foi a bola, vamos criar no proximo ponto
            // Mas esse ponto está depois do ultimo TileBasico presente na cena
            GameObject.FindObjectOfType<ControladorJogo>().SpawnProxTile();
            
            // Destroy(this, tempoDestruir);
            // E agora destroi esse TileBasico
            Destroy(transform.parent.gameObject, tempoDestruir);
            print("aqui destroi");
        }
    }
}