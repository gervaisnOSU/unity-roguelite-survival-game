using UnityEngine;
using UnityEngine.UI;

public class HeartUIScript : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public PlayerStats player;

    // Update is called once per frame
    void Update()
    {
        float health = player.GetCurrentHealth();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}