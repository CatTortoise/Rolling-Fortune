using System;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace Animation
{
	public class AnimationAuthoring : MonoBehaviour
	{
		[SerializeField] private AnimationConfig<float3> position;
		[SerializeField] private AnimationConfig<float3> rotation;
		[SerializeField] private AnimationConfig<float3> rotationConstant;
		[SerializeField] private AnimationConfig<float> scale;

		public class Baker : Baker<AnimationAuthoring>
		{
			public override void Bake(AnimationAuthoring authoring)
			{
				var entity = GetEntity(TransformUsageFlags.Dynamic);
				if (authoring.position)
				{
					AddComponent(entity, new PositionAnimation() { targetLocal = authoring.position.target, animationSpeed = authoring.position.speed });
					SetComponentEnabled<PositionAnimation>(entity, false);
				}
				if (authoring.rotation)
				{
					AddComponent(entity, new RotationAnimationTarget() { targetLocal = math.radians(authoring.rotation.target), animationSpeed = math.radians(authoring.rotation.speed) });
					SetComponentEnabled<RotationAnimationTarget>(entity, false);
				}
				if (authoring.rotationConstant)
				{
					AddComponent<RotationAnimationConstant>(entity, math.radians(authoring.rotationConstant.target * authoring.rotationConstant.speed));
				}
				if (authoring.scale)
				{
					AddComponent(entity, new ScaleAnimation() { targetLocal = authoring.scale.target, animationSpeed = authoring.scale.speed });
					SetComponentEnabled<ScaleAnimation>(entity, false);
				}
			}
		}

		[Serializable]
		private struct AnimationConfig<T>
		{
			public bool enabled;
			public T target;
			public float speed;

			public static implicit operator bool(in AnimationConfig<T> config) => config.enabled;
		}
	}
}
