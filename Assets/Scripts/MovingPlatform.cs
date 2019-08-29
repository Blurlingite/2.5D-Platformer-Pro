using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  // The start and end points of the moving platform as shown by the Point_A and Point_B objects we made in Unity
  [SerializeField]
  private Transform _targetA, _targetB;
  [SerializeField]
  private float _speed = 3.0f;

  private bool _switching = false;
  // Start is called before the first frame update
  void Start()
  {

  }

  // FixedUpdate() is like Update() but it moves based on Physics movements (such as a moving platform) 
  // If we used Update() the platform would move weirdly when the Player jumps on it b/c Update() runs 60 frames/second.
  // FixedUpdate() runs every 0.2 seconds (5 frames/second) so we see the movement as normal based on physics
  void FixedUpdate()
  {

    // Before the platform wasn't moving b/c this is in the Update() which runs every frame. So the platform would try to move to _targetA but also _targetB at the same time. To fix this we put the code in this if else if statement and used a boolean to tell the platform to keep moving after the 1st frame. (W/O it the platform would only move for 1 frame b/c transform.postion == _targetA.position is false b/c after the 1st frame they are not equal anymore)

    if (_switching == false)
    {
      // move to targetB
      // We use MoveTowards to move an object from one position to another.
      // param 1: the position of the object (transform.position)
      // param 2: the position of the target destination(_targetB.position)
      // param 3: The speed to move the object by (_speed * Time.deltaTime)

      transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
    }
    else
    {
      // move to targetA
      transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
    }

    if (transform.position == _targetB.position)
    {
      _switching = true;

    }
    else if (transform.position == _targetA.position)
    {
      _switching = false;
    }


  }



  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      // Assign the parent of the Player(Player is "other") to be this object's transfrom (the Moving Platform), so that way the player moves with the platform
      other.transform.parent = this.transform;
    }

  }

  // called when something is no longer on this object(Player jumps off moving platform). If we didn't use this, the Player would still be parented to the Moving Platform, so we would still move with it even if we were off of it
  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      // by setting the parent of Player to null, Player is it's own independent object again so we will not move with the moving platform
      other.transform.parent = null;
    }

  }
}
