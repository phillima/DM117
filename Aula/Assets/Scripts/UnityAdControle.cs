using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdControle : MonoBehaviour {

    public static bool showAds = true;

    //Variavel para controlar o tempo
    public static DateTime? proxTempoReward = null;

    //Referencia para o obstaculo
    public static ObsComp obstaculo;

    //Metodo para mostrar anunicio simples (Simple Ad)
    public static void ShowAd() {

        //Opcoes para o ad
        //Chamar o callback apos o anuncio finalizar
        ShowOptions opcoes = new ShowOptions();
        opcoes.resultCallback = Unpause;

        //Mostra o anuncio
        if (Advertisement.IsReady()) {
            Advertisement.Show(opcoes);
        }

        MenuPause.pausado = true;
        Time.timeScale = 0;

    }

    /// <summary>
    /// Metodo para mostrar anuncio com recompensa
    /// </summary>
    public static void ShowAdReward() {

        proxTempoReward = DateTime.Now.AddSeconds(15);

        if (Advertisement.IsReady()) {
            //Pausar o jogo
            MenuPause.pausado = true;
            Time.timeScale = 0f;
            var opcoes = new ShowOptions {
                resultCallback = TratarMostrarResultado
            };
            Advertisement.Show(opcoes);
        }
    }

    /// <summary>
    /// Callback para despausar o jogo no ad simples
    /// </summary>
    /// <param name="result"></param>
    public static void Unpause(ShowResult result) {
        //Quando o anuncio abacar
        //sair do modo pausado
        MenuPause.pausado = false;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Callback para tratar o jogo no ad com recompensa
    /// </summary>
    public static void TratarMostrarResultado (ShowResult result) {

        switch (result) {
            case ShowResult.Finished:
                obstaculo.Continue();
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad pulado. Faz nada");
                break;
            case ShowResult.Failed:
                Debug.Log("Erro no ad");
                break;
        }
        MenuPause.pausado = false;
        Time.timeScale = 1f;
    }
}
