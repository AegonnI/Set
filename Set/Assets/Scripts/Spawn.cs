using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject card;

    public static int cardCount = 12;

    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    private bool[,,,] cardBoard = new bool[3,3,3,3];

    private Card[] cards = new Card[cardCount];

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();

        GenCards();

        while (!SolvabilityTest())
        {
            cardBoard = new bool[3, 3, 3, 3];
            GenCards();
        }        

        for (int i = 0; i < cardCount; i++)
        {
            spawnPosition = new Vector3(i % 3, i / 3, 0);

            Card crd = card.GetComponent<Card>();
            SpriteRenderer spr = GetComponent<SpriteRenderer>();

            crd.type = cards[i].type;
            crd.color = cards[i].color;
            crd.count = cards[i].count;
            crd.filling = cards[i].filling;

            Instantiate(card, spawnPosition, spawnRotation);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsEqualOrUnqual(int i, int j, int k)
    {
        return (((i == j) && (j == k)) || ((i != j) && (j != k) && (i != k)));
    }

    void GenCards()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < cardCount; i++)
        {
            cards[i] = new Card();
            cards[i].type = rand.Next(3);
            cards[i].color = rand.Next(3);
            cards[i].count = rand.Next(3);
            cards[i].filling = rand.Next(3);
            while (cardBoard[cards[i].type, cards[i].color, cards[i].count, cards[i].filling] == true)
            {
                cards[i].type = rand.Next(3);
                cards[i].color = rand.Next(3);
                cards[i].count = rand.Next(3);
                cards[i].filling = rand.Next(3);
            }
            cardBoard[cards[i].type, cards[i].color, cards[i].count, cards[i].filling] = true;
        }

    }

    bool SolvabilityTest()
    {
        for (int i = 0; i < cardCount - 2; i++)
        {
            for (int j = i + 1; j < cardCount - 1; j++)
            {
                for (int k = j + 1; k < cardCount; k++)
                {

                    if (IsEqualOrUnqual(cards[i].type, cards[j].type, cards[k].type) &&
                        IsEqualOrUnqual(cards[i].color, cards[j].color, cards[k].color) &&
                        IsEqualOrUnqual(cards[i].count, cards[j].count, cards[k].count) &&
                        IsEqualOrUnqual(cards[i].filling, cards[j].filling, cards[k].filling))
                    {
                        Debug.Log(cards[i].type + ", " + cards[i].color + ", " + cards[i].count + ", " + cards[i].filling);
                        Debug.Log(cards[j].type + ", " + cards[j].color + ", " + cards[j].count + ", " + cards[j].filling);
                        Debug.Log(cards[k].type + ", " + cards[k].color + ", " + cards[k].count + ", " + cards[k].filling);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
