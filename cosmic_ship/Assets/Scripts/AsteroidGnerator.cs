using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGnerator : MonoBehaviour
{
    public List<GameObject> asteroids = new List<GameObject>();
    public int count;
    public int maxSceneCount;
    public int destr = 0;
    private int oldCount = 0;

    public void gener()
    {
        int type = Random.Range(0, 4);
        float distance = Random.Range(300f, 500f);
        float angle = Random.Range(0f, 100f);

        Vector3 position = new Vector3(-Mathf.Cos(Mathf.Deg2Rad * angle) * distance, 0, -Mathf.Sin(Mathf.Deg2Rad * angle) * distance);
        Quaternion rotatin = Quaternion.identity;
        rotatin.eulerAngles = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
        float scale = Random.Range(0.5f, 4);
        GameObject asteroid = Instantiate(asteroids[type], position, rotatin);
        asteroid.transform.localScale = new Vector3(scale, scale, scale);
        asteroid.GetComponent<Renderer>().material.SetColor("AsteroidColor", new Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, 1f));
        asteroid.GetComponent<RockController>().generator = this;
    }

    IEnumerator Generate()
    {

        yield return new WaitForSeconds(Random.Range(10, 20));
        gener();

    }
    void Start()
    {
        oldCount = maxSceneCount;
        for (int i = 0; i < maxSceneCount; i++)
        {
            gener();
        }
    }

    public void asteroidDestroyed()
    {
        gener();
    }
}
