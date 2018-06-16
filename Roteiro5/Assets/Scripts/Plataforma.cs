using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {

	// Update is called once per frame
	void Update () {

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
}
