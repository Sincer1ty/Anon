using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.VisualScripting;
using MyBox;
using UnityEditor;
public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    private List<string> dialogueKeys;
    private Dictionary<string, bool> DialogueCheckDictionary;
    private Dictionary<string, Dialogue> DialogueDatas;
    // Start is called before the first frame update
    void Start()
    {
        DialogueCheckDictionary = new Dictionary<string, bool>();
        DialogueDatas=new Dictionary<string, Dialogue>();
        InitializeDialogueData();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DialogueMgr.Instance.ShowDialogue("Test");
        }
    }
    void InitializeDialogueData()
    {
        // 이 부분 때문에 씬 로드 들어갈 때 비동기 로드처리 해야될 거 같습니다.
        for(int i=0;i<dialogueKeys.Count;i++)
        {
            List<Dictionary<string, object>> data = CSVReader.Read(dialogueKeys[i]);
            Dialogue dialogue = new Dialogue();
            dialogue.listName=new List<string>();
            dialogue.listSentences=new List<string>();
            for (int j = 0; j < data.Count; j++)
            {
                //Debug.Log("index " + (i).ToString() + " : " + data[i]["Starttime"] + " " + data[i]["Location"] + " " + data[i]["Kind"]);
                dialogue.listName.Add(data[j]["Name"].ToString());
                dialogue.listSentences.Add(data[j]["Sentence"].ToString());
            }
            DialogueDatas[dialogueKeys[i]]= dialogue;
            DialogueCheckDictionary[dialogueKeys[i]] = false;
        }
    }
    public Dialogue DialogueValid(string s)
    {
        if (DialogueCheckDictionary[s] == true)
            return null;
        else
        {
            DialogueCheckDictionary[s] = true;
            return DialogueDatas[s];
        }
    }
}
