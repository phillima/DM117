using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

    [SerializeField]
    Sprite[] damageSprites;

    //Referencia para o audio source
    AudioSource blockAudioSource;
    //Referencia para o sprite renderer
    SpriteRenderer blockSpriteRenderer;
    //Referencia para o score text
    [SerializeField]
    GameObject scoreText;
    //Referencia para o canvas
    Canvas canvas;

    int maxHits;
    int numHits;
    // Use this for initialization
    void Start () {
        maxHits = damageSprites.Length + 1;
        blockAudioSource = GetComponent<AudioSource>();
        blockSpriteRenderer = GetComponent<SpriteRenderer>();
        canvas = FindObjectOfType<Canvas>();    
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Detectar colisao com a bola
    void OnCollisionEnter2D(Collision2D collision) {
        HandleDamage();
    }
    //Metado para tratar colisao no bloco
    public void HandleDamage() {
        AudioSource.
            PlayClipAtPoint(blockAudioSource.clip,
                            Camera.main.transform.position);
        numHits++;
        if(numHits >= maxHits) {
            SpawnScoreText();
            Destroy(gameObject);
        } else {
            blockSpriteRenderer.sprite = 
                damageSprites[numHits - 1];
        }
        
    }

    void SpawnScoreText() {
        GameObject scoreTextClone = Instantiate(scoreText,
            transform.position,
            transform.rotation,
            canvas.transform);
        Text text = scoreTextClone.
            GetComponentInChildren<Text>();

        text.color = blockSpriteRenderer.color;
        text.text = (maxHits * 20).ToString();
        Destroy(scoreTextClone,2.0f);

    }
}
