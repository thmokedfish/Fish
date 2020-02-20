using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject InfoPanel;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //从摄像机发出到点击坐标的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject gameobj = hit.collider.gameObject;
                MainPanel.ShowData s = gameobj.GetComponent<DataNumber>().data;
                if (gameobj.tag == "Fish")
                {
                    MainPanel.SetActive(false);
                    InfoPanel.SetActive(true);
                    InfoPanel.GetComponent<InfoPanel>().SetText(s.briefInfo.ToString(), s.rarity.ToString(), s.info.ToString());
                }
            }
        }
    }
}
