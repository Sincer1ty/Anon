using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.UI;
using TMPro;
public class DialogueMgr : Singleton<DialogueMgr>
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI text;
    public Image rendererSprite;
    public Image rendererDialogueWindow;

    private Dialogue curDialogueData ;
   

    private int count;

    public bool talking = false; // ��ȭ�� �̷����� ���� ���� �Է��� ����
    private bool keyActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        Name.text = "";
        text.text = "";
        curDialogueData = new Dialogue();
        curDialogueData.listName=new List<string>();
        curDialogueData.listSentences = new List<string>();
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
        }

        StartCoroutine(StartDialogueCoroutine());
    }
    public void ExitDialogue()
    {
        text.text = "";
        Name.text = "";
        count = 0;
        curDialogueData = null;
        curDialogueData = new Dialogue();
        curDialogueData.listName = new List<string>();
        curDialogueData.listSentences = new List<string>();
        talking = false;
    }
    IEnumerator StartDialogueCoroutine()
    {
        Name.text += curDialogueData.listName[count];
        
        keyActivated = true;
        for (int i = 0; i < curDialogueData.listSentences[count].Length; i++)
        {
            text.text += curDialogueData.listSentences[count][i]; // 1���ھ� ���.

            yield return new WaitForSeconds(0.05f);
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