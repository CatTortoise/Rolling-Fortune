using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class Accumulate2D : InputProcessor<Vector2>
{
	private Vector2 accumulated = Vector2.zero;

	#if UNITY_EDITOR
	static Accumulate2D()
	{
		Initialize();
	}
	#endif

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		InputSystem.RegisterProcessor<Accumulate2D>();
	}

	public override Vector2 Process(Vector2 value, InputControl control)
	{
		accumulated += value;
		return accumulated;
	}
}
