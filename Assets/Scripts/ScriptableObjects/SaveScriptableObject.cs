using UnityEngine;

[CreateAssetMenu(fileName = "NewSaveSO", menuName = "SaveSO")]
public class SaveScriptableObject : ScriptableObject
{
    public float MatchTime;
    public float TimeBetweenEnemySpawn;
    public bool hasMusic;
}
