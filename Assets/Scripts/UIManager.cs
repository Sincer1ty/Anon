using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    public List<PanelModel> Panels;
    // showed panels list
    private List<PanelInstanceModel> listInstances = new List<PanelInstanceModel>();
    // instantiated panels list
    private List<GameObject> panelObjects = new List<GameObject>();

    public void Show(string panelId, PanelShowBehaviour behaviour = PanelShowBehaviour.KEEP_PREVIOUS)
    {
        PanelModel panelModel = Panels.FirstOrDefault(panel => panel.PanelId == panelId);
        
        if (panelModel != null)
        {
            Debug.Log(panelModel.PanelId);

            var instance = panelObjects.FirstOrDefault(obj => obj.name == panelId+ "(Clone)");

            if (behaviour == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelsInList() > 0)
            {
                var lastPanel = GetLastPanel();
                if (lastPanel != null)
                {
                    lastPanel.PanelInstance.SetActive(false);
                }
            }

            if (instance != null)
            {
                Debug.Log("持失喫 : "+instance);

                if(!instance.activeSelf)
                {
                    Debug.Log("list 持失");
                    AddInstancePanel(panelId, instance);
                }
                instance.SetActive(true);
            }
            else
            {
                var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);

                AddInstancePanel(panelId, newInstancePanel);

                panelObjects.Add(newInstancePanel);
            }
        }
        else
        {
            Debug.LogWarning($"Trying to use panelId = {panelId}, but this is not found in Panels");
        }
    }

    public void Close()
    {
        if(AnyPanelShowing())
        {
            var lastPanel = GetLastPanel();

            listInstances.Remove(lastPanel);

            lastPanel.PanelInstance.SetActive(false);

            if (GetAmountPanelsInList() > 0)
            {
                lastPanel = GetLastPanel();
                if(lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                {
                    lastPanel.PanelInstance.SetActive(true);
                }
            }
        }
    }

    void AddInstancePanel(string panelId, GameObject obj)
    {
        listInstances.Add(new PanelInstanceModel
        {
            PanelId = panelId,
            PanelInstance = obj
        });
    }

    PanelInstanceModel GetLastPanel()
    {
        return listInstances[listInstances.Count - 1];
    }
    private bool AnyPanelShowing()
    {
        return GetAmountPanelsInList() > 0;
    }

    private int GetAmountPanelsInList()
    {
        return listInstances.Count;
    }
}
