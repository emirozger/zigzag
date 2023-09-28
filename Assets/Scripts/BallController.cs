using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    [SerializeField] private float addedSpeed;

    void Start()
    {

        direction = Vector3.forward;
        for (int i = 0; i <40; i++)
        {
            Spawner.Instance.Spawn();
        }
    }
    void Update()
    {
        Vector3 movement = direction * speed * Time.deltaTime;
        transform.position += movement;
        Movements();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            for (int i = 0; i < 1; i++)
            {
                Spawner.Instance.Spawn();
            }
            StartCoroutine(DeleteGround(collision.gameObject));
            Spawner.Instance.spawnedGrounds.Remove(collision.gameObject);
        }
    }

    IEnumerator DeleteGround(GameObject ground)
    {
        yield return new WaitForSeconds(1f);
        ground.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(ground,1f);
    }
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Diamond"))
        {
            GameManager.Instance.score+=2;
            other.gameObject.SetActive(false);
        }
    }
    void Movements()
    {
        if (GameManager.Instance.isDead) return;
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.score++;
                GameManager.Instance.scoreText.text = GameManager.Instance.score.ToString();
                speed += addedSpeed * Time.deltaTime;
                
                if (direction.x == 0)
                {
                    direction = Vector3.left;
                }
                else
                {
                    direction = Vector3.forward;
                }
            }  
    }
}
