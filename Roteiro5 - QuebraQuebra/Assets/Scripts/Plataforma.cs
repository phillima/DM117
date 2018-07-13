using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {

    [SerializeField]
    private bool autoPlay = false;

    private Bola bola;

    private float offset;

    private void Start() {
        bola = FindObjectOfType<Bola>();
        offset = bola.transform.position.x - transform.position.x;
    }

    // Update is called once per frame
    void Update () {
        if (!autoPlay) {
            MovimentoMouse();
        } else {
            if (bola.JogoComecou) {
                MovimentoAutomatico();
            }
        }
	}

    private void MovimentoMouse() {
        float mousePosWorldUnitX =
            ((Input.mousePosition.x)
            / Screen.width * 16);
        Vector2 plataformaPos =
            new Vector2(0,
            transform.position.y);

        plataformaPos.x = Mathf.Clamp(mousePosWorldUnitX,
            0f, 15f);
        transform.position = plataformaPos;
    }

    private void MovimentoAutomatico() {
        float bolaPosX = bola.transform.position.x - offset;
        Vector2 plataformaPos =
            new Vector2(0, transform.position.y);
        plataformaPos.x = Mathf.Clamp(bolaPosX, 0f, 15f);
        transform.position = plataformaPos;
         
    }
}
