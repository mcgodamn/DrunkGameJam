using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatSystemController : MonoBehaviour
{
    public static EatSystemController instance;
    [SerializeField]
    private GameObject vomitObj;

    [SerializeField]
    private AudioSource eatingSound;
    [SerializeField]
    private AudioSource snoringSound;

    [SerializeField]
    private List<Image> images;
    private Dictionary<string, string> EvolutionTable = new Dictionary<string, string>();
    private SortedSet<string> eatSet = new SortedSet<string>();
    [SerializeField]
    private Sprite UIMask;

    private int WeedCnt = 0;

    public List<int> propAppearSequence = new List<int>();
    public List<GameObject> propPrefabs;

    private Vector2 mouthPosition;
    private int BlockCnt = 0;
    // Use this for initialization
    void Start()
    {
        instance = this;
        /*
        eatSet.Add("05");
        eatSet.Add("08");
        eatSet.Add("06");
        eatSet.Add("06");
        eatSet.Add("12");
        */
        ConstuctHashTable();
        //print(Evolution());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ConstuctHashTable()
    {
        string[] lines = System.IO.File.ReadAllLines("HashTable/table.txt");
        foreach (string line in lines)
        {
            string key = line.Substring(0, 6);
            string value = line.Substring(7);
            EvolutionTable[key] = value;
        }
    }

    private string CombineEatSet()
    {
        string sortedStr = "";

        foreach (string i in eatSet)
        {
            sortedStr += i;
        }
        return sortedStr;
    }


    public void Eat(string s, Image img)
    {
        if(BlockCnt>=3)
        {
            print("full");
            return;
        }
        eatingSound.Play();
        images[BlockCnt].sprite = img.sprite;
        BlockCnt++;
        eatSet.Add(s);
        propAppearSequence.Add(int.Parse(s));
    }

    public void Evolution()
    {
        string combinationStr = CombineEatSet();
        if (combinationStr.Length == 0) return;
        if (combinationStr.Length == 2) //只有一個數，傳回自己
        {
            print(combinationStr);
            eatSet.Clear();
            ClearBlock();
            VomitPrefab(combinationStr);
            return;// combinationStr;
        }
        else if (combinationStr.Length == 4) //兩個數，前面加0再hash
        {
            combinationStr = "00" + combinationStr;
        }
        string ans = "";
        if (EvolutionTable.ContainsKey(combinationStr))
        {
            ans = EvolutionTable[combinationStr];
        }
        else
        {
            string last = combinationStr.Substring(combinationStr.Length - 2);
            string small = RandomSamllNumber(last);
            ans = small;
        }
        CoroutineUtility.instance.Do().Then(() =>
        {
            Guy.mouthType = GuyMouthType.MOUTH_CLOSE;
        }).Wait(0.5f).Then(() =>
        {
            Guy.mouthType = GuyMouthType.MOUTH_PUKE;
        });
        eatSet.Clear();
        ClearBlock();
        VomitPrefab(ans);
        return;// ans;
    }

    private void ClearBlock()
    {
        foreach (Image img in images)
        {
            img.sprite = UIMask;
        }
        BlockCnt = 0;

    }

    private string RandomSamllNumber(string max)
    {
        int m = int.Parse(max);
        int rand = Random.Range(1, m);
        string s = "";
        if (rand < 10)
        {
            s += "0";
        }
        s += rand;
        return s;
    }

    public void OnPropDragging(PropController prop)
    {
        if (!MainController.GetInstance().isGameStarted)
        {
            MainController.GetInstance().isGameStarted = true;
        }
        Guy.mouthType = GuyMouthType.MOUTH_OPEN;
    }

    public void OnPropEndDragging(PropController prop)
    {
        Guy.mouthType = GuyMouthType.MOUTH_NORMAL;
        Debug.Log("Mouse Type Normal");
    }

    public void OnDrinkingAlcohol()
    {
        //play drink aniamtion and wait
        //do evolution
        Evolution();
    }

    private void CreatePrefab(string id)
    {
        int i = int.Parse(id);
        bool blech = propPrefabs[i].GetComponent<PropController>().isBelch;
        if (blech)
        {
            CreateBlech();
            return;
        }
        if(WeedCnt%3==2)
        {
            GameObject obb = Instantiate(propPrefabs[44], MainController.GetInstance().canvas.transform);
            obb.GetComponent<PropController>().InstantiateAnimation();
        }
        propAppearSequence.Add(i);
        GameObject obj = Instantiate(propPrefabs[i], MainController.GetInstance().canvas.transform);
        obj.GetComponent<PropController>().InstantiateAnimation();
    }

    private void CreateBlech()
    {
        CoroutineUtility.instance.Do()
        .Then(() =>
        {
            Guy.mouthType = GuyMouthType.MOUTH_DAGER;
            snoringSound.Play();
        }).Wait(0.8f)
        .Then(() =>
        {
            Guy.mouthType = GuyMouthType.MOUTH_NORMAL;
        }).Go();
    }

    private void VomitPrefab(string id)
    {
        WeedCnt++;

        //enable vomit
        CoroutineUtility.instance.Do()
        .Then(() =>
        {
            vomitObj.SetActive(true);
        }).Wait(1.2f)
        .Then(() =>
        {
            vomitObj.SetActive(false);
        }).Go();
        CreatePrefab(id);
    }
}
