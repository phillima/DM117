using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObsComp : MonoBehaviour {

    [SerializeField]
    private GameObject explosao;
    
    //Referencia para o jogador
    private GameObject jogador;

    [Tooltip("Quanto tempo antes de reiniciar o jogo")]
    private float tempoEspera = 2.0f;

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.GetComponent<JogadorComp>()) {
            //Destroy(collision.gameObject);
            
            //Faz o jogador ficar desativado
            collision.gameObject.SetActive(false);

            //Salva uma referencia para o jogador
            jogador = collision.gameObject;

            //Chamada o metodo resetar
            Invoke("Reset", tempoEspera);

        }

    }

    /// <summary>
    /// Metodo para reiniciar o jogo
    /// </summary>
    private void Reset() {

        //Faz a busca do MenuGameOver
        var gameOverMenu = GetGameOverMenu();
        gameOverMenu.SetActive(true);

        //Busca os botoes do menu gameOver
        var botoes = gameOverMenu.transform.
                    GetComponentsInChildren<Button>();
        Button botaoContinue = null;

        //Busca o botao continue dentro do menu game over
        foreach(var botao in botoes) {
            if (botao.gameObject.name.Equals("Botao-Continuar")) {
                botaoContinue = botao;
                break;
            }
        }

        if(botaoContinue != null) {
            StartCoroutine(ShowContinue(botaoContinue));

            //botaoContinue.onClick.AddListener(
            //    UnityAdControle.ShowAdReward);
            //UnityAdControle.obstaculo = this;
        }

        //Reinicia o level
        //SceneManager.LoadScene(SceneManager.
          //  GetActiveScene().name);
    }

    /// <summary>
    /// Metodo usado para destruir o obstaculo via toque na tela
    /// </summary>
    public void ObjetoTocado() {
        if(explosao != null) {
            var particulas = Instantiate(explosao, transform.position,
                                Quaternion.identity);
            Destroy(particulas, 1.0f);
        }
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Metodo para continuar o jogo
    /// Caso o jogador veja o anuncio
    /// </summary>
    public void Continue() {
        var go = GetGameOverMenu();
        go.SetActive(false);
        jogador.SetActive(true);
        ObjetoTocado();
    }

    /// <summary>
    /// Metodo para buscar o MenuGameOver
    /// </summary>
    /// <returns></returns>
    private GameObject GetGameOverMenu() {
        return GameObject.Find("Canvas").transform.
                    Find("MenuGameOver").gameObject;
    }

    /// <summary>
    /// Co-rotina para verificar se o botao pode
    /// ou nao ser clicado
    /// </summary>
    /// <param name="botaoContinue"></param>
    /// <returns></returns>
    public IEnumerator ShowContinue(Button botaoContinue) {
        var btnText = botaoContinue.GetComponentInChildren<Text>();
        while (true) {
            if(UnityAdControle.proxTempoReward.HasValue &&
            (DateTime.Now < UnityAdControle.proxTempoReward.Value)) {
                botaoContinue.interactable = false;

                TimeSpan restante = UnityAdControle.proxTempoReward.Value 
                                - DateTime.Now;

                var contagemRegressiva = string.Format("{0:D2}:{1:D2}",
                                                    restante.Minutes,
                                                    restante.Seconds);
                btnText.text = contagemRegressiva;
                yield return new WaitForSeconds(1f);
            } else {
                botaoContinue.interactable = true;
                botaoContinue.onClick.AddListener(
                    UnityAdControle.ShowAdReward);
                UnityAdControle.obstaculo = this;
                btnText.text = "Continue - Ver Ad";
                break;
            }
        }

    }
}
