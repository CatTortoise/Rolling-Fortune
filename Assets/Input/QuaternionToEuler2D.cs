using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[DisplayStringFormat("{quaternionInput}")]
public class QuaternionToEuler2D : InputBindingComposite<Vector2>
{
	[InputControl(layout = "Quaternion")]
	public int quaternionInput;

	public override Vector2 ReadValue(ref InputBindingCompositeContext context)
	{
		var euler = context.ReadValue<Quaternion, QuaternionToEuler.QuaternionEulerComparer>(quaternionInput);
		return euler.eulerAngles;
	}

	static QuaternionToEuler2D() => Init();

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Init() => InputSystem.RegisterBindingComposite<QuaternionToEuler2D>();
}