using UnityEngine;

public class CameraPause : MonoBehaviour
{
	private RenderTexture _renderTexture;
	private bool _paused = false;

	public void SetPaused(bool paused)
	{
		_paused = paused;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (_renderTexture == null || !CompareSizes(_renderTexture, source))
			_renderTexture = new(source);

		if (_paused)
		{
			Graphics.Blit(_renderTexture, destination);
		}
		else
		{
			Graphics.CopyTexture(source, _renderTexture);
			Graphics.Blit(source, destination);
		}
	}

	private static bool CompareSizes(Texture tex1, Texture tex2)
	{
		return tex1.height == tex2.height & tex1.width == tex2.width;
	}
}