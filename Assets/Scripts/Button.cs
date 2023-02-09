using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Choose the function to call when the button is pressed in the editor
    public enum ButtonFunction {
        Initiate,
        Set15,
        Set30,
        Set50
    }
    [SerializeField] private ButtonFunction buttonFunction;

    // Reference to the game mode
    [SerializeField] private GameMode gameMode;
    
    public void PressButton() {
        // Call the function according to the button function
        switch (buttonFunction) {
            case ButtonFunction.Initiate:
                gameMode.StartGame();
                break;
            case ButtonFunction.Set15:
                gameMode.SetTargetCount(15);
                break;
            case ButtonFunction.Set30:
                gameMode.SetTargetCount(30);
                break;
            case ButtonFunction.Set50:
                gameMode.SetTargetCount(50);
                break;
        }

        // Brefly change the color of the button
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor() {
        // Change the color to green
        GetComponent<Renderer>().material.color = Color.green;

        // Wait for a bit
        yield return new WaitForSeconds(.3f);

        // Change the color back to white
        GetComponent<Renderer>().material.color = Color.red;
    }
}
