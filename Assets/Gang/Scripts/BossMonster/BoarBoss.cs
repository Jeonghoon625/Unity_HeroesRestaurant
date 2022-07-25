using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoarBoss : MonoBehaviour
{
    public CameraShake cameraShake;
    public StageManager stageManager;
    public Enemy Board;
    /******************************************
     * ½ºÅ³
     * ***************************************/
    private bool isFirstSkill = false;
    public bool isAlive = true;
    [SerializeField]
    private float boarSpawnTime = 10f;
    [SerializeField]
    private float stunCoolTime = 15f;
    private float timer = 0f;
    [SerializeField]
    private GameObject stunSkillPrefab;
    private GameObject stunSkill;

    [SerializeField]
    private List<Vector3> spawnPoint;
    private Quaternion rot = Quaternion.identity;

    [SerializeField]
    private GameObject boarPrefab;

    private List<GameObject> boarList = new List<GameObject>();
    private void Start()
    {
        stageManager = Board.stageManager;
        rot.y += 180f;
    }

    public void BossUpdate()
    {
        if(!isAlive)
        {
            return;
        }
        timer += Time.deltaTime;
        if(isFirstSkill)
        {
            if (timer > stunCoolTime)
            {
                isFirstSkill = false;
                timer = 0f;
                stunSkill = Instantiate(stunSkillPrefab);
                Board.animator.SetTrigger("StunSkill");
                Board.SetState("None");
            }
            return;
        }
        if(timer > boarSpawnTime)
        {
            Board.animator.SetTrigger("SpawnSkill");
            timer = 0f;
        }
        
    }
    public void StunSkill()
    {
        cameraShake.Shake();
        Destroy(stunSkill);
        foreach (var hero in stageManager.herosList)
        {
            var heroInfo = hero.GetComponent<Heros>();
            if (!heroInfo.isInvincibility)
            {
                heroInfo.bossPos = transform.position;
                heroInfo.doneControll = true;
                heroInfo.skillButton.GetComponent<Button>().interactable = false;
                heroInfo.SetState("Stun");
            }
        }
    }
    public void SkillEnd()
    {
        Board.SetState("Idle");
        foreach (var hero in stageManager.herosList)
        {
            var heroInfo = hero.GetComponent<Heros>();
            if (!heroInfo.isInvincibility)
            {
                heroInfo.doneControll = false;
            }
        }
    }

    public void BoarSpawn()
    {
        isFirstSkill = true;
        foreach(var pos in spawnPoint)
        {
            boarList.Add(Instantiate(boarPrefab, pos, rot));
        }
    }
    void BossDead()
    {
        foreach (var pos in boarList)
        {
            pos.GetComponent<Enemy>().Dead();
        }
    }
}
