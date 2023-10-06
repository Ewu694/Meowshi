using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;
    private ItemSpawner itemspawner;

    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public int score;
    public float multiply = 1f;

    //two item prefabs made, given the item tag.
    public GameObject ItemA;
    public GameObject ItemB;


    static public float itemDur = 0;

    //counts up each time an item is collected.
    public int itemHeld = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        itemspawner = FindObjectOfType<ItemSpawner>();

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        itemHeld = 0;
        itemDur = 0;

        playButton.SetActive(false);
        gameOver.SetActive(false);

        //Activating allowing the items to move.
        ItemA.SetActive(true);
        ItemB.SetActive(true);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }

        itemMove[] items = FindObjectsOfType<itemMove>();

        for(int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].gameObject);
        }


    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;

    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (itemDur > 0 && itemDur < 5)
        {
            score = score++ + 1;
        }
    }

    //Item Collection Counter goes up for each item you collect.
    public void itemCollection()
    {
            itemHeld++;

        
    }

    private void Update()
    {
        if (itemHeld >= 1)
        {
            itemDur += Time.deltaTime;

            if (itemDur >= 5)
            {
                itemHeld = 0;
                itemDur = 0;
            }

        }


    }
}
