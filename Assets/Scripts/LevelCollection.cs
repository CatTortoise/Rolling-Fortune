using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities.Serialization;

[CreateAssetMenu(menuName = "LevelCollection", fileName = "LevelCollection", order = int.MaxValue)]
public class LevelCollection : ScriptableObject, IReadOnlyList<EntitySceneReference>
{
	[SerializeField] private List<EntitySceneReference> _levels;

	public EntitySceneReference this[int index] => _levels[index];

	public int Count => _levels.Count;

	public List<EntitySceneReference>.Enumerator GetEnumerator() => _levels.GetEnumerator();

	IEnumerator<EntitySceneReference> IEnumerable<EntitySceneReference>.GetEnumerator() => _levels.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => _levels.GetEnumerator();
}
