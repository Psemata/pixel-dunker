using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>SpawnBonus</c>, spawns random power up at fix position
/// </summary>
public class SpawnBonus : MonoBehaviour
{
    //Prefabs of all items
    public GameObject speedUp, armor, reverseBaskets, frozen, explosion;

    //spawn rate for the items
    public float spawnRate;
    private float cooldown;

    //Position of spawning zone
    public Vector2 spawn1Active, spawn2Active;
    public Vector2 spawn1Passive, spawn2Passive;

    //List of gameobject instantiate
    private GameObject[] instantiate;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = spawnRate;
        instantiate = new GameObject[4];
    }

    /// <summary>
    /// Spawn active power up
    /// </summary>
    /// <param name="prefab"></param>
    void spawnActive(GameObject prefab){
        //Random for the begining
        if(instantiate[2] == null && instantiate[3] == null){
            int random = Random.Range(1,2);
            if(random == 1){
                instantiate[2] = Instantiate(prefab, spawn1Active, Quaternion.identity);
            }else{
                instantiate[3] = Instantiate(prefab, spawn2Active, Quaternion.identity);
            }
        }else if(instantiate[2] == null){
            instantiate[2] = Instantiate(prefab, spawn1Active, Quaternion.identity);
        }else if(instantiate[3] == null){
            instantiate[3] = Instantiate(prefab, spawn2Active, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawn passive power up
    /// </summary>
    /// <param name="prefab"></param>
    void spawnPassive(GameObject prefab){
        //Random for the begining
        if(instantiate[0] == null && instantiate[1] == null){
            int random = Random.Range(1,2);
            if(random == 1){
                instantiate[0] = Instantiate(prefab, spawn1Passive, Quaternion.identity);
            }else{
                instantiate[1] = Instantiate(prefab, spawn2Passive, Quaternion.identity);
            }
        }else if(instantiate[0] == null){
            instantiate[0] = Instantiate(prefab, spawn1Passive, Quaternion.identity);
        }else if(instantiate[1] == null){
            instantiate[1] = Instantiate(prefab, spawn2Passive, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        //After the cooldown
        if(cooldown <= 0){
            cooldown = spawnRate;
            //Random for items
            int random = Random.Range(1,8);
            // 6/8 passives - 2/8 active
            switch(random){
                case 1:
                    spawnPassive(speedUp);
                break;
                case 2:
                    spawnPassive(armor);
                break;
                case 3:
                    spawnPassive(reverseBaskets);
                break;
                case 4:
                    spawnActive(frozen);
                break;
                case 5:
                    spawnActive(explosion);
                break;
                case 6:
                    spawnPassive(speedUp);
                break;
                case 7:
                    spawnPassive(armor);
                break;
                case 8:
                    spawnPassive(reverseBaskets);
                break;
                default:
                break;
            }
        }
    }
}
