using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainerSlotChessUI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool open;
    public ItemContainerSlot ChessSlot;
    //
    private void OnMouseDown()
    {
        if (!open)
        {
            ChessSlot.on();
        }
        open = true;
    }
    private void Start()
    {
        ChessSlot.ItemContainerStart();
        ChessSlot.off();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChessSlot.off();
            open = false;
        }
    }
}