using UnityEngine;
using Unity.Entities;

namespace Input
{
	[UpdateInGroup(typeof(InitializationSystemGroup))]
	public partial class InputSystem : SystemBase
	{
		private Actions Actions => SystemAPI.ManagedAPI.GetSingleton<Actions>();

		protected override void OnCreate()
		{
			if (!SystemAPI.ManagedAPI.HasComponent<Actions>(SystemHandle))
				EntityManager.AddComponentObject(SystemHandle, new Actions());
			if (!SystemAPI.HasComponent<InputComponent>(SystemHandle))
				EntityManager.AddComponent<InputComponent>(SystemHandle);
		}

		protected override void OnStartRunning() => Actions.Enable();

		protected override void OnStopRunning() => Actions.Disable();

		protected override void OnUpdate()
		{
			var player = Actions.Player;
			SetInput(SystemAPI.GetComponentRW<InputComponent>(SystemHandle));

			void SetInput(RefRW<InputComponent> input)
			{
				var tilt = player.Tilt;
				if (tilt.enabled)
					input.ValueRW.tiltInput = tilt.ReadValue<Vector2>();
				var resetTilt = player.ResetTilt;
				if (resetTilt.enabled)
					input.ValueRW.resetTilt = resetTilt.IsPressed() || tilt.WasCompletedThisFrame();
			}
		}
	}
}