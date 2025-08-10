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
			if (CheckEscapedThisFrame())
				Escaped?.Invoke();
			if (CheckDiedThisFrame())
				Died?.Invoke();
		}

		private bool CheckEscapedThisFrame() => ChangedThisFrame(ref _escaped, _escapedQuery);

		private bool CheckDiedThisFrame() => ChangedThisFrame(ref _died, _diedQuery);

		private static bool ChangedThisFrame(ref bool previous, EntityQuery thisFrame)
		{
			var newValue = !thisFrame.IsEmpty;
			var changed = (previous, newValue) switch
			{
				(false, true) => true,
				_ => false
			};
			previous = newValue;
			return changed;
		}
	}
}
