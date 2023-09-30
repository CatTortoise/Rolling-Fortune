using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UnityAnalyticsManager : MonoBehaviour
{
    [SerializeField] private string game_version;

    public void LevelComplete(int level, int LivesLeft , int StartTime , int CompletedTime)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
        {
            {"Level", level },
            {"LivesLeft", LivesLeft },
            {"StartTime", StartTime },
            {"CompletedTime", CompletedTime }
        };

        AnalyticsResult DebugCustomEvent = Analytics.CustomEvent(("LevelComplete_" + game_version), analyticsData);

        Debug.Log("Analytics Result (level complete): " + DebugCustomEvent);
    }

}
