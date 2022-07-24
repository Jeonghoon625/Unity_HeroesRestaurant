using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReinforcementButton : MonoBehaviour
{
    public Reinforcement reinforcement;
    private int up = 24;

    [SerializeField]
    private TextMeshProUGUI waponLv;
    [SerializeField]
    private TextMeshProUGUI currentWapon;
    [SerializeField]
    private TextMeshProUGUI nextwapon;
    [SerializeField]
    private TextMeshProUGUI waponGold;

    [SerializeField]
    private TextMeshProUGUI healthLv;
    [SerializeField]
    private TextMeshProUGUI currentHealth;
    [SerializeField]
    private TextMeshProUGUI nextHealth;
    [SerializeField]
    private TextMeshProUGUI healthGold;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Debug.Log("활성화");
        waponLv.text = $"LV.{reinforcement.powerLevel}";
        currentWapon.text = $"모든 영웅 공력력 {reinforcement.power}% 증가";
        nextwapon.text = $"다음:모든 영웅 공력력 {reinforcement.power + up}% 증가";
        waponGold.text = $"{reinforcement.powerUpGold}";

        healthLv.text = $"LV.{reinforcement.healthLevel}";
        currentHealth.text = $"모든 영웅 체력 {reinforcement.health}% 증가";
        nextHealth.text = $"다음:모든 영웅 체력 {reinforcement.health + up}% 증가";
        healthGold.text = $"{reinforcement.healthUpGold}";
    }
    public void WaponUpgrade()
    {
        // if 골드 체크 소비
        reinforcement.powerLevel++;
        reinforcement.power += up;
        reinforcement.powerUpGold *= 2;

        waponLv.text = $"LV.{reinforcement.powerLevel}";
        currentWapon.text = $"모든 영웅 공력력 {reinforcement.power}% 증가";
        nextwapon.text = $"다음:모든 영웅 공력력 {reinforcement.power + up}% 증가";
        waponGold.text = $"{reinforcement.powerUpGold}";

    }
    public void HealthUpgrade()
    {
        // if 골드 체크 소비
        reinforcement.healthLevel++;
        reinforcement.health += up;
        reinforcement.healthUpGold *= 2;

        healthLv.text = $"LV.{reinforcement.healthLevel}";
        currentHealth.text = $"모든 영웅 체력 {reinforcement.health}% 증가";
        nextHealth.text = $"다음:모든 영웅 체력 {reinforcement.health + up}% 증가";
        healthGold.text = $"{reinforcement.healthUpGold}";
    }
}
