using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public TMP_Text textLife;
    public int size;
    public Text scoreTB;
    Rigidbody2D rb;

    private int ball3Field;

    private void Awake()
    {
        textLife.text = health.ToString();
        scoreTB = GameObject.Find("Score").GetComponent<Text>();
        ball3Field = GenerateBalls.ball3InField;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(int maxHealth)
    {
        health = maxHealth;
        this.maxHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        textLife.text = health.ToString();
        Manager.score += damage;
        scoreTB.text = Manager.score.ToString();
        if (health <= 0)
        {
            GenerateBalls generateBalls = new GenerateBalls();
            var pos = gameObject.transform.position;
            int life = gameObject.GetComponent<Ball>().maxHealth;
            GenerateBalls.ballsField--;
            if (size == 3)
            {
                if (gameObject.GetComponent<Ball>().maxHealth != 1)
                {
                    life = (life / 2);
                }
                var b1 = generateBalls.GetBall(new Vector3(0.9f, 0.9f, 0.9f), new Vector3(pos.x - 1, pos.y, pos.z), life, 2);
                var b2 = generateBalls.GetBall(new Vector3(0.9f, 0.9f, 0.9f), new Vector3(pos.x + 1, pos.y, pos.z), life, 2);
                size--;
                GenerateBalls.ballsField = GenerateBalls.ballsField + 2;
                GenerateBalls.ball3InField--;
                Destroy(gameObject);
            }
            else if (size == 2)
            {
                if (gameObject.GetComponent<Ball>().maxHealth != 1)
                {
                    life = (life / 2);
                }

                var b1 = generateBalls.GetBall(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(pos.x - 1, pos.y, pos.z), life, 1);
                var b2 = generateBalls.GetBall(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(pos.x + 1, pos.y, pos.z), life, 1);
                GenerateBalls.ballsField = GenerateBalls.ballsField + 2;
                size--;
                Destroy(gameObject);
            }
            else if (size == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Dead()
    {
        GameObject player = GameObject.Find("FullPlayer");
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        LoadScene.SceneLoad(2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("FullPlayer");
            player.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            Player.isDead = true;
            Invoke("Dead", 0.5f);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            int jump = 2;
            Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(rb.velocity.x + 1, jump);

            int damage = collision.gameObject.GetComponent<Bullet>().damage;
            TakeDamage(damage);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Grass")
        {
            rb.velocity = new Vector2(rb.velocity.x + 2, 8f);
        }
    }

}
