using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

namespace Input
{
	[BurstCompile]
	public partial struct RotateToInputSystem : ISystem
	{
		private EntityQuery _query;

		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate(_query = SystemAPI.QueryBuilder().WithAll<RotateToInputComponent>().WithPresentRW<Animation.RotationAnimationTarget>().WithAllRW<LocalTransform>().Build());
		}

		public void OnUpdate(ref SystemState state)
		{
			if (SystemAPI.TryGetSingleton<InputComponent>(out var input))
				new SetInputRotationJob() { tilt = input.tiltInput }.ScheduleParallel(_query);
		}

		[BurstCompile]
		private partial struct SetInputRotationJob : IJobEntity
		{
			public float2 tilt;

			public readonly void Execute(in RotateToInputComponent rotate, EnabledRefRW<Animation.RotationAnimationTarget> resetting, ref LocalTransform transform)
			{
				if (resetting.ValueRW = math.all(tilt == float2.zero))
					return;
				var newTilt = tilt * rotate.rotationScale;
				transform.Rotation = quaternion.Euler(new(newTilt.x, 0f, newTilt.y));
			}
		}
	}
}
