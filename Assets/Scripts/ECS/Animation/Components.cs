using Unity.Entities;
using Unity.Mathematics;

namespace Animation
{
	public struct PositionAnimation : IComponentData, IEnableableComponent
	{
		/// <summary>
		/// Target position.
		/// </summary>
		public float3 targetLocal;
		public float animationSpeed;
	}

	public struct RotationAnimationTarget : IComponentData, IEnableableComponent
	{
		/// <summary>
		/// Target rotation in euler (Radians).
		/// </summary>
		public float3 targetLocal;
		public float animationSpeed;
	}

	public struct RotationAnimationConstant : IComponentData, IEnableableComponent
	{
		/// <summary>
		/// Rotation speed per second (Radians).
		/// </summary>
		public float3 rotate;

		public static implicit operator float3(in RotationAnimationConstant animation) => animation.rotate;
		public static implicit operator RotationAnimationConstant(in float3 speed) => new() { rotate = speed };
	}

	public struct ScaleAnimation : IComponentData, IEnableableComponent
	{
		/// <summary>
		/// Target scale.
		/// </summary>
		public float targetLocal;
		public float animationSpeed;
	}

	public struct DestroyOnAnimationEnd :IComponentData
	{ }
}
