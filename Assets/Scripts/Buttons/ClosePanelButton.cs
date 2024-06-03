using UnityEngine;

public class ClosePanelButton : MonoBehaviour
{
    public void DoClosePanel()
    {
        UIManager.Instance.Close();
    }
}
