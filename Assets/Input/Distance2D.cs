using UnityEditor;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[DisplayStringFormat("{activate}+{input1}+{input2}")]
public class Distance2D : InputBindingComposite<float>
{
	[InputControl(layout = "Button")]
	public int activate1;
	[InputControl(layout = "Button")]
	public int activate2;
	[InputControl(layout = "Vector2")]
	public int input1;
	[InputControl(layout = "Vector2")]
	public int input2;

	public override float ReadValue(ref InputBindingCompositeContext context)
	{
		bool active1 = context.ReadValueAsButton(activate1);
		bool active2 = context.ReadValueAsButton(activate1);
		Vector2 position1 = context.ReadValue<Vector2, Vector2MagnitudeComparer>(input1);
		Vector2 position2 = context.ReadValue<Vector2, Vector2MagnitudeComparer>(input2);
		return active1 & active2 ? Vector2.Distance(position1, position2) : 0;
	}

	public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
	{
		bool active1 = context.ReadValueAsButton(activate1);
		bool active2 = context.ReadValueAsButton(activate2);
		Debug.Log($"Active1: {active1}\nActive2: {active2}\nMag1: {context.EvaluateMagnitude(input1)}\nMag2: {context.EvaluateMagnitude(input2)}");
		return context.EvaluateMagnitude(activate1) * context.EvaluateMagnitude(activate2);
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