using System;
using Unity.Entities;

namespace Input
{
	public class InputActions : IComponentData, IDisposable
	{
		public Actions actions;

		public void Dispose() => actions.Dispose();

		public static implicit operator Actions(InputActions inputAction) => inputAction.actions;
	}
}
