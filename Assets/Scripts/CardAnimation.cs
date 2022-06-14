using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    public Manager manager;
    public float animspeed = 0.0045f;
    private float fastsecond = 0f;
    private bool islearping = false;
    private bool islearpingonstart = false;
    private Vector3 sceanstartpos;
    private Vector3 targetpos;
    private int numberofcardhalfrotations = 1;
    private Vector3 lastvector;
    private Vector3 cardlerpsizeafter;
    private Vector3 cardlerpsizeorigional;
    public GameObject priviousanimobj;
    private List<Transform> transformlist = new List<Transform>();
    private List<Transform[]> transformArrayList = new List<Transform[]>();

    void Start()
    {
        cardlerpsizeorigional = gameObject.transform.localScale;
        targetpos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(2, 10), Random.Range(-10, 10));
        sceanstartpos = gameObject.transform.position;
        islearpingonstart = true;
    }


    void Update()
    {

        fastsecond += animspeed;

        if (fastsecond > 1)
        {
            fastsecond = 1;
        }


        //this loops perform lerf in pyramid objects
        // first layer's 1-2 lerp value put in second layers 1 obj, fl 2-3 => sl 2, fl 3-4 => sl 3........... like that
        // and we get result which we wanted
        // and we can edit curve path in runtime also
        if (islearping)
        {
            if(fastsecond == 1)
            {
                islearping = false;
            }

            for (int i = 0; i < transformArrayList.Count - 1; i++)
            {
                int length = transformArrayList[i + 1].Length;
                for (int j = 0; j < length; j++)
                {
                    transformArrayList[i + 1][j].position = Vector3.Lerp(transformArrayList[i][j].position, transformArrayList[i][j + 1].position, fastsecond);
                }
            }

            gameObject.transform.position = transformArrayList[transformArrayList.Count - 1][transformArrayList[transformArrayList.Count - 1].Length - 1].position;

            gameObject.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(90, transform.localRotation.y, transform.localRotation.z)
                , Quaternion.Euler(90, transform.localRotation.y, 180 * numberofcardhalfrotations), fastsecond);

            gameObject.transform.localScale = Vector3.Lerp(cardlerpsizeorigional, cardlerpsizeafter, fastsecond);
        }


        //simple Starting Lerp for card itself
        if(islearpingonstart)
        {
            if(gameObject.transform.position == targetpos)
            {
                islearpingonstart = false;
            }
            gameObject.transform.position = Vector3.Slerp(sceanstartpos, targetpos, fastsecond);
        }
    }



    // Bottom function create Objects in pyramid form
    // transform.length => * * * * *
    //     length - 1       * * * *
    //            - 2        * * *
    //            - 3         * *
    //                         *



    public void AnimatePosition(Transform[] Positionarr, bool bol, Vector3 size, float animationspeed,int numberofcardhalfrotations = 1)
    {
        this.numberofcardhalfrotations = numberofcardhalfrotations;
        lastvector = Positionarr[Positionarr.Length - 1].position;
        animspeed = animationspeed;
        cardlerpsizeafter = size;

        if (priviousanimobj != null)
        {
            Destroy(priviousanimobj);
        }
        priviousanimobj = new GameObject("tempanimobj");

        transformArrayList.Clear();
        islearpingonstart = false;


        for (int i = 0; i < (Positionarr.Length); i++)
        {
            Transform[] tr = new Transform[Positionarr.Length - i];
            for (int j = 0; j < Positionarr.Length - i; j++)
            {
                if(i == 0)
                {
                    GameObject obj = new($"{i}{j}");
                    obj.transform.parent = priviousanimobj.transform;
                    obj.transform.position = Positionarr[j].position;
                    tr[j] = obj.transform;
                }
                else
                {
                    GameObject obj = new($"{i}{j}");
                    obj.transform.parent = priviousanimobj.transform;
                    tr[j] = obj.transform;
                }
            }

            transformArrayList.Add(tr);
        }

        islearping = bol;
        fastsecond = 0f;
    }
}
