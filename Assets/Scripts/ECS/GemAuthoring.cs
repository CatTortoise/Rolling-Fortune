using UnityEngine;
using Unity.Entities;

public class GemAuthoring : MonoBehaviour
{
	public class Baker : Baker<GemAuthoring>
	{
		public override void Bake(GemAuthoring authoring) => AddComponent<Gem>(GetEntity(TransformUsageFlags.Dynamic));
	}
}
