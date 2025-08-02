using UnityEngine;
using Unity.Entities;
using Unity.Collections;

namespace Input
{
	[UpdateInGroup(typeof(InitializationSystemGroup))]
	public partial class InputSystem : SystemBase
	{
		private Actions _actions;

		protected override void OnCreate()
		{
			CreateSingletonInputEntity();
			_actions = new();

			void CreateSingletonInputEntity()
			{
				if (SystemAPI.HasSingleton<InputComponent>())
					return;
				NativeArray<ComponentType> types = new(1, Allocator.Temp);
				types[0] = new(typeof(InputComponent), ComponentType.AccessMode.ReadWrite);
				EntityManager.CreateEntity(types);
				types.Dispose();
			}
		}

		protected override void OnDestroy() => _actions.Dispose();

		protected override void OnStartRunning() => _actions.Enable();

		protected override void OnStopRunning() => _actions.Disable();

		protected override void OnUpdate()
		{
			var player = _actions.Player;
			SetInput(SystemAPI.GetSingletonRW<InputComponent>());

			void SetInput(RefRW<InputComponent> input)
			{
				var tilt = player.Tilt;
				if (tilt.enabled)
					input.ValueRW.tiltInput = tilt.ReadValue<Vector2>();
				var resetTilt = player.ResetTilt;
				if (resetTilt.enabled)
					input.ValueRW.resetTilt = resetTilt.IsPressed();
			}
		}
	}
}