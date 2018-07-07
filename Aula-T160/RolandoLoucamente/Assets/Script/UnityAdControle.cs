using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAdControle : MonoBehaviour {

    //Variavel que verifica se podemos mostrar anuncio
    public static bool showsAds = true;


    //Metodo para anuncio simples
    public static void ShowAd() {

        //Criando uma opcao para
        //despausar o jogo
        ShowOptions opcoes = new ShowOptions();
        opcoes.resultCallback = Unpause;

#if UNITY_ADS
        if (Advertisement.IsReady()) {
            Advertisement.Show(opcoes);
        }
        MenuPause.isPausado = true;
        Time.timeScale = 0;
#endif

    }

#if UNITY_ADS
    public static void Unpause(ShowResult result) {
        //Quando o anuncio acabar
        //Sera executado esse metodo
        //Para o jogo despausar
        MenuPause.isPausado = false;
        Time.timeScale = 1;
    }

#endif

}
