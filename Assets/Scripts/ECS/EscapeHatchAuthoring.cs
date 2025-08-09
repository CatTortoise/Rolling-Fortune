using UnityEngine;
using Unity.Entities;

public class EscapeHatchAuthoring : MonoBehaviour
{
	public class Baker : Baker<EscapeHatchAuthoring>
	{
		public override void Bake(EscapeHatchAuthoring authoring) => AddComponent<EscapeHatch>(GetEntity(TransformUsageFlags.None));
	}
}
