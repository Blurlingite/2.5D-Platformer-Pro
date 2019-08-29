using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
  // handle to the Character Controller component on the Player object
  private CharacterController _controller;

  private UIManager _uiManager;
  [SerializeField]
  private float _speed = 5.0f;
  [SerializeField]
  private float _gravity = 1.3f;
  [SerializeField]
  private float _jumpHeight = 20.0f;
  // Used to store the current y value of velocity
  private float _yVelocity;
  private bool _canDoubleJump = false;
  [SerializeField]
  private int _coins = 0;
  [SerializeField]
  private int _lives = 3;
  // Start is called before the first frame update
  void Start()
  {
    _controller = GetComponent<CharacterController>();
    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    if (_uiManager == null)
    {
      Debug.Log("UI Manager is NULL");
    }

    _uiManager.UpdateLivesDisplay(_lives);
  }

  // Update is called once per frame
  void Update()
  {

    float horizontalInput = Input.GetAxis("Horizontal");

    Vector3 direction = new Vector3(horizontalInput, 0, 0);

    Vector3 velocity = direction * _speed;

    // isGrounded is a boolean on the CharacterController that can tell if the object this script is attached to (Player) is on something (like an object). If it's false, it means the player is in the air (not on an object). If it's true it's on the ground (on an object)
    if (_controller.isGrounded == true)
    {
      // If I hit the Space bar, jump by setting velocity's y to jump height
      if (Input.GetKeyDown(KeyCode.Space))
      {
        // b/c Update() is called frame by frame, using "velocity.y" won't work b/c as soon as the next frame starts, we set the velocity's y to 0 (in Vector3 direction = new Vector3(horizontalInput, 0, 0);). So we use this variable to store the current y and then we will set velocity.t to that variable
        _yVelocity = _jumpHeight;
        _canDoubleJump = true;
      }
    }
    else
    {

      // enable double jump if they press the Space key when they've jumped once. We can't do this in the above if statement b/c we can only execute that if we were previously on the ground
      if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump == true)
      {
        _yVelocity += _jumpHeight;
        _canDoubleJump = false;
      }

      // If we manipulate the y on the velocity we can make the object move down to simulate gravity pushing it down
      _yVelocity -= _gravity;

    }

    velocity.y = _yVelocity;
    // We multiply velocity by Time.deltaTime so we don't fly off the screen when we move
    _controller.Move(velocity * Time.deltaTime);

  }

  public void Damage()
  {
    _lives--;
    _uiManager.UpdateLivesDisplay(_lives);

    // If out of lives, restart the environment (which we need SceneManagement for)
    if (_lives < 1)
    {
      SceneManager.LoadScene(0);
    }

  }

  public void AddCoins()
  {
    _coins++;
    _uiManager.UpdateCoinDisplay(_coins);
  }
}
