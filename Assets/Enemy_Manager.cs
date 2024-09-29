using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool can_input;
    [SerializeField] private int health;
    private const int max_health = 500;
    private int damageDealt;

    public AK.Wwise.Event growl;

    [SerializeField] private Event_Manager event_manager;
    [SerializeField] private GameObject health_bar;
    void Start()
    {
        can_input = false;
        health = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if (!can_input) return;

        takeTurn();
        event_manager.Next_Turn();
    }

    void takeTurn()
    {
        growl.Post(gameObject);

        //play animation

        //deduct from player health through e_m
        damageDealt = 10;
        event_manager.applyDamage(damageDealt);

    }


    public void setCan_Input(bool set)
    {
        can_input = set;

        //reset damage value
        damageDealt = 0;

    }

    public void dealDamage(int damage)
    {
        health -= damage;
        health_bar.GetComponent<Slider>().value = (float)health/(float)max_health;
    }
}
