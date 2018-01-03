using UnityEngine;
using System.Collections;

public class KiwiInput : MonoBehaviour {

    private bool _peckButton;
    private bool _digButton;
    private bool _actionButton;
    private bool _jumpButton;
    private float _xMovement;
    
    public float XMovement {
        
    }

    // Update is called once per frame
    void Update() {
        CaptureInput();
    }

    private void CaptureInput() {
       _xMovement    = Input.GetAxis("Horizontal");
       _jumpButton   = Input.GetButtonDown("Jump");
       _actionButton = Input.GetButton("Action");
       _peckButton   = Input.GetButtonDown("Peck");
       _digButton    = Input.GetButtonDown("Dig");
    }
}
