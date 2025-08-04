using Unity.Mathematics;

public static class Extensions
{
	public static void Approach(ref float current, float target, float maxDelta)
	{
		var diff = target - current;
		maxDelta = math.min(math.abs(diff), maxDelta);
		diff = math.clamp(diff, -maxDelta, maxDelta);
		current += diff;
	}

	public static void Approach(ref float2 current, float2 target, float2 maxDelta)
	{
		var diff = target - current;
		maxDelta = math.min(math.abs(diff), maxDelta);
		diff = math.clamp(diff, -maxDelta, maxDelta);
		current += diff;
	}

	public static void Approach(ref float3 current, float3 target, float3 maxDelta)
	{
		var diff = target - current;
		maxDelta = math.min(math.abs(diff), maxDelta);
		diff = math.clamp(diff, -maxDelta, maxDelta);
		current += diff;
	}

	public static void Approach(ref float4 current, float4 target, float4 maxDelta)
	{
		var diff = target - current;
		maxDelta = math.min(math.abs(diff), maxDelta);
		diff = math.clamp(diff, -maxDelta, maxDelta);
		current += diff;
	}
}
