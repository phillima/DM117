using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Classe para definir o comportamento do obstaculo
/// </summary>
public class ObsComp : MonoBehaviour {

    /// <summary>
    /// Variavel referencia para o jogador
    /// </summary>
    private GameObject jogador;

    [SerializeField]
    [Tooltip("Tempo para reiniciar o jogo")]
    private float tempoDestruir = 2.0f;

    [SerializeField]
    [Tooltip("Referencia para a explosao")]
    private GameObject explosao;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<JogadorComp>()) {
            //Vamos esconder o jogador 
            //ao inves de destruir
            collision.gameObject.SetActive(false);

            jogador = collision.gameObject; 

            //Antes de implementar o sistema de recompensa
            //Destruiamos o jogador diretamente
            //Destroy(collision.gameObject);

            //Chame a funcao Reset depois de um tempo
            Invoke("Reset", tempoDestruir);
        }
    }

    private void Reset() {

        //Faz o MenuGameOver aparecer
        var gameOverMenu = GetGameOverMenu();
        gameOverMenu.SetActive(true);

        //Busca os botoes do MenuGameOver
        var botoes = gameOverMenu.transform.GetComponentsInChildren<Button>();
        Button botaoContinue = null;

        //Varre todos os botoes, em busca do botao continue. 
        foreach (var botao in botoes) {
            if (botao.gameObject.name.Equals("BotaoContinuar")) {
                botaoContinue = botao;//Salva uma referencia para o botao continue
                break;
            }
        }
        //Verifica se o botaoContinue eh diferente de null
        if (botaoContinue) {
#if UNITY_ADS
            //Se o botao continue for clicado, iremos tocar o anúncio
            StartCoroutine(ShowContinue(botaoContinue));
#else
            //Se nao existe add, nao precisa mostrar o botao Continue
            botaoContinue.gameObject.SetActive(false);
#endif
        }
        //Nao eh necessario mais recarregar o jogo por aqui. 
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Faz o reset do jogo
    /// </summary>
    public void Continue() {
        var go = GetGameOverMenu();
        go.SetActive(false);
        jogador.SetActive(true);
        //Exploda essa obstaculo, caso o jogador resolvar apertar o Continue.
        ObjetoTocado();
    }
    
    /// <summary>
    /// Busca o MenuGameOver
    /// </summary>
    /// <returns>O GameObject MenuGameOver</returns>
    GameObject GetGameOverMenu() {
        return GameObject.Find("Canvas").transform.
            Find("MenuGameOver").gameObject;
    }

    /// <summary>
    /// Metodo para verificar se o obstaculo foi tocado
    /// </summary>
    public void ObjetoTocado() {
        if (explosao) {
            var particulas = Instantiate(explosao, transform.position, Quaternion.identity);
            Destroy(particulas, 1.0f);
        }
        Destroy(this.gameObject);
    }


    public IEnumerator ShowContinue(Button botaoContinue) {
        var btnText = botaoContinue.GetComponentInChildren<Text>();
        while (true) {
            if (UnityAdControle.proxTempoReward.HasValue &&
            (DateTime.Now < UnityAdControle.proxTempoReward.Value)) {
                botaoContinue.interactable = false;

                TimeSpan restante = UnityAdControle.proxTempoReward.Value - DateTime.Now;

                var contagemRegressiva = string.Format("{0:D2}:{1:D2}",
                                                       restante.Minutes,
                                                       restante.Seconds);
                btnText.text = contagemRegressiva;
                yield return new WaitForSeconds(1f);
            } else {
                botaoContinue.interactable = true;
                botaoContinue.onClick.AddListener(UnityAdControle.ShowRewardAd);
                UnityAdControle.obstaculo = this;
                btnText.text = "Continue (Ver Ad)";
                break;
            }
        }
    }
}
