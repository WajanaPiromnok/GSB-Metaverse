using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using System;

namespace MarksAssets.LaunchURLWebGL {
	[DisallowMultipleComponent]
	public class LaunchURLWebGL:MonoBehaviour {
		private static LaunchURLWebGL m_Instance = null;

		public static LaunchURLWebGL instance { get => m_Instance; }

		private void Awake() {
			if (m_Instance != null && m_Instance != this) {
				Destroy(this.gameObject);
			} else {
				m_Instance = this;
			}

		}

		[DllImport("__Internal", EntryPoint="LaunchURLWebGL_launchURL")]
		public static extern void launchURL(string url = "", string windowName = "_blank", string windowFeatures = "");

		public void launchURLSelf(string url = "") {
			launchURL(url, "_self");
		}
		
		public void launchURLBlank(string url = "") {
			launchURL(url);
		}

        /*private void OnApplicationFocus(bool v)
        {
            throw new NotImplementedException();
        }*/

        public void OpenURL(string url) {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                launchURL(url);
            else
                Application.OpenURL(url);
        }


		public void OpenURLBlank(string url = "")
		{
            Application.ExternalEval("window.open('" + url + "','_blank')");

		}

	}
}