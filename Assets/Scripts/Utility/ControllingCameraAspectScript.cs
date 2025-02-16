using UnityEngine;
using System.Collections;

public class ControllingCameraAspectScript : MonoBehaviour
{
	//21:9
	public const float MAX_RATIO = 2.3333333f;//1.77777777778f;//
	public const float MIN_RATIO = 1.33333333333f;//1.77777777778f;//
	float m_LastRadio;

	// Use this for initialization
	void Awake()
	{
		vSetRadio();
	}

	void vSetRadio()
	{
		if (((float)Screen.width / (float)Screen.height) >= MIN_RATIO && ((float)Screen.width / (float)Screen.height) <= MAX_RATIO)
		{
			vSetCameraNormal();
			return;
		}

		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = ((float)Screen.width / (float)Screen.height) < MIN_RATIO ? MIN_RATIO : MAX_RATIO;//16.0f / 9.0f;//MAX_RATIO;//

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}

	void vSetCameraNormal() 
	{
		Camera camera = GetComponent<Camera>();

		camera.rect = new Rect(0, 0, 1, 1);
	}

	void vSetPortraitMode()
	{
		float targetaspect = 4.0f / 3.0f;//16f / 9f;//

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}

	private void Update()
	{
		float radio = (float)Screen.width / (float)Screen.height;
		if (!Mathf.Approximately( radio ,m_LastRadio))
		{
			//min at 4:3
			if (radio <= 1f)
				vSetPortraitMode();
			else
				vSetRadio();
		}
		m_LastRadio = radio;
	}
}
