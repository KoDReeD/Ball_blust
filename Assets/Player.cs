using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;

    public float moveSpeed = 12f;
    public GameObject bullet;
    public Transform shotPoint;
    public static int bulletInSecond = 3;
    private float timeBetweenShot = ((float)60 / bulletInSecond) / (float)100;
    private float timer;

    public static bool isDead;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = timeBetweenShot;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!isDead)
            {
                MovePlayer();
            }

        }
    }

    void ShotPlayer()
    {
        if (timer >= timeBetweenShot)
        {
            Instantiate(bullet, shotPoint.position, transform.rotation);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void MovePlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        Touch touch = Input.GetTouch(0);
        //переводим в локальные координаты
        touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        if (touchPosition.y <= 4)
        {
            touchPosition.z = 0;
            //преодаливаемое расстояние
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
            ShotPlayer();

            if (touch.phase == TouchPhase.Ended)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;

            }
        }
    }
}
