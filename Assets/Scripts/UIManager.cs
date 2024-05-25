using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class UIManager : Singleton<UIManager>
{
    // showed panels list
    private List<PanelInstanceModel> listInstances = new List<PanelInstanceModel>();

    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = ObjectPool.Instance;
    }

    public void Show(string panelId, PanelShowBehaviour behaviour = PanelShowBehaviour.KEEP_PREVIOUS)
    {
        GameObject panelInstance = objectPool.GetObjectFromPool(panelId);

        if(panelInstance != null)
        {
            if(behaviour == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelsInList() > 0)
            {
                var lastPanel = GetLastPanel();
                if(lastPanel != null)
                {
                    lastPanel.PanelInstance.SetActive(false);
                }
            }

            listInstances.Add(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = panelInstance
            });
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

            objectPool.PoolObject(lastPanel.PanelInstance);

            if(GetAmountPanelsInList() > 0)
            {
                lastPanel = GetLastPanel();
                if(lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                {
                    lastPanel.PanelInstance.SetActive(true);
                }
            }
        }
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
