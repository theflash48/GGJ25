using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState{Menu, Intro, Gameplay, Minigame, Cinematic}
    public GameState State;

    public GameObject panelMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameState.Menu:
                Menu();
                break;
            case GameState.Intro:
                Intro();
                break;
            case GameState.Gameplay:
                Gameplay();
                break;
            case GameState.Minigame:
                Minigame();
                break;
            case GameState.Cinematic:
                Cinematic();
                break;
        }
    }

    private void Menu()
    {
        panelMenu.SetActive(true);
    }

    private void Intro()
    {
        panelMenu.SetActive(false);
    }

    private void Gameplay()
    {
        panelMenu.SetActive(false);
    }

    private void Minigame()
    {
        panelMenu.SetActive(false);
    }

    private void Cinematic()
    {
        panelMenu.SetActive(false);
    }

    private void SetState(GameState newState)
    {
        State = newState;
    }
    
    //Buttons

    public void Play()
    {
        SetState(GameState.Intro);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
