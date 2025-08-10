using Unity.Entities;

namespace Input
{
	public struct RotateToInputComponent : IComponentData
	{
		/// <summary>
		/// Rotation scale in radians.
		/// </summary>
		public float rotationScale;

		public static implicit operator RotateToInputComponent(float scale) => new() { rotationScale = scale };
		public static implicit operator float(RotateToInputComponent scale) => scale.rotationScale;
	}
}