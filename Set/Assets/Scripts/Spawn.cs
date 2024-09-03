using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            
            for (int i = 0; i < cardCount; i++) // Генерация карт
            {
                cards[i] = new Card();
                GenCards(i);
            }
        }
        while (!SolvabilityTest()); // Проверка на решаемость

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
        Vector3 spawnPosition = new Vector2(2 * (i % numberOfColumns - 1), 2.3f * (i / numberOfColumns - 1.5f));

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
}
