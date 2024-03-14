using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    PlayerController player;
    TextMeshProUGUI distanceText;
    TextMeshProUGUI chargeText;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        chargeText = GameObject.Find("ChargeText").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        distanceText.text = Mathf.FloorToInt(player.distance) + " m";
        chargeText.text = "Dash Charges: " + player.currentDashCharges;
    }
}
