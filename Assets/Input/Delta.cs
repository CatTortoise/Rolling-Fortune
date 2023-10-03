using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class Delta: InputProcessor<float>
{
	private float _previousValue = 0;

	#if UNITY_EDITOR
	static Delta()
	{
		Initialize();
	}
	#endif

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Initialize()
	{
		InputSystem.RegisterProcessor<Delta>();
	}

	public override float Process(float value, InputControl control)
	{
		var retVal = value - _previousValue;
		_previousValue = value;
		return retVal;
	}
}
