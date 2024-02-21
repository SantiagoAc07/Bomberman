using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfBombers;

    public Image[] bombers;
    public Sprite fullBomber;
    public Sprite emptyBomber;

    void Update()
    {
        if(health > numOfBombers)
        {
            health = numOfBombers;
        }
        for (int i = 0; i < bombers.Length; i++)
        {
            if(i<health)
            {
                bombers[i].sprite = fullBomber;
            }
            else
            {
                bombers[i].sprite = emptyBomber;
            }



            if (i < numOfBombers)
            {
                bombers[i].enabled = true;
            }
            else
            {
                bombers[i].enabled = false;
            }

        }
    }

}

