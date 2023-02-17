using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private SaveScriptableObject _saveScriptableObject;
    private int sceneIndex;

    private void Awake()
    {;
        var player = GameObject.FindGameObjectWithTag("Player");
        if(player) player.GetComponent<HealthHandler>().onDead += ShowMenu;
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void ShowMenu()
    {
        Time.timeScale = 0.3f;
        endPanel.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    public void ClearFadeScreen()
    {
        _fadeAnimator.Play("FadeOut");
    }

    public void CloseFadeScreen()
    {
        _fadeAnimator.Play("FadeIn");
    }

    public void SetSceneIndex(int index)
    {
        sceneIndex = index;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
