using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTrashController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Roomba")
            RoombaController.Instance.EmptyTrash();
    }
}
