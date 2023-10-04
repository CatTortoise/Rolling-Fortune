using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InGame;

namespace Management
{
    [RequireComponent(typeof(LevelManager))]
    public class GemManager : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private List<Gem> _gemList;

        private bool AllGemsCollected { get => !_gemList.Any(); }

        private void OnValidate()
        {
            _levelManager = GetComponent<LevelManager>();
            _gemList = GetComponentsInChildren<Gem>().ToList();
        }

        private void GemCheck()
        {
            if (AllGemsCollected)
                _levelManager.OnAllGemsCollected();
        }

        public void GemCollected(Gem gem)
        {
            if (_gemList.Contains(gem))
                _gemList.Remove(gem);
            GemCheck();
        }
    }
}
