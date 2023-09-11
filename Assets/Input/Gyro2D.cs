using UnityEditor;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[DisplayStringFormat("{gyroInput}")]
public class Gyro2D : InputBindingComposite<Vector2>
{
	[InputControl(layout = "Vector3")]
	public int gyroInput;

	public override Vector2 ReadValue(ref InputBindingCompositeContext context)
	{
		Vector3 gyroInputValue = context.ReadValue<Vector3, Vector3MagnitudeComparer>(gyroInput);
		return (Vector2)gyroInputValue; // Discard 'z' value
	}

	public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
	{
		Vector3 gyroInputValue = context.ReadValue<Vector3, Vector3MagnitudeComparer>(gyroInput);
		return gyroInputValue.magnitude;
	}

	static Gyro2D()
	{
		InputSystem.RegisterBindingComposite<Gyro2D>();
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Init() { }
}