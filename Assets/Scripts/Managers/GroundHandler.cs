using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    [SerializeField] private int amountOfGroundObjects = 3;
    [SerializeField] private GameObject[] grounds;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject environmentParent;

    private int distanceBetweenGround = 100;
    private float groundYPosition = -7.11f;
    private int distanceBetweenGroundRelocation;

    private void Awake()
    {
        grounds = new GameObject[amountOfGroundObjects];
        distanceBetweenGroundRelocation = distanceBetweenGround * 3;
    }
    void Start()
    {
        InstantiateGroundObjects(amountOfGroundObjects);
    }

    void Update()
    {
        RelocateGroundObjects();
    }

    void RelocateGroundObjects()
    {
        foreach (GameObject ground in grounds)
        {
            //If the palyer walks forward, move the ground in front of the player
            if (Player.Instance.transform.position.x >= ground.transform.position.x + 85f)
            {
                ground.transform.position = new Vector3(ground.transform.position.x + distanceBetweenGroundRelocation, groundYPosition, 0f);
            }
            //If the player walks backwards, move the furtherest away ground, behind the player
            if (Player.Instance.transform.position.x <= ground.transform.position.x - 235f)
            {
                ground.transform.position = new Vector3(ground.transform.position.x - distanceBetweenGroundRelocation, groundYPosition, 0f);
            }
        }
    }

    void InstantiateGroundObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = new Vector3(i * distanceBetweenGround, groundYPosition, 0f);
            GameObject tempGround = Instantiate(groundPrefab, spawnPos, Quaternion.identity);
            tempGround.transform.parent = environmentParent.transform;
            grounds[i] = tempGround;
        }
    }

}
