using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Animation
{
	[BurstCompile]
	public partial struct PositionSystem : ISystem
	{
		private EntityQuery _query;

		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate(_query = SystemAPI.QueryBuilder().WithAllRW<PositionAnimation, LocalTransform>().Build());
		}

		public void OnUpdate(ref SystemState state)
		{
			new AnimateJob() { deltaTime = SystemAPI.Time.DeltaTime }.ScheduleParallel(_query);
		}

		[BurstCompile]
		private partial struct AnimateJob : IJobEntity
		{
			public float deltaTime;

			public readonly void Execute(ref PositionAnimation animation, EnabledRefRW<PositionAnimation> enabled, ref LocalTransform transform)
			{
				Extensions.Approach(ref transform.Position, animation.targetLocal, deltaTime * animation.animationSpeed);
				enabled.ValueRW = math.any(transform.Position != animation.targetLocal);
			}
		}
	}

	[BurstCompile]
	public partial struct RotationSystem : ISystem
	{
		private EntityQuery _query;

		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate(_query = SystemAPI.QueryBuilder().WithAllRW<RotationAnimationTarget, LocalTransform>().Build());
		}

		public void OnUpdate(ref SystemState state)
		{
			new AnimateJob() { deltaTime = SystemAPI.Time.DeltaTime }.ScheduleParallel(_query);
		}

		[BurstCompile]
		private partial struct AnimateJob : IJobEntity
		{
			public float deltaTime;

			public readonly void Execute(ref RotationAnimationTarget animation, EnabledRefRW<RotationAnimationTarget> enabled, ref LocalTransform transform)
			{
				var euler = math.Euler(transform.Rotation);
				Extensions.Approach(ref euler, animation.targetLocal, deltaTime * animation.animationSpeed);
				transform.Rotation = quaternion.Euler(euler);
				enabled.ValueRW = math.any(euler != animation.targetLocal);
			}
		}
	}

	[BurstCompile]
	public partial struct RotationSystemConstant : ISystem
	{
		private EntityQuery _query;

		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate(_query = SystemAPI.QueryBuilder().WithAll<RotationAnimationConstant>().WithAllRW<LocalTransform>().Build());
		}

		public void OnUpdate(ref SystemState state)
		{
			new AnimateJob() { deltaTime = SystemAPI.Time.DeltaTime }.ScheduleParallel(_query);
		}

		[BurstCompile]
		private partial struct AnimateJob : IJobEntity
		{
			public float deltaTime;

			public readonly void Execute(in RotationAnimationConstant animation, ref LocalTransform transform)
			{
				var euler = math.Euler(transform.Rotation);
				euler += animation.rotate * deltaTime;
				transform.Rotation = quaternion.Euler(euler);
			}
		}
	}

	[BurstCompile]
	public partial struct ScaleSystem : ISystem
	{
		private EntityQuery _query;

		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate(_query = SystemAPI.QueryBuilder().WithAllRW<ScaleAnimation, LocalTransform>().Build());
		}

		public void OnUpdate(ref SystemState state)
		{
			new AnimateJob() { deltaTime = SystemAPI.Time.DeltaTime }.ScheduleParallel(_query);
		}

		[BurstCompile]
		private partial struct AnimateJob : IJobEntity
		{
			public float deltaTime;

			public readonly void Execute(ref ScaleAnimation animation, EnabledRefRW<ScaleAnimation> enabled, ref LocalTransform transform)
			{
				Extensions.Approach(ref transform.Scale, animation.targetLocal, deltaTime * animation.animationSpeed);
				enabled.ValueRW = transform.Scale != animation.targetLocal;
			}
		}
	}
}
