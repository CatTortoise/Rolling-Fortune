using UnityEditor;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[DisplayStringFormat("{eulerInput}")]
public class EulerToQuaternion : InputBindingComposite<Quaternion>
{
	[InputControl(layout = "Vector3")]
	public int eulerInput;

	public override Quaternion ReadValue(ref InputBindingCompositeContext context)
	{
		var euler = context.ReadValue<Vector3, Vector3MagnitudeComparer>(eulerInput);
		return Quaternion.Euler(euler * 180f);
	}

	public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
	{
		var euler = context.ReadValue<Vector3, Vector3MagnitudeComparer>(eulerInput);
		return Mathf.Max(Mathf.Max(euler.x, euler.y), euler.z);
	}

	static EulerToQuaternion() => Init();

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Init() => InputSystem.RegisterBindingComposite<EulerToQuaternion>();
}