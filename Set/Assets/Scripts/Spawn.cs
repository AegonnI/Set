using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject card;

    public static int cardCount = 12;

    public int numberOfColumns = 3;

    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    private bool[,,,] cardBoard = new bool[3, 3, 3, 3];

    public static Card[] cards = new Card[cardCount];

    private Card crd;

    // Start is called before the first frame update
    void Start()
    {
        do
        {
            cardBoard = new bool[3, 3, 3, 3];
            GenCards();
        }
        while (!SolvabilityTest());

        for (int i = 0; i < cardCount; i++)
        {
            CardSpawner(i);
        }            
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Logic.win_or_loose);
        if (Logic.win_or_loose == 1)
        {
            Regeneration();

            Logic.win_or_loose = 0;
        }
        if (Logic.win_or_loose == 2)
        {
            for (int i = 0; i < cardCount; i++)
            {
                if (cards[i].framed)
                {
                    cards[i].framed = false;
                }
            }
            Logic.win_or_loose = 0;
        }
    }

    void CardSpawner(int i)
    {
        spawnPosition = new Vector2(2 * (i % numberOfColumns - 1), 2.3f * (i / numberOfColumns - 1.5f));

        crd = card.GetComponent<Card>();

        crd.type = cards[i].type;
        crd.color = cards[i].color;
        crd.count = cards[i].count;
        crd.filling = cards[i].filling;
        crd.index = i;

        Debug.Log("Spawn");
        Instantiate(card, spawnPosition, spawnRotation);
    }

    void Regeneration()
    {
        int ii = 0; 
        for (int i = 0; i < cardCount; i++)
        {
            if (cards[i].framed)
            {
                GenCard(i);
                cards[i].framed = false;
                ii = ii * 10 + i;
                Debug.Log(ii);
            }
        }

        while (!SolvabilityTest())
        {
            GenCard(ii / 100);
            GenCard((ii / 10) % 10);
            GenCard(ii % 10);
        }

        CardSpawner(ii / 100);
        CardSpawner((ii / 10) % 10);
        CardSpawner(ii % 10);
    }

    public static bool IsEqualOrUnqual(int i, int j, int k)
    {
        return (((i == j) && (j == k)) || ((i != j) && (j != k) && (i != k)));
    }

    void GenCards()
    {
        for (int i = 0; i < cardCount; i++)
        {
            cards[i] = new Card();

            do
            {
                GenCard(i);
            } 
            while (cardBoard[cards[i].type, cards[i].color, cards[i].count, cards[i].filling] == true);

            cardBoard[cards[i].type, cards[i].color, cards[i].count, cards[i].filling] = true;
        }
    }

    void GenCard(int i)
    {
        System.Random rand = new System.Random();
        cards[i].type = rand.Next(3);
        cards[i].color = rand.Next(3);
        cards[i].count = rand.Next(3);
        cards[i].filling = rand.Next(3);
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
