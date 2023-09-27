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
		if (_renderTexture == null || _renderTexture.dimension != source.dimension)
			_renderTexture = new(source.width, source.height, source.depth);

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
}