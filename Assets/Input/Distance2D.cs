using UnityEditor;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[DisplayStringFormat("{input1}+{input2}")]
public class Distance2D : InputBindingComposite<float>
{
	[InputControl(layout = "Vector2")]
	public int input1;
	[InputControl(layout = "Vector2")]
	public int input2;

	public override float ReadValue(ref InputBindingCompositeContext context)
	{
		Vector2 position1 = context.ReadValue<Vector2, Vector2MagnitudeComparer>(input1);
		Vector2 position2 = context.ReadValue<Vector2, Vector2MagnitudeComparer>(input2);
		return Vector2.Distance(position1, position2);
	}

	public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
	{
		return context.EvaluateMagnitude(input1) * context.EvaluateMagnitude(input2);
	}

	static Distance2D()
	{
		Init();
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Init()
	{
		InputSystem.RegisterBindingComposite<Distance2D>();
	}
}