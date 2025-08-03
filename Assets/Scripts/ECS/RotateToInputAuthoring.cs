using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class RotateToInputAuthoring : MonoBehaviour
{
	[Tooltip("Rotation scale in degrees.")] public float rotationScale = 30;

	public class Baker : Baker<RotateToInputAuthoring>
	{
		public override void Bake(RotateToInputAuthoring authoring)
		{
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent<RotateToInputComponent>(entity, math.radians(authoring.rotationScale));
		}
	}
}
