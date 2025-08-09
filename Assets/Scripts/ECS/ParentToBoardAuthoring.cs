using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Input;

namespace InGame
{
	public class ParentToBoardAuthoring : MonoBehaviour
	{
		public class Baker : Baker<ParentToBoardAuthoring>
		{
			public override void Bake(ParentToBoardAuthoring authoring)
			{
				var entity = GetEntity(TransformUsageFlags.Dynamic);
				AddComponent<Parent>(entity, new() { Value = GetEntity(authoring.GetComponentInParent<RotateToInputAuthoring>(), TransformUsageFlags.Dynamic) });
			}
		}
	}
}
