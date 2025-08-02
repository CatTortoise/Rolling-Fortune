using Unity.Entities;
using Unity.Mathematics;

namespace Input
{
	public struct InputComponent : IComponentData
	{
		public float2 tiltInput;
		public bool resetTilt;
	}
}