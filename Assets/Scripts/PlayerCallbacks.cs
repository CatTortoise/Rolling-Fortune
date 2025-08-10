using UnityEngine;
using UnityEngine.Events;
using Unity.Entities;
using Unity.Collections;

namespace UI
{
	public class PlayerCallbacks : MonoBehaviour
	{
		private EntityQuery _escapedQuery, _diedQuery;
		private bool _escaped, _died;

		public event UnityAction Escaped, Died;

		private void OnEnable()
		{
			var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			EntityQueryBuilder builder = new(Allocator.Temp);
			_escapedQuery = builder.WithAll<Escaped>().Build(entityManager);
			_diedQuery = builder.Reset().WithAll<Died>().Build(entityManager);
			builder.Dispose();
		}

		private void Update()
		{
			if (CheckEscaped())
				Escaped?.Invoke();
			if (CheckDied())
				Died?.Invoke();
		}

		private bool CheckEscaped()
		{
			if (_escapedQuery.IsEmpty != _escaped)
				return false;
			return _escaped = true;
		}

		private bool CheckDied()
		{
			if (_diedQuery.IsEmpty != _died)
				return false;
			return _died = true;
		}
	}
}
