using UnityEngine;
using System.Collections;

public class HeartGenerator : MonoBehaviour
{

    public ObjectPooler heartPooler;
    public void SpawnHeart(Vector3 startPosition)
    {
        GameObject heart = heartPooler.GetPooledObject();
        heart.transform.position = startPosition;
        heart.SetActive(true);
    }
}
