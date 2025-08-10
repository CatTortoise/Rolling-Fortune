using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using static Physics.Extensions;
using Animation;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
[BurstCompile]
public partial struct PlayerEscapeSystem : ISystem
{
	private ComponentLookup<LocalTransform> _transformLookup;
	private ComponentLookup<PositionAnimation> _animationLookup;

	[BurstCompile]
	public void OnCreate(ref SystemState state)
	{
		_transformLookup = state.GetComponentLookup<LocalTransform>(true);
		_animationLookup = state.GetComponentLookup<PositionAnimation>();
	}

	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		_transformLookup.Update(ref state);
		_animationLookup.Update(ref state);
		state.Dependency = new PlayerEscapeJob()
		{
			transformLookup = _transformLookup,
			animationLookup = _animationLookup,
			ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged),
			world = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld
		}.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
	}

	[BurstCompile]
	private struct PlayerEscapeJob : ITriggerEventsJob
	{
		[ReadOnly] public ComponentLookup<LocalTransform> transformLookup;
		public ComponentLookup<PositionAnimation> animationLookup;
		public EntityCommandBuffer ecb;
		[ReadOnly] public PhysicsWorld world;

		public void Execute(TriggerEvent triggerEvent)
		{
			var filterA = world.GetCollisionFilter(triggerEvent.BodyIndexA);
			var filterB = world.GetCollisionFilter(triggerEvent.BodyIndexB);
			var playerEntity = triggerEvent.EntityA;
			var hatchEntity = triggerEvent.EntityB;
			if (BelongsTo(filterA, filterB, PLAYER_TAG, HATCH_TAG, ref playerEntity, ref hatchEntity))
			{
				ecb.RemoveComponent<PhysicsCollider>(hatchEntity);
				ecb.RemoveComponent<PhysicsCollider>(playerEntity);
				ecb.RemoveComponent<PhysicsWorldIndex>(playerEntity);
				ecb.RemoveComponent<PhysicsVelocity>(playerEntity);
				ecb.RemoveComponent<PhysicsMass>(playerEntity);
				ecb.RemoveComponent<PhysicsDamping>(playerEntity);
				ecb.RemoveComponent<PhysicsCustomTags>(playerEntity);
				ecb.AddComponent<Escaped>(playerEntity);
				animationLookup.GetEnabledRefRW<PositionAnimation>(playerEntity).ValueRW = true;
				animationLookup.GetRefRW(playerEntity).ValueRW.targetLocal = transformLookup.GetRefRO(hatchEntity).ValueRO.Position;
			}
		}
	}
}
