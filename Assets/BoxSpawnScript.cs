using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BoxPrototypeInfo
{
    public BoxStats BoxPrototype;

    [Range(0, 1)]
    public float Probability;
}

public class BoxSpawnScript : MonoBehaviour {

    [SerializeField]
    float SpawnDistance = 100;

    [SerializeField]
    float MaxRotationOffset = 60;

    [SerializeField]
    float SpawnDelay = 10;

    [SerializeField]
    float SpawnDelayDegradation = 0.9f;

    float SpawnTimer;

    [SerializeField]
    BoxPrototypeInfo[] BoxPrototypes;

    float ProbabilitiesSumm;

    void Awake()
    {
        foreach (var item in BoxPrototypes)
        {
            ProbabilitiesSumm += item.Probability;
        }
    }
	
	void Update ()
    {
        if (SpawnTimer <= 0)
        {
            SpawnDelay *= SpawnDelayDegradation;
            SpawnTimer = SpawnDelay;

            SpawnBox();
        }
        else
        {
            SpawnTimer -= Time.deltaTime;
        }
    }

    void SpawnBox()
    {
        GameObject.Instantiate<BoxStats>(GetPrototype(), 
            transform.position + new Vector3((Random.value - 0.5f)* SpawnDistance, 0, 0), 
            Quaternion.Euler(0, 0, (Random.value - 0.5f) * MaxRotationOffset));
    }

    BoxStats GetPrototype()
    {
        int i = 0;
        float Key = Random.value * ProbabilitiesSumm;

        while (Key > 0 || BoxPrototypes[i].Probability == 0)
        {
            Key -= BoxPrototypes[i].Probability;
            i = (i + 1) % BoxPrototypes.Length;
        }

        return BoxPrototypes[i].BoxPrototype;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(SpawnDistance + 1, 1, 1));
    }
}
