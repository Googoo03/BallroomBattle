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
    [SerializeField] private Enemy_Manager boss;

    [SerializeField] private GameObject test_turn_marker;
    [SerializeField] private GameObject timer;

    private float t_timer = 1;

    public void Next_Turn() { next_turn = true; t_timer = 1; }

    void Start()
    {

        //Set up initial params and unlock player
        current_turn = turns.PLAYER;
        test_turn_marker.GetComponent<Text>().text = current_turn.ToString();

        if (current_turn == turns.PLAYER) player.setCan_Input(true);
        //-------------------------------------

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
        if (current_turn == turns.ENEMY)
        {
            player.setCan_Input(false);
            boss.setCan_Input(true);
        }

        //other dispatch player
        if (current_turn == turns.PLAYER)
        {
            player.setCan_Input(true);
            boss.setCan_Input(false);
        }

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
            if (current_turn == turns.PLAYER) player.endTurn(); //ends player turn and applies any damage
            Next_Turn();
        }
    }

    public void applyDamage(int damage) {
        if (current_turn == turns.ENEMY)
        {
            //apply damage to player
            player.dealDamage(damage);
        }
        else {
            //apply damage to enemy
            boss.dealDamage(damage);
        }
    }
}
