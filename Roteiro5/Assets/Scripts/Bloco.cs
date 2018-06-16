using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour {

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private ParticleSystem explosion;

    public static int numBlocosDestrutivel = 0;

    private SpriteRenderer spriteRenderer;
    private LevelControle levelControle;
    private int numHits;

    private AudioSource audioSource;
    

    // Use this for initialization
    void Start () {
        numHits = 0;
        levelControle = FindObjectOfType<LevelControle>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (transform.CompareTag("Destrutivel")) {
            numBlocosDestrutivel++;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (transform.CompareTag("Destrutivel")){
            AudioSource.PlayClipAtPoint(audioSource.clip,
                        transform.position);
            EfeitoExplosao();
            TratarDano();
        }
    }

    private void TratarDano() {
        numHits++;
        int maxHits = sprites.Length + 1;
        if (numHits >= maxHits) {
            numBlocosDestrutivel--;
            levelControle.BlocoDestruido();
            Destroy(gameObject);
        } else {
            CarregaSprite();
        }
    }

    private void CarregaSprite() {
        int spriteIndex = numHits - 1;
        if (sprites[spriteIndex]) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void EfeitoExplosao() {
        ParticleSystem ps = Instantiate(explosion,transform.position,
                            Quaternion.identity);
        ParticleSystem.MainModule main = ps.main;
        main.startColor = spriteRenderer.color;

    }
}
