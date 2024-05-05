using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<PanelModel> Panels;
    private Queue<PanelInstanceModel> queue = new Queue<PanelInstanceModel>();

    public void Show(string panelId)
    {
        PanelModel panelModel = Panels.FirstOrDefault(panel => panel.PanelId == panelId);
        Debug.Log("Show " + panelId);

        if(panelModel != null)
        {
            Debug.Log(panelModel.PanelId);
            
            var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);

            queue.Enqueue(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = newInstancePanel
            });
        }
        else
        {

        }
    }
}
