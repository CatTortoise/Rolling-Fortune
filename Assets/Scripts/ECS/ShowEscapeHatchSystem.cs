using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct EnableEscapeHatchSystem : ISystem
{
	EntityQuery _hatchQuery, _gemQuery;

	[BurstCompile]
	public void OnCreate(ref SystemState state)
	{
		_gemQuery = SystemAPI.QueryBuilder().WithAll<Gem>().Build();
		state.RequireForUpdate(_hatchQuery = SystemAPI.QueryBuilder().WithAll<EscapeHatch, Disabled>().Build());
	}

	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		if (!_gemQuery.IsEmpty)
			return;
		new EnableHatchJob() {
			ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged)
		}.Schedule(_hatchQuery);
	}

	[BurstCompile]
	private partial struct EnableHatchJob : IJobEntity
	{
		public EntityCommandBuffer ecb;

		public void Execute(Entity entity)
		{
			ecb.SetEnabled(entity, true);
		}
	}
}
