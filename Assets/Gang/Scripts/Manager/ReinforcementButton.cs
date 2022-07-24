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
        Debug.Log("Ȱ��ȭ");
        waponLv.text = $"LV.{reinforcement.powerLevel}";
        currentWapon.text = $"��� ���� ���·� {reinforcement.power}% ����";
        nextwapon.text = $"����:��� ���� ���·� {reinforcement.power + up}% ����";
        waponGold.text = $"{reinforcement.powerUpGold}";

        healthLv.text = $"LV.{reinforcement.healthLevel}";
        currentHealth.text = $"��� ���� ü�� {reinforcement.health}% ����";
        nextHealth.text = $"����:��� ���� ü�� {reinforcement.health + up}% ����";
        healthGold.text = $"{reinforcement.healthUpGold}";
    }
    public void WaponUpgrade()
    {
        // if ��� üũ �Һ�
        reinforcement.powerLevel++;
        reinforcement.power += up;
        reinforcement.powerUpGold *= 2;

        waponLv.text = $"LV.{reinforcement.powerLevel}";
        currentWapon.text = $"��� ���� ���·� {reinforcement.power}% ����";
        nextwapon.text = $"����:��� ���� ���·� {reinforcement.power + up}% ����";
        waponGold.text = $"{reinforcement.powerUpGold}";

    }
    public void HealthUpgrade()
    {
        // if ��� üũ �Һ�
        reinforcement.healthLevel++;
        reinforcement.health += up;
        reinforcement.healthUpGold *= 2;

        healthLv.text = $"LV.{reinforcement.healthLevel}";
        currentHealth.text = $"��� ���� ü�� {reinforcement.health}% ����";
        nextHealth.text = $"����:��� ���� ü�� {reinforcement.health + up}% ����";
        healthGold.text = $"{reinforcement.healthUpGold}";
    }
}
