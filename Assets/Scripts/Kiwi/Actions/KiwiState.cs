using UnityEngine;
using System.Collections;

public class KiwiState : Singleton<KiwiState> {
    //Data members.

    private Vector2 _movement;

    private bool _isGrounded;
    private bool _wasGrounded;
    private bool _isJumping;
    private bool _isFalling;
    private bool _airCollision;
    private bool _isIdle;
    private bool _isWalking;
    private bool _isSprinting;
    private bool _isMoving;
    private bool _isScared;
    private bool _isPanicing;
    private bool _isDigging;
    private bool _isPecking;
    private bool _isDoingAction;
    private bool _isDying;
    private bool _isDead;

    public bool Grounded {
        get { return _isGrounded; }
        set { _isGrounded = value; }
    }
    public bool WasGrounded {
        get { return _wasGrounded; }
        set { _wasGrounded = value; }
    }
    public bool Jumping {
        get { return _isJumping; }
        set { _isJumping = value; }
    }
    public bool Falling {
        get { return _isFalling; }
        set { _isFalling = value; }
    }
    public bool AirCollision {
        get { return _airCollision; }
        set { _airCollision = value; }
    }
    public bool Idle {
        get { return _isIdle; }
        set { _isIdle = value; }
    }
    public bool Walking {
        get { return _isWalking; }
        set { _isWalking = value; }
    }
    public bool Sprinting {
        get { return _isSprinting; }
        set { _isSprinting = value; }
    }
    public bool Moving {
        get { return _isMoving; }
        set { _isMoving = value; }
    }
    public bool Scared {
        get { return _isScared; }
        set { _isScared = value; }
    }
    public bool Panicing {
        get { return _isPanicing; }
        set { _isPanicing = value; }
    }
    public bool Digging {
        get { return _isDigging; }
        set { _isDigging = value; }
    }
    public bool Pecking {
        get { return _isPecking; }
        set { _isPecking = value; }
    }
    public bool DoingAction {
        get { return _isDoingAction; }
        set { _isDoingAction = value; }
    }
    public bool Dying {
        get { return _isDying; }
        set { _isDying = value; }
    }
    public bool Dead {
        get { return _isDead; }
        set { _isDead = value; }
    }
}
