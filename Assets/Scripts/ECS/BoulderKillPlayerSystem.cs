using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using static Physics.Extensions;
using Animation;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
[BurstCompile]
public partial struct BoulderKillPlayerSystem : ISystem
{
	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		state.Dependency = new PlayerEscapeJob()
		{
			ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged),
			world = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld
		}.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
	}

	[BurstCompile]
	private struct PlayerEscapeJob : ICollisionEventsJob
	{
		public EntityCommandBuffer ecb;
		[ReadOnly] public PhysicsWorld world;

		public void Execute(CollisionEvent collisionEvent)
		{
			var filterA = world.GetCollisionFilter(collisionEvent.BodyIndexA);
			var filterB = world.GetCollisionFilter(collisionEvent.BodyIndexB);
			var playerEntity = collisionEvent.EntityA;
			var boulderEntity = collisionEvent.EntityB;
			if (BelongsTo(filterA, filterB, PLAYER_TAG, BOULDER_TAG, ref playerEntity, ref boulderEntity))
			{
				ecb.RemoveComponent<PhysicsCollider>(playerEntity);
				ecb.RemoveComponent<PhysicsWorldIndex>(playerEntity);
				ecb.RemoveComponent<PhysicsVelocity>(playerEntity);
				ecb.RemoveComponent<PhysicsMass>(playerEntity);
				ecb.RemoveComponent<PhysicsDamping>(playerEntity);
				ecb.RemoveComponent<PhysicsCustomTags>(playerEntity);
				ecb.AddComponent<Died>(playerEntity);
				ecb.SetComponentEnabled<ScaleAnimation>(playerEntity, true);
			}
		}
	}
}
