using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBalls : MonoBehaviour
{
    public static float timer;
    [SerializeField] public List<Ball> balls = new List<Ball>();
    private static List<Ball> allBalls = new List<Ball>();
    public static int counter = 0;
    private static Rigidbody2D rbBall;
    public static int ballsField;

    public static int ball3InField = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        allBalls = balls;
    }
    public void GetBall(Vector3 vectorScale, Vector3 vectorPos, int size)
    {
        ballsField++;
        Vector3 scale = Vector3.zero;
        if (size == 3)
        {
            ball3InField++;
            scale = new Vector3(1, 1, 1);
        }
        else if (size == 2)
        {
            scale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        else if (size == 1)
        {
            scale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        int indBall = Random.Range(0, allBalls.Count);
        var ball = allBalls[indBall];
        ball.transform.localScale = vectorScale;
        ball.SetHealth(RandHealth());
        ball.size = size;
        ball.transform.localScale = scale;
        if (vectorPos == Vector3.zero)
        {
            var spawns = GameObject.FindGameObjectsWithTag("SpawnBallPoint");
            int indSpawn = Random.Range(0, 2);
            Instantiate(ball, spawns[indSpawn].transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ball, vectorPos, Quaternion.identity);
        }
    }
    int RandHealth()
    {
        int rand = Random.Range(0, 7);
        if (rand % 2.0 == 0 && rand != 0) return rand;
        else
        {
            rand = RandHealth();
            return rand;
        }
    }

    public Ball GetBall(Vector3 vectorScale, Vector3 vectorPos, int health, int size)
    {
        int indBall = Random.Range(0, allBalls.Count);
        var ball = allBalls[indBall];
        ball.transform.localScale = vectorScale;
        ball.SetHealth(health);
        ball.size = size;
        if (vectorPos == Vector3.zero)
        {
            var spawns = GameObject.FindGameObjectsWithTag("SpawnBallPoint");
            int indSpawn = Random.Range(0, 2);
            Instantiate(ball, spawns[indSpawn].transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ball, vectorPos, Quaternion.identity);
        }
        if (size == 3) ball3InField++;
        return ball;
    }
    // Update is called once per frame
    void Update()
    {
        if (ball3InField >= 3)
        {
            timer = 0;
        }
        if (timer >= 5)
        {
            if (counter <= 6)
            {
                timer = 0;
                counter++;
                GetBall(new Vector3(1, 1, 1), Vector3.zero, Random.Range(1, 4));
            }
            else
            {
                if (ballsField == 0)
                {
                    LoadScene.SceneLoad(3);
                }

            }
        }
        else if (counter == 0 && timer >= 2)
        {
            timer = 0;
            counter++;
            GetBall(new Vector3(1, 1, 1), Vector3.zero, Random.Range(1, 4));
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
