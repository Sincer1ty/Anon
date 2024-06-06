using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.UI;
using TMPro;
public class DialogueMgr : Singleton<DialogueMgr>
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI RightName;
    public TextMeshProUGUI text;
    public Image rendererSprite;
    public Image rendererDialogueWindow;

    private Dialogue curDialogueData ;
    [SerializeField]
    private float DialogueTermTime = 0.05f;
    private WaitForSeconds WaitForSeconds;
   

    private int count;

    public bool talking = false; // 대화가 이뤄지지 않을 때는 입력을 막음
    private bool keyActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        Name.text = "";
        RightName.text = "";
        text.text = "";
        curDialogueData = new Dialogue();
        curDialogueData.listName=new List<string>();
        curDialogueData.listSentences = new List<string>();
        curDialogueData.listLocations = new List<string>();
        WaitForSeconds = new WaitForSeconds(DialogueTermTime);
        //this.gameObject.SetActive(false);
    }

    
    public void ShowDialogue(string dialoguekey)
    {
        Dialogue dialogue = DataManager.Instance.DialogueValid(dialoguekey);
        talking = true;
        if (dialogue == null)
            return;
        for (int i = 0; i < dialogue.listSentences.Count; i++)
        {
            curDialogueData.listName.Add(dialogue.listName[i]);
            curDialogueData.listSentences.Add(dialogue.listSentences[i]);
            curDialogueData.listLocations.Add(dialogue.listLocations[i]);
        }

        StartCoroutine(StartDialogueCoroutine());
    }
    public void ExitDialogue()
    {
        text.text = "";
        Name.text = "";
        RightName.text = "";
        count = 0;
        curDialogueData = null;
        curDialogueData = new Dialogue();
        curDialogueData.listName = new List<string>();
        curDialogueData.listSentences = new List<string>();
        talking = false;
    }
    IEnumerator StartDialogueCoroutine()
    {
        if (curDialogueData.listLocations[count]=="L")
        {
            Name.text += curDialogueData.listName[count];
            RightName.text = "";
        }
        else
        {
            RightName.text += curDialogueData.listName[count];
            Name.text = "";
        }
        
        keyActivated = true;
        for (int i = 0; i < curDialogueData.listSentences[count].Length; i++)
        {
            text.text += curDialogueData.listSentences[count][i]; // 1글자씩 출력.

            yield return WaitForSeconds;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (text.text.Length == curDialogueData.listSentences[count].Length)
                {
                    keyActivated = false;
                    count++;
                    Name.text = "";
                    text.text = "";


                    if (count == curDialogueData.listSentences.Count)
                    {
                        StopAllCoroutines();
                        ExitDialogue();
                        this.gameObject.SetActive(false);
                    }
                    else
                    {
                        StopAllCoroutines();
                        StartCoroutine(StartDialogueCoroutine());
                    }
                }
                else
                {
                    text.text = curDialogueData.listSentences[count];
                }
                
            }
        }
    }
}
