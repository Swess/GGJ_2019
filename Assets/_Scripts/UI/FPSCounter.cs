using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utils {
	public class FPSCounter : MonoBehaviour {
		private Text _textComponent;
		private int _liveResult;

		void Start (){
			_textComponent = GetComponent<Text>();
			StartCoroutine(DisplayUpdate());
		}

		void Update () {
			_liveResult = (int)(1 / Time.deltaTime);
		}

		IEnumerator DisplayUpdate(){
			while (true) {
				yield return new WaitForSeconds(0.2f);
				_textComponent.text = _liveResult + " FPS";
			}
		}

	}
}
