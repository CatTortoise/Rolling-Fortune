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

	public struct RotationAnimation : IComponentData, IEnableableComponent
	{
		/// <summary>
		/// Target rotation in euler (Radians).
		/// </summary>
		public float3 targetLocal;
		public float animationSpeed;
	}

	public struct ScaleAnimation : IComponentData, IEnableableComponent
	{
		/// <summary>
		/// Target scale.
		/// </summary>
		public float targetLocal;
		public float animationSpeed;
	}
}
