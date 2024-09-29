using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Input_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private const int max_arrows = 5;

    private Color[] colors = new Color[4] { Color.red,Color.blue,Color.green,Color.yellow };

    private GameObject player1_abilities;
    private GameObject player2_abilities;

    private bool can_input;


    //At the moment, there are 6 entries, 0,1,2 correspond to player 1's attack,defend,special respectively. The latter are palyer 2's
    private List<Arrow[]> arrow_list = new List<Arrow[]>();

    private void Awake()
    {
        player1_abilities = GameObject.FindGameObjectWithTag("Player_1");
        player2_abilities = GameObject.FindGameObjectWithTag("Player_2");


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
                int rand = Random.Range(0, 4); //generate number 1-4
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
                //arrow.setColor(colors[rand]);
            }
        }
    }

    private void Start()
    {
        generateNewArrows();
    }


    // Update is called once per frame
    void Update()
    {
        if (!can_input) return;

        //
    }
}
