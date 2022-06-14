using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject[] CardObjs = new GameObject[13];
    public Transform[] cardssectioncardsposition = new Transform[13];
    public Transform[] decksectioncardsposition = new Transform[13];
    public List<GameObject> players; //player cards destination(pos)
    public List<Transform> playerlerptransforms; //middel point of maincard pos and player destination (To make curve path)
    public Transform[] point1 = new Transform[4];  // Shuffel curve path points
    public Transform[] point2 = new Transform[4];  // Shuffel curve path points
    public Transform[] point3 = new Transform[4];  // Shuffel curve path points
    public Transform[] point4 = new Transform[4];  // Shuffel curve path points
    public Transform[] point5 = new Transform[4];  // Shuffel curve path points
    public Transform[] point6 = new Transform[4];  // Shuffel curve path points
    public List<Transform[]> transformlist = new List<Transform[]>();  // shuffel point cointainer
    public float cardanimdelay = 0.2f;
    public float shuffeldelay = 0.1f;
    public float playercardsvanishtime = 5f;
    public float animationspeed = 0.0045f;
    [Range(2, 6)] public int playercount = 2;

    private void Awake()
    {
        transformlist.Add(point1);
        transformlist.Add(point2);
        transformlist.Add(point3);
        transformlist.Add(point4);
        transformlist.Add(point5);
        transformlist.Add(point6);
    }

    private void Start()
    {
        StartCoroutine(VanishPlayerCards());
    }


    public void Shuffle()
    {
        StartCoroutine(Shuf());
    }

    // Extra Details for more cooler user experience
    IEnumerator Shuf()
    {
        GameObject[] firsttempobj = new GameObject[CardObjs.Length];
        System.Array.Copy(CardObjs, firsttempobj, CardObjs.Length);

        for (int i = 0; i < CardObjs.Length; i++)
        {
            int first = Random.Range(0, CardObjs.Length);
            int second = Random.Range(0, CardObjs.Length);

            if (first != second)
            {
                GameObject tempobj = CardObjs[first];
                CardObjs[first] = CardObjs[second];
                CardObjs[second] = tempobj;
            }
        }


        for (int j = 0; j < CardObjs.Length; j++)
        {
            int number = Random.Range(0, transformlist.Count - 1);
            Transform[] posarr = new Transform[transformlist[number].Length + 2];
            int num = 0;

            for (int i = 0; i < transformlist[number].Length + 2; i++)
            {
                if (i == 0) posarr[i] = firsttempobj[j].transform;
                else if (i == transformlist[number].Length + 1) posarr[i] = CardObjs[System.Array.IndexOf(CardObjs, firsttempobj[j])].transform;
                else { posarr[i] = transformlist[number][num]; num++; }
            }

            firsttempobj[j].GetComponent<CardAnimation>().AnimatePosition(posarr, true, firsttempobj[j].transform.localScale, animationspeed);

            yield return new WaitForSeconds(shuffeldelay);
        }
    }

    public void DistributeCards()
    {
        StartCoroutine(Distribute());
    }

    public IEnumerator Distribute()
    {
        // You can perform Slerp between main card pos and target pos
        //int incrementnum = 0;
        //for (int i = 0; i < 13; i++)
        //{
        //    for (int j = 0; j < playercount; j++)
        //    {
        //        CardObjs[incrementnum].transform.position = Vector3.Slerp(CardObjs[incrementnum].transform.position, players[j].transform.position, 0.5f);
        //    }
        //}

        // Like that perform Slerp on Rotation and Scale



        ///////////////////////////////////    OR    /////////////////////////////////////////////////////////////////////////



        // But for more Flexibility and modification we need to go a bit advanced

        int incrementnum = 0;

        for (int i = 0; i < 13; i++)
        {
            
            for (int j = 0; j < playercount; j++)
            {

                Transform[] temptransform = new Transform[] {
                    CardObjs[incrementnum].transform,
                    playerlerptransforms[j].transform,
                    players[j].transform.GetChild(i).gameObject.transform,
                };

                if (j == 0)
                {
                    CardObjs[incrementnum].GetComponent<CardAnimation>().AnimatePosition(temptransform, true, players[j].transform.GetChild(i).gameObject.transform.localScale, animationspeed, 0);
                }
                else
                {
                    CardObjs[incrementnum].GetComponent<CardAnimation>().AnimatePosition(temptransform, true, players[j].transform.GetChild(i).gameObject.transform.localScale, animationspeed);
                }
                incrementnum++;
            }

            yield return new WaitForSeconds(cardanimdelay);
        }

    }

    IEnumerator VanishPlayerCards()
    {
        yield return new WaitForSeconds(playercardsvanishtime);

        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < players[i].transform.childCount; j++)
            {
                Destroy(players[i].transform.GetChild(j).gameObject.GetComponent<MeshRenderer>());
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < testtransform.Length; i++)
    //    {
    //        Gizmos.DrawLine(testtransform[i].position, testtransform[i + 1].position);
    //    }
    //}
}
