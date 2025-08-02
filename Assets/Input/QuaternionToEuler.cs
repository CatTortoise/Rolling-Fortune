using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[DisplayStringFormat("{quaternionInput}")]
public class QuaternionToEuler : InputBindingComposite<Vector3>
{
	[InputControl(layout = "Quaternion")]
	public int quaternionInput;

	public override Vector3 ReadValue(ref InputBindingCompositeContext context)
	{
		var euler = context.ReadValue<Quaternion, QuaternionEulerComparer>(quaternionInput);
		return euler.eulerAngles;
	}

	static QuaternionToEuler() => Init();

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Init() => InputSystem.RegisterBindingComposite<QuaternionToEuler>();

	internal struct QuaternionEulerComparer : IComparer<Quaternion>
	{
		public readonly int Compare(Quaternion x, Quaternion y)
		{
			var lenx = x.eulerAngles.sqrMagnitude;
			var leny = y.eulerAngles.sqrMagnitude;

			if (lenx < leny)
				return -1;
			if (lenx > leny)
				return 1;
			return 0;
		}
	}
}