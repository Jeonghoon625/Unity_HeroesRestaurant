using UnityEngine;

[CreateAssetMenu(fileName = "Reinforcement", menuName = "ScriptableObject/Reinforcement")]
public class Reinforcement : ScriptableObject
{
    public int power = 0;
    public int health = 0;

    public int powerLevel = 1;
    public int healthLevel = 1;

    public int powerUpGold = 50; 
    public int healthUpGold = 50;
}
