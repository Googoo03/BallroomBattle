using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Input_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private const int max_arrows = 5;

    //private Color[] colors = new Color[4] { Color.red,Color.blue,Color.green,Color.yellow };

    [SerializeField] private int[] player1_progress = new int[3]; //signifies the progress in the attacks
    [SerializeField] private int[] player2_progress = new int[3];

    private GameObject player1_abilities;
    private GameObject player2_abilities;

    [SerializeField] private bool player1_done;
    [SerializeField] private bool player2_done; //these signify if the players have finished their inputs

    private bool can_input;

    [SerializeField] private Event_Manager event_manager;

    [SerializeField] private int damageDealt;

    [SerializeField] private float health = 200;

    [SerializeField] private bool timeUp;

    [SerializeField] private bool initialize = true;


    //At the moment, there are 6 entries, 0,1,2 correspond to player 1's attack,defend,special respectively. The latter are palyer 2's
    private List<Arrow[]> arrow_list = new List<Arrow[]>();

    private void Awake()
    {
        player1_abilities = GameObject.FindGameObjectWithTag("Player_1");
        player2_abilities = GameObject.FindGameObjectWithTag("Player_2");

        player1_done = false;
        player2_done = false;
        timeUp = false;


        //Get the arrows in player 1
        for (int i = 0; i < 3; ++i) {

            Arrow[] arrows = new Arrow[max_arrows];
            for (int j = 0; j < max_arrows; ++j) {
                arrows[j] = player1_abilities.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Arrow>();
            }
            arrow_list.Add(arrows);
        }

        //Get the arrows in player 2
        for (int i = 0; i < 3; ++i)
        {
            Arrow[] arrows = new Arrow[max_arrows];
            for (int j = 0; j < max_arrows; ++j)
            {
                arrows[j] = player2_abilities.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Arrow>();
            }
            arrow_list.Add(arrows);
        }
    }

    public void generateNewArrows() {
        for (int i = 0; i < 6; ++i) {
            for (int j = 0; j < max_arrows; ++j) {
                int rand = UnityEngine.Random.Range(0, 4); //generate number 1-4
                float rot = 0;
                switch (rand)
                {
                    case 0:
                        rot = 0;
                        break;
                    case 1:
                        rot = 90;
                        break;
                    case 2:
                        rot = 180;
                        break;
                    case 3:
                        rot = 270;
                        break;
                    default:
                        break; //shouldn't ever reach here
                }
                Arrow arrow = arrow_list[i][j];

                arrow.setRotation(rot);
                arrow.setColor(Color.red);
                arrow.gameObject.GetComponent<Animator>().SetTrigger("Reset");
            }
        }
    }

    private void Start()
    {
        generateNewArrows();
        initialize = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!can_input) return;

        //When it's our turn
        trackPlayerInput();

        int softlockSum = 0;
        for (int i = 0; i < 3; ++i) {
            softlockSum += player1_progress[i];
            softlockSum += player2_progress[i];
        }
        //if (softlockSum == -6) event_manager.Next_Turn(); //soft lock, players incorrectly input all combinations

        //if both players are done, change turn
        if (player1_done && player2_done || softlockSum == -6 || timeUp)
        {
            endTurn();
        }
    }

    private void trackPlayerInput() {
        int player1_dir = -1;
        int player2_dir = -1;

        //player1 input checking---------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.W))
        {
            player1_dir = 0;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            player1_dir = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            player1_dir = 2;
        }
        else if (Input.GetKeyDown(KeyCode.D)) { player1_dir = 3; }

        //if wrong input, set index to -1. Otherwise, increment
        for (int i = 0; i < 3; ++i) {

            if (player1_progress[i] == -1) continue;

            Arrow current_arrow = arrow_list[i][ player1_progress[i] ];

            //if the direction matches
            if (player1_dir == (int)(current_arrow.getRotation()) / 90 && player1_progress[i] != -1)
            {
                player1_progress[i]++;

                if (!player1_done && player1_progress[i] == max_arrows) accumulateDamage(i);

                player1_done = player1_progress[i] == max_arrows ? true : player1_done;
                player1_progress[i] = Mathf.Min(player1_progress[i], max_arrows - 1);
                current_arrow.setColor(Color.white);
            }
            else if(player1_dir != -1){
                if (player1_progress[i] != -1) current_arrow.gameObject.GetComponent<Animator>().SetTrigger("Shake");
                player1_progress[i] = -1;
                //RUMBLE CURRENT ARROW USING ANIMATIONS
            }
        }
        //---------------------------------------------------------------------------





        //player2 input checking---------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player2_dir= 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player2_dir = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player2_dir = 2;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) { player2_dir = 3; }

        //if wrong input, set index to -1. Otherwise, increment
        for (int i = 0; i < 3; ++i)
        {
            if (player2_progress[i] == -1) continue;

            Arrow current_arrow = arrow_list[3+i][player2_progress[i]];

            //if the direction matches
            if (player2_dir == (int)(current_arrow.getRotation()) / 90 && player2_progress[i] != -1)
            {
                player2_progress[i]++;

                if (!player2_done && player2_progress[i] == max_arrows) accumulateDamage(i);

                player2_done = player2_progress[i] == max_arrows ? true : player2_done;
                player2_progress[i] = Mathf.Min(player2_progress[i], max_arrows - 1);
                current_arrow.setColor(Color.white);
            }
            else if(player2_dir != -1)
            {
                if(player2_progress[i] != -1) current_arrow.gameObject.GetComponent<Animator>().SetTrigger("Shake"); //shake the first wrong arrow
                player2_progress[i] = -1;
                
            }
        }
        //---------------------------------------------------------------------------



    }

    public void setCan_Input(bool set) {
        can_input = set;

        //reset damage value
        damageDealt = 0;

        //reset timer
        timeUp = false;

        //resets the player progress
        player1_progress = new int[]{ 0,0,0};
        player2_progress = new int[] { 0, 0, 0 };
    }

    public void endTurn() {
        event_manager.Next_Turn();
        generateNewArrows();

        //calculate total damage and apply to boss through e_m
        event_manager.applyDamage(damageDealt);

        player1_done = false;
        player2_done = false;
    }

    private void accumulateDamage(int index) {
        int addend = 0;
        switch (index)
        {
            case 0:
                addend = 30;
                break;
            case 1:
                addend = 0;
                break;
            case 2:
                addend = 50;
                break;
            default:
                break;
        }
        damageDealt += addend;
    }



    public void dealDamage(int damage) {
        health -= damage;
    }
}
