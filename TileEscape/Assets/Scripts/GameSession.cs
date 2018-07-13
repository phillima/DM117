using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {

    //Variaveis de controle do jogo
    public int numPlayerLife = 3;
    public static bool gameStart = false;

    //Implementacao do singleton para o GameSession
    void Awake() {
        if(FindObjectsOfType<GameSession>().Length > 1) {
            Destroy(gameObject);
        }else {
            DontDestroyOnLoad(gameObject);
        }  
    }

    //Metodo executado na primeira parte do jogo
    //Para iniciar o jogo
    public void StartGame() {
        GameObject door = GameObject.Find("Door");
        Animator doorAnimator = door.GetComponent<Animator>();
        doorAnimator.SetTrigger("OpenDoor");
    }

    //Invocado sempre que o jogador eh morto
    public void PlayerDied() {
        if (numPlayerLife > 1) 
            RemoveLife();
        else
            RestartGame();
    }

    //Reiniciar jogo
    void RestartGame() {
        SceneManager.LoadScene(4);//Invoca a tela de game over
        Destroy(gameObject);
    }

    void RemoveLife() {
        numPlayerLife--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}   

