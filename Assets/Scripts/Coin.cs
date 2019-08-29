using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{



  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Player _player = other.GetComponent<Player>();
      if (_player != null)
      {
        _player.AddCoins();
      }
      Destroy(this.gameObject);

    }

  }
}
