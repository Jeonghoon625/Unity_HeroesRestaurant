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

    private Reinforce character;

    private void Awake()
    {
        Reinforce upgradeLoad = ReinforceSystem.Load("Upgrade");
        reinforcement.power = upgradeLoad.power;
        reinforcement.health = upgradeLoad.health;
        reinforcement.powerLevel = upgradeLoad.powerLevel;
        reinforcement.healthLevel = upgradeLoad.healthLevel;
        reinforcement.powerUpGold = upgradeLoad.powerUpGold;
        reinforcement.healthUpGold = upgradeLoad.healthUpGold;

        character = new Reinforce(upgradeLoad.power, upgradeLoad.health, upgradeLoad.powerLevel, upgradeLoad.healthLevel, upgradeLoad.powerUpGold, upgradeLoad.healthUpGold);

        //gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        waponLv.text = $"LV.{reinforcement.powerLevel}";
        currentWapon.text = $"��� ���� ���·� {reinforcement.power}% ����";
        nextwapon.text = $"����:��� ���� ���·� {reinforcement.power + up}% ����";
        waponGold.text = $"{reinforcement.powerUpGold}a";

        healthLv.text = $"LV.{reinforcement.healthLevel}";
        currentHealth.text = $"��� ���� ü�� {reinforcement.health}% ����";
        nextHealth.text = $"����:��� ���� ü�� {reinforcement.health + up}% ����";
        healthGold.text = $"{reinforcement.healthUpGold}a";
    }
    public void WaponUpgrade()
    {
        if (GameManager.Instance.goodsManager.gold < reinforcement.powerUpGold)
        {
            return;
        }
        GameManager.Instance.goodsManager.gold -= reinforcement.powerUpGold;
        // if ��� üũ �Һ�
        reinforcement.powerLevel++;
        reinforcement.power += up;
        reinforcement.powerUpGold *= 2;

        waponLv.text = $"LV.{reinforcement.powerLevel}";
        currentWapon.text = $"��� ���� ���·� {reinforcement.power}% ����";
        nextwapon.text = $"����:��� ���� ���·� {reinforcement.power + up}% ����";
        waponGold.text = $"{reinforcement.powerUpGold}a";

        Save();
    }
    public void HealthUpgrade()
    {
        if (GameManager.Instance.goodsManager.gold < reinforcement.healthUpGold)
        {
            return;
        }
        GameManager.Instance.goodsManager.gold -= reinforcement.healthUpGold;
        // if ��� üũ �Һ�
        reinforcement.healthLevel++;
        reinforcement.health += up;
        reinforcement.healthUpGold *= 2;

        healthLv.text = $"LV.{reinforcement.healthLevel}";
        currentHealth.text = $"��� ���� ü�� {reinforcement.health}% ����";
        nextHealth.text = $"����:��� ���� ü�� {reinforcement.health + up}% ����";
        healthGold.text = $"{reinforcement.healthUpGold}a";

        Save();
    }

    public void Save()
    {
        character.power = reinforcement.power;
        character.health = reinforcement.health;
        character.powerLevel = reinforcement.powerLevel;
        character.healthLevel = reinforcement.healthLevel;
        character.powerUpGold = reinforcement.powerUpGold;
        character.healthUpGold = reinforcement.healthUpGold;

        ReinforceSystem.Save(character, "Upgrade");
    }
}
