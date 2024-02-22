using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{
    PlayerController player;
    TextMeshProUGUI distanceText;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceText.text = Mathf.FloorToInt(player.distance) + " m";
    }
}