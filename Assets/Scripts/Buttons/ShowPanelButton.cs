using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string PanelId;

    public PanelShowBehaviour Behaviour;

    public void DoShowPanel()
    {
        Debug.Log("DoShowPanel");
        UIManager.Instance.Show(PanelId, Behaviour);
    }
}
