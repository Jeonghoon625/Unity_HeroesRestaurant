[System.Serializable]
public class Reinforce
{
    public Reinforce(int power, int health, int powerLevel, int healthLevel, int powerUpGold, int healthUpGold)
    {
        this.power = power;
        this.health = health;
        this.powerLevel = powerLevel;
        this.healthLevel = healthLevel;
        this.powerUpGold = powerUpGold;
        this.healthUpGold = healthUpGold;
    }
    public int power = 0;
    public int health = 0;

    public int powerLevel = 1;
    public int healthLevel = 1;

    public int powerUpGold = 50;
    public int healthUpGold = 50;
}


