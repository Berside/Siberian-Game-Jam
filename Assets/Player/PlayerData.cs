using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int kills = 0;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getKills()
    {
        return kills;
    }

    public void setKills(int value)
    {
        kills = value;
    }
    public int getScore()
    {
        return score;
    }

    public void setScore(int value)
    {
        score = value;
    }
}
