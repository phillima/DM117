using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimTileComportamento : MonoBehaviour {

    /// <summary>
    /// Tempo para destruir o tile
    /// </summary>
    float tempoDestruir = 2.0f;

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<JogadorControle>()) {
            FindObjectOfType<ControladorJogo>().SpawnProxTile();
            Destroy(transform.parent.gameObject, tempoDestruir);
        }    
    }
}
