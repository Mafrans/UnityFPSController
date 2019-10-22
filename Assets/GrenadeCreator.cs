using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCreator : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject grenade;

    public void SpawnGrenade() {
        GameObject gameObj = Instantiate(grenade);
        gameObj.transform.position = spawnPosition.position;
        gameObj.transform.localScale = spawnPosition.localScale;
        gameObj.transform.rotation = spawnPosition.rotation;
    }
}
