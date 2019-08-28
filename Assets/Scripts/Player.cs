using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // handle to the Character Controller component on the Player object
  private CharacterController _controller;
  [SerializeField]
  private float _speed = 5.0f;
  [SerializeField]
  private float _gravity = 1.0f;
  // Start is called before the first frame update
  void Start()
  {
    _controller = GetComponent<CharacterController>();

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
      // do nothing, maybe jump later
    }
    else
    {
      // If we manipulate the y on the velocity we can make the object move down to simulate gravity pushing it down
      velocity.y -= _gravity;

    }

    // We multiply velocity by Time.deltaTime so we don't fly off the screen when we move
    _controller.Move(velocity * Time.deltaTime);

  }
}
