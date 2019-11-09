using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatSystemController : MonoBehaviour {
    public static EatSystemController instance;
    [SerializeField]
    private List<Image> images;
    private Dictionary<string, string> EvolutionTable = new Dictionary<string, string>();
    private SortedSet<string> eatSet = new SortedSet<string>();

    private int BlockCnt = 0;
	// Use this for initialization
	void Start () {
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
	void Update () {
		
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


    public void Eat(string s,Image img)
    {
        print(BlockCnt);
        if(BlockCnt>=3)
        {
            print("full");
            return;
        }
        images[BlockCnt].sprite = img.sprite;
        BlockCnt++;
        eatSet.Add(s);
    }

    public string Evolution()
    {
        string combinationStr = CombineEatSet();
        if(combinationStr.Length==2) //只有一個數，傳回自己
        {
            eatSet.Clear();
            ClearBlock();
            return combinationStr;
        }
        else if(combinationStr.Length==4) //兩個數，前面加0再hash
        {
            combinationStr = "00" + combinationStr;
        }
        string ans="";
        if ( EvolutionTable.ContainsKey(combinationStr) )
        {
            ans = EvolutionTable[combinationStr];
        }
        else
        {
            string last = combinationStr.Substring(combinationStr.Length - 2);
            string small = RandomSamllNumber(last);
            ans = small;
        }
        eatSet.Clear();
        ClearBlock();
        return ans;
    }

    private void ClearBlock()
    {
        foreach (Image img in images)
        {
            img.sprite = null;
        }
        BlockCnt = 0;

    }

    private string RandomSamllNumber(string max)
    {
        int m = int.Parse(max);
        int rand = Random.Range(1, m);
        string s = "";
        if (rand<10)
        {
            s += "0";
        }
        s += rand;
        return s;
    }

}
