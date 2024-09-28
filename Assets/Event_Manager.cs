using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private enum turns {
        ENEMY,
        PLAYER
    }
    private turns current_turn = turns.PLAYER;
    private bool next_turn;

    public void Next_Turn() { next_turn = true; }

    void Start()
    {
        next_turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!next_turn) return;

        //This is where all the 
    }
}
