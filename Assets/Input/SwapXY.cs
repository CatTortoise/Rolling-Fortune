using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class SwapXY : InputProcessor<Vector2>
{

	#if UNITY_EDITOR
	static SwapXY()
	{
		Initialize();
	}
	#endif

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Initialize()
	{
		InputSystem.RegisterProcessor<SwapXY>();
	}

	public override Vector2 Process(Vector2 value, InputControl control)
	{
		return new(value.y, value.x);
	}
}
