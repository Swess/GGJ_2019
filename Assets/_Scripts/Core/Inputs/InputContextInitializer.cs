using Core.Inputs;
using UnityEngine;


/// <summary>
/// Add this component to a GameObject present at load of the scene
/// to make the context apply on Start.
/// EX: The Camera
/// </summary>
public class InputContextInitializer : MonoBehaviour {

    public ContextualInputsState inputContext;

    void Start() {
        if(inputContext != null)
            Core.GameController.Instance.actionsMapsHelper.ApplyContext(inputContext);
    }

}