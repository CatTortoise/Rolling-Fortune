using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class ClampMagnitude2D: InputProcessor<Vector2>
{
	public float _Magnitude;

	#if UNITY_EDITOR
	static ClampMagnitude2D()
	{
		Initialize();
	}
#endif

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Initialize()
	{
		InputSystem.RegisterProcessor<ClampMagnitude2D>();
	}

	public override Vector2 Process(Vector2 value, InputControl control)
	{
		return value.magnitude > _Magnitude ? value.normalized * _Magnitude : value;
	}
}
