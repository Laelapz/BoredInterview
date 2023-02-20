using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _actualTime;
    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private SaveScriptableObject _saveScriptableObject;

    private AudioSource _gameMusic;
    private GameObject _player;
    private int sceneIndex;
    private float _currentMatchTime = 0;
    private bool _hasGameEnd = true;

    private int _points = 0;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if(_player) _player.GetComponent<HealthHandler>().OnDead += ShowMenu;
        _gameMusic = GetComponent<AudioSource>();
        if (_saveScriptableObject) LoadSaveFile();
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if(!_hasGameEnd)
        {
            int min = (int)_currentMatchTime / 60;
            int sec = (int)_currentMatchTime % 60;
            _actualTime.text = min + ":" + sec;
        }

        if(_currentMatchTime <= 0 && !_hasGameEnd)
        {
            _hasGameEnd = true;
            _player.GetComponent<HealthHandler>().CanDamage = false;
            ShowMenu();
        }

        _currentMatchTime -= Time.deltaTime;
    }

    public void IncreasePoints()
    {
        _points += 100;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    private void ShowMenu()
    {
        StopAllCoroutines();
        Time.timeScale = 0.3f;
        _hasGameEnd = true;
        _pointsText.text = _points + " Points";
        endPanel.SetActive(true);
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

    private void LoadSaveFile()
    {
        _hasGameEnd = false;
        EnemySpawner.Instance.TimeBetweenSpawn = _saveScriptableObject.TimeBetweenEnemySpawn;
        EnemySpawner.Instance.MaxEnemiesOnScreen = _saveScriptableObject.MaxEnemiesOnScreen;
        _gameMusic.enabled = _saveScriptableObject.HasMusic ? true : false;
        _currentMatchTime = _saveScriptableObject.MatchTime;
    }
}
