using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerScoreScriptableObject", menuName = "ScriptableObjects/PlayerScore")]
public class PlayerScoreScriptableObject : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<int> scoreChangedEvent;

    public int PlayerScore
    { get; private set; }

    private void OnEnable()
    {
        scoreChangedEvent ??= new UnityEvent<int>();
        ResetScore();
    }

    public void ResetScore()
    {
        PlayerScore = 0;
    }

    public void RaiseScore(int by)
    {
        PlayerScore+= by;
        scoreChangedEvent.Invoke(PlayerScore);
    }
}
