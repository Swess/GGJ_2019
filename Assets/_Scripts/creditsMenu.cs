using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsMenu : MonoBehaviour
{
	public Animator anim;
	public Rewired.Player controller;

	void Start(){
		controller = Core.GameController.Instance.actionsMapsHelper.Player1Inputs;
	}
    // Update is called once per frame
    void Update()
    {
		if (controller.GetButtonDown(RewiredConsts.Action.UICancel)){
			CreditsOff();
		}
    }

	public void Credits (){
		anim.SetBool ("CreditsIN", true);
	}
	public void CreditsOff (){
		anim.SetBool ("CreditsIN", false);
	}
	public void StartGame(){
		Core.GameController.Instance.SceneController.FadeAndLoadScene("JF_MainLevel");
	}
	public void Quit(){
		Application.Quit();
	}
}
