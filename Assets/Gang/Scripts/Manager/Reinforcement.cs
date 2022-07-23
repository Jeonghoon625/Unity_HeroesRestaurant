using UnityEngine;

[CreateAssetMenu(fileName = "Reinforcement", menuName = "ScriptableObject/Reinforcement")]
public class Reinforcement : ScriptableObject
{
    public int power;
    public int health;

    public int powerUpGold; 
    public int healthUpGold;
}
