using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Management
{
    public class UnityAnalyticsManager : MonoBehaviour
    {
        [SerializeField] private string _gameVersion;

        public static UnityAnalyticsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public void LevelComplete(int level, int LivesLeft, bool LevelCompleted)
        {
            Dictionary<string, object> analyticsData = new Dictionary<string, object>
        {
            {"Level", level },
            {"LivesLeft", LivesLeft },
            {"LevelCompleted", LevelCompleted}
        };

            AnalyticsResult DebugCustomEvent = Analytics.CustomEvent(("LevelComplete_" + _gameVersion), analyticsData);

            Debug.Log("Analytics Result (level complete): " + DebugCustomEvent);
        }
    }
}
