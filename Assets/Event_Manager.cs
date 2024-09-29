using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private enum turns {
        ENEMY,
        PLAYER
    }
    private turns current_turn = turns.PLAYER;
    [SerializeField] private bool next_turn;

    [SerializeField] private Player_Input_Manager player;
    [SerializeField] private GameObject test_turn_marker;
    [SerializeField] private GameObject timer;

    private float t_timer = 1;

    public void Next_Turn() { next_turn = true; t_timer = 1; }

    void Start()
    {
        next_turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer Protocol
        timerProtocol();


        if (!next_turn) return;

        current_turn = current_turn == turns.ENEMY ? turns.PLAYER : turns.ENEMY; //switch the turns

        //if the enemy's turn, dispatch the enemy
        if (current_turn == turns.ENEMY) player.setCan_Input(false);

        //other dispatch player
        if (current_turn == turns.PLAYER) player.setCan_Input(true);

        test_turn_marker.GetComponent<Text>().text = current_turn.ToString();

        //set the next_turn to false
        next_turn = false; 
    }

    private void timerProtocol() {
        t_timer -= 0.1f * Time.deltaTime;
        timer.GetComponent<Slider>().value = t_timer;

        if (t_timer < 0)
        {
            //t_timer = 1;
            Next_Turn();
        }
    }
}
