using UnityEngine;
using Unity.Entities;
using Input;

namespace UI
{
	public class LevelBoard : MonoBehaviour
	{
		public void SetLevelBoard(bool level)
		{
			World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentDataRW<InputComponent>(World.DefaultGameObjectInjectionWorld.GetExistingSystem<InputSystem>()).ValueRW.resetTilt = level;
		}
	}
}
