using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Animation;
using static Physics.Extensions;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
[BurstCompile]
public partial struct GemCollectionSystem : ISystem
{
	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		state.Dependency = new CollectGemsJob()
		{
			ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged),
			world = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld
		}.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
	}

	[BurstCompile]
	private struct CollectGemsJob : ITriggerEventsJob
	{
		public EntityCommandBuffer ecb;
		[ReadOnly] public PhysicsWorld world;

		public void Execute(TriggerEvent triggerEvent)
		{
			var filterA = world.GetCollisionFilter(triggerEvent.BodyIndexA);
			var filterB = world.GetCollisionFilter(triggerEvent.BodyIndexB);
			var playerEntity = triggerEvent.EntityA;
			var gemEntity = triggerEvent.EntityB;
			if (BelongsTo(filterA, filterB, PLAYER_TAG, GEM_TAG, ref playerEntity, ref gemEntity))
			{
				ecb.RemoveComponent<PhysicsCollider>(gemEntity);
				ecb.SetComponentEnabled<ScaleAnimation>(gemEntity, true);
				ecb.AddComponent<DestroyOnAnimationEnd>(gemEntity);
			}
		}
	}
}
