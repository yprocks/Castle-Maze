using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.BarbarianScripts;
using _Scripts.LevelHelpers;

public class DoorAOpen : MonoBehaviour
{
    private bool _collidersDisabled;

    private void Start()
    {
        _collidersDisabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && _collidersDisabled)
            return;
        InGameTextManager.GetInstance().ShowPanel("Hey Barbarian!", "Press 'E' to open the door!");
        if (!Input.GetKeyDown(KeyCode.E)) return;
        InGameTextManager.GetInstance().HidePanel();
        GetComponent<Animator>().SetTrigger("DoorATrigger");
        TrickDoorMaster.Instance.DisableColliders();
        _collidersDisabled = true;
    }
}