using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyBox;
public class UIManager : Singleton<UIManager>
{
    public List<PanelModel> Panels;
    private List<PanelInstanceModel> listInstances = new List<PanelInstanceModel>();

    public void Show(string panelId)
    {
        PanelModel panelModel = Panels.FirstOrDefault(panel => panel.PanelId == panelId);
        Debug.Log("Show " + panelId);

        if (panelModel != null)
        {
            Debug.Log(panelModel.PanelId);

            var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);

            listInstances.Add(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = newInstancePanel
            });
        }
        else
        {
            Debug.LogWarning($"Trying to use panelId = {panelId}, but this is not found in Panels");
        }
    }
}
