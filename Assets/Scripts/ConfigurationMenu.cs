using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationMenu : MonoBehaviour
{
    [SerializeField] SaveScriptableObject _saveScriptableObject;
    [SerializeField] Slider _matchTimerSlider;
    [SerializeField] TextMeshProUGUI _matchTimerText;
    [SerializeField] Slider _enemySpawnTimeSlider;
    [SerializeField] TextMeshProUGUI _enemySpawnTimeText;
    [SerializeField] Slider _maxEnemiesOnScreenSlider;
    [SerializeField] TextMeshProUGUI _maxEnemiesOnScreenText;
    [SerializeField] Toggle _musicToggle;


    public void ReloadConfigurations()
    {
        _matchTimerSlider.value = _saveScriptableObject.MatchTime;
        _enemySpawnTimeSlider.value = _saveScriptableObject.TimeBetweenEnemySpawn;
        _maxEnemiesOnScreenSlider.value = _saveScriptableObject.MaxEnemiesOnScreen;
        _musicToggle.isOn = _saveScriptableObject.HasMusic;

    }

    public void SetMatchTime()
    {
        _saveScriptableObject.MatchTime = _matchTimerSlider.value;
        _matchTimerText.text = _matchTimerSlider.value.ToString();
    }

    public void SetEnemySpawnTime()
    {
        _saveScriptableObject.TimeBetweenEnemySpawn = Mathf.Round(_enemySpawnTimeSlider.value * 100.0f) * 0.01f;
        _enemySpawnTimeText.text = _saveScriptableObject.TimeBetweenEnemySpawn.ToString();
    }

    public void SetMaxEnemiesOnScreen()
    {
        _saveScriptableObject.MaxEnemiesOnScreen = (int)_maxEnemiesOnScreenSlider.value;
        _maxEnemiesOnScreenText.text = _saveScriptableObject.MaxEnemiesOnScreen.ToString();
    }

    public void SetMusicOnAndOff()
    {
        _saveScriptableObject.HasMusic = _musicToggle.isOn;
    }
}
