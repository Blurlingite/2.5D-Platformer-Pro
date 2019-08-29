using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
  [SerializeField]
  private GameObject _respawnPoint;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {

      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.Damage();
      }

      // Player could not respawn after falling off map b/c it fell too quickly. We can fix this by setting the player's velocity to 0 when it collides with Dead_Zone or we could disable the CharcterController (which has the velocity on it). If you disable the CharacterController, don't forget to re-enable it after the player respawns or else you can't move after respawning
      CharacterController cc = other.GetComponent<CharacterController>();

      if (cc != null)
      {
        cc.enabled = false;
      }
      // Set the player's position to the respawn point's
      other.transform.position = _respawnPoint.transform.position;

      // this would re-enable the cc but it's better to use a coroutine
      // if (cc != null)
      // {
      //   cc.enabled = true;
      // }

      StartCoroutine(CCEnableRoutine(cc));
    }
  }

  IEnumerator CCEnableRoutine(CharacterController controller)
  {
    yield return new WaitForSeconds(0.5f);
    controller.enabled = true;
  }
}
