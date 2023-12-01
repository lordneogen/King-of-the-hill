using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
public class SceneScript : MonoBehaviour,ISceneScript
{
    // Start is called before the first frame update

    [FormerlySerializedAs("PauseMenu")] [FormerlySerializedAs("pauseMenu")] public GameObject PauseUI;
    [FormerlySerializedAs("DraftUI")] [FormerlySerializedAs("craftUI")] public GameObject CraftUI;
    public GameObject CraftUIScroll;
    public GameObject ChessUI;
    public GameObject ItemCeil;
    public GameObject ItemCeilCraft;
    public List<Item> Items;
    public bool Chess = false;
    [FormerlySerializedAs("ispaused")] public bool Pause=false;
    [FormerlySerializedAs("craft")] public bool Craft=false;
    [Inject]
    private Player player;
    void Start()
    {
        PauseUI.SetActive(false);
        CraftUI.SetActive(false);
        // CraftUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(player!=null);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause = !Pause;
            if (Pause)
            {
                PauseUI.SetActive(true);
            }
            else
            {
                PauseUI.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Craft = !Craft;
            if (Craft)
            {
                CraftUI.SetActive(true);
            }
            else
            {
                CraftUI.SetActive(false);
            }
        }

        if (Craft || Pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }

    public void PauseGame()
    {
        Pause = true;
    }

    public void ResumeGame()
    {
        Pause = false;
    }

    public GameObject GetItemCeil()
    {
        return this.ItemCeil;
    }
}
