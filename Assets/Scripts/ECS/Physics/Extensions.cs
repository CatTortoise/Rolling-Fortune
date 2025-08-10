using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace Physics
{
	[BurstCompile]
	public static class Extensions
	{
		public const uint	BOARD_TAG = 1 << 0,
							PLAYER_TAG = 1 << 1,
							BOULDER_TAG = 1 << 2,
							GEM_TAG = 1 << 3,
							HATCH_TAG = 1 << 4;

		[BurstCompile]
		public static bool BelongsTo(in CollisionFilter filterA, in CollisionFilter filterB, uint tagA, uint tagB, ref Entity entityA, ref Entity entityB)
		{
			if (filterA.Contains(tagA) && filterB.Contains(tagB))
				return true;
			if (filterA.Contains(tagB) && filterB.Contains(tagA))
			{
				(entityA, entityB) = (entityB, entityA);
				return true;
			}
			return false;
		}

		public static bool Contains(this CollisionFilter filter, uint toCheck) => (filter.BelongsTo & toCheck) != 0;

		public static bool Contains(this uint filter, uint toCheck) => (filter & toCheck) != 0;
	}
}
