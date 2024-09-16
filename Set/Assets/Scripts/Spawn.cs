using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public GameObject card;

    public static int cardCount = 12;
    public int numberOfColumns = 3;

    private HashSet<Vector4> cardSet = new HashSet<Vector4>(); // Множество, для уникальности карт

    public static Card[] cards = new Card[cardCount]; // Массив карт

    // Start is called before the first frame update
    void Start()
    {
        do // Генерация массива с картами
        {
            cardSet = new HashSet<Vector4>(); // Обнуление множества, в случае неудачной генерации

            //GeneralCardGen();

            for (int i = 0; i < cardCount; i++) // Генерация карт
            {
                cards[i] = new Card();
                GenCards(i);

                //if (cards[i] == null)
                //{
                //    cards[i] = new Card();
                //    GenCards(i);
                //}
            }
        }
        while (!SolvabilityTest()); // Проверка на решаемость

        cardSet = new HashSet<Vector4>(); // Обнуление множества, в случае неудачной генерации

        //GeneralCardGen();

        //for (int i = 0; i < cardCount; i++) // Генерация карт
        //{
        //    //cards[i] = new Card();
        //    //GenCards(i);

        //    if (cards[i] == null)
        //    {
        //        cards[i] = new Card();
        //        GenCards(i);
        //    }
        //}

        for (int i = 0; i < cardCount; i++) // Отрисовка карт
        {
            CardSpawner(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Logic.win_or_loose == 1) // В случе победы: Вызов регенерации и сброс победы на неопределенность
        {
            Regeneration();

            Logic.win_or_loose = 0;
        }

        if (Logic.win_or_loose == 2) // В случае порожения: Сброс поражения на неопределенность
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
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void CardSpawner(int i)
    {
        Vector3 spawnPosition = new Vector2
            (2 * (i % numberOfColumns - 1), 2.3f * (i / numberOfColumns - 1.5f));

        Card crd = card.GetComponent<Card>(); // Получение компоненты геймобжекта, для присваивания значений

        crd.type = cards[i].type;
        crd.color = cards[i].color;
        crd.count = cards[i].count;
        crd.filling = cards[i].filling;
        crd.index = i;

        Instantiate(card, spawnPosition, Quaternion.identity); // Сам спавн карты
    }

    void Regeneration()
    {
        List<int> ints = new List<int>(); // Лист индексов карт для регенерации

        for (int i = 0; i < cardCount; i++)
        {
            if (cards[i].framed)
            {
                cardSet.Remove(new Vector4(cards[i].type, cards[i].color, cards[i].count, cards[i].filling)); // удаление карты из множества

                GenCards(i); // генерация новой карты

                cards[i].framed = false;

                ints.Add(i);
            }
        }

        while (!SolvabilityTest()) // Проверка новой колоды на решаемость
        {
            foreach (int i in ints)
            {
                cardSet.Remove(new Vector4(cards[i].type, cards[i].color, cards[i].count, cards[i].filling));
            }

            foreach (int i in ints)
            {
                GenCards(i);
            }
        }

        foreach (int i in ints) // Спавн новых 3 карт
        {
            CardSpawner(i);
        }
    }

    public static bool IsEqualOrUnqual(int i, int j, int k)
    {
        return (((i == j) && (j == k)) || ((i != j) && (j != k) && (i != k)));
    }

    void GenCards(int i)
    {
        do
        {
            GenCard(i);
        }
        while (cardSet.Contains(new Vector4(cards[i].type, cards[i].color, cards[i].count, cards[i].filling)));

        cardSet.Add(new Vector4(cards[i].type, cards[i].color, cards[i].count, cards[i].filling));
    }

    void GenCard(int i)
    {
        System.Random rand = new System.Random();
        cards[i].type = rand.Next(3);
        cards[i].color = rand.Next(3);
        cards[i].count = rand.Next(3);
        cards[i].filling = rand.Next(3);
    }

    bool SolvabilityTest() // Проверяет, что есть хотябы 1 решаемый вариант
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

    void GeneralCardGen()
    {
        System.Random rand = new System.Random();
        int i, j, k;
        do
        {
            i = rand.Next(12); 
            j = rand.Next(12); 
            k = rand.Next(12);
        } 
        while (i == j || i == k || j == k);

        cards[i] = new Card();
        cards[j] = new Card();
        cards[k] = new Card();

        GenCards(i);
        GenCards(j);

        do
        {
            SolvedColumn(cards[i].type, cards[j].type, k, 0);
            SolvedColumn(cards[i].color, cards[j].color, k, 1);
            SolvedColumn(cards[i].count, cards[j].count, k, 2);
            SolvedColumn(cards[i].filling, cards[j].filling, k, 3);

            //if (cards[i].type == cards[j].type)
            //{
            //    cards[k].type = cards[i].type;
            //}
            //else
            //{
            //    for (int l = 0; l < 3; l++)
            //    {
            //        if (cards[i].type != l && cards[j].type != l)
            //        {
            //            cards[k].type = l;
            //            break;
            //        }
            //    }
            //}

            //if (cards[i].color == cards[j].color)
            //{
            //    cards[k].color = cards[i].color;
            //}
            //else
            //{
            //    for (int l = 0; l < 3; l++)
            //    {
            //        if (cards[i].color != l && cards[j].color != l)
            //        {
            //            cards[k].color = l;
            //            break;
            //        }
            //    }
            //}

            //if (cards[i].count == cards[j].count)
            //{
            //    cards[k].count = cards[i].count;
            //}
            //else
            //{
            //    for (int l = 0; l < 3; l++)
            //    {
            //        if (cards[i].count != l && cards[j].count != l)
            //        {
            //            cards[k].count = l;
            //            break;
            //        }
            //    }
            //}

            //if (cards[i].filling == cards[j].filling)
            //{
            //    cards[k].filling = cards[i].filling;
            //}
            //else
            //{
            //    for (int l = 0; l < 3; l++)
            //    {
            //        if (cards[i].filling != l && cards[j].filling != l)
            //        {
            //            cards[k].filling = l;
            //            break;
            //        }
            //    }
            //}

        }
        while (cardSet.Contains(new Vector4(cards[k].type, cards[k].color, cards[k].count, cards[k].filling)));

        cardSet.Add(new Vector4(cards[k].type, cards[k].color, cards[k].count, cards[k].filling));

        Debug.Log(cards[i].type + ", " + cards[i].color + ", " + cards[i].count + ", " + cards[i].filling);
        Debug.Log(cards[j].type + ", " + cards[j].color + ", " + cards[j].count + ", " + cards[j].filling);
        Debug.Log(cards[k].type + ", " + cards[k].color + ", " + cards[k].count + ", " + cards[k].filling);

        //if (cards[i].type == cards[j].type)
        //{
        //    cards[k].type = cards[i].type;
        //}
        //else
        //{
        //    for (int l = 0; l < 3; l++) 
        //    {
        //        if (cards[i].type != l && cards[j].type != l)
        //        {
        //            cards[k].type = l;
        //            break;
        //        }
        //    }
        //}
    }

    void SolvedColumn(int i, int j, int k, int who)
    {
        if (i == j)
        {
            SetCardValue(i, k, who);
        }
        else
        {
            for (int l = 0; l < 3; l++)
            {
                if (i != l && j != l)
                {
                    SetCardValue(l, k, who);
                    break;
                }
            }
        }
    }

    void SetCardValue(int value, int index, int who)
    {
        switch (who)
        {
            case 0:
                cards[index].type = value; break;
            case 1:
                cards[index].color = value; break;
            case 2:
                cards[index].count = value; break;
            case 3:
                cards[index].filling = value; break;
        }
    }

    void SetCardValues(int index, int x, int y, int z, int w)
    {
        cards[index].type = x;
        cards[index].color = y;
        cards[index].count = z;
        cards[index].filling = w;
    }
}
