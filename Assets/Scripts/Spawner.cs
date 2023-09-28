using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }
    public GameObject ground;
    public Color[] colors;
    public float colorChangeInterval = 2f; 

    private int currentColorIndex = 0;
    public List<GameObject> spawnedGrounds = new List<GameObject>();

    private float lerpTime = 0f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (GameManager.Instance.isDead) return;
        
        StartCoroutine(ChangeGroundColors());
    }
    public void Spawn()
    {
        if (GameManager.Instance.isDead) return;
        
        Vector3 direction;


        if (Random.Range(0, 2) == 0)
        {
            direction = Vector3.forward;

        }
        else
        {
            direction = Vector3.left;

        }


        ground = Instantiate(ground, ground.transform.position + direction, ground.transform.rotation);
        spawnedGrounds.Add(ground);
        

    }

    IEnumerator ChangeGroundColors()
    {
        while (true)
        {
            if (GameManager.Instance.isGameStarted)
            {
                var currentColor = colors[currentColorIndex];
                var nextColor = colors[(currentColorIndex + 1) % colors.Length];

                lerpTime = 0f;
                while (lerpTime < 1f)
                {
                    lerpTime += Time.deltaTime / colorChangeInterval;
                    foreach (var groundObject in spawnedGrounds)
                    {
                        var groundRenderer = groundObject.GetComponent<Renderer>();
                        groundRenderer.material.color = Color.Lerp(currentColor, nextColor, lerpTime);
                    }
                    yield return null;
                }

                currentColorIndex = (currentColorIndex + 1) % colors.Length;
                yield return new WaitForSeconds(colorChangeInterval);
            }
            else
            {
                yield return null; 
            }
        }
    }

}