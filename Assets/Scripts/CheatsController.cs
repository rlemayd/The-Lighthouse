using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatsController : MonoBehaviour
{
    bool showConsole = false;
    string codeBuffer = "";
    CheatCodeManager cheatCodeManager;

    public List<object> cheatList;

    private void Awake()
    {
        cheatCodeManager = new CheatCodeManager();
        // Register cheats
        cheatCodeManager.RegisterCheat(new CheatCode("level1", GoToLevelOne));
        cheatCodeManager.RegisterCheat(new CheatCode("killmaria", KillMaria));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            showConsole = !showConsole;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnReturn();
        }
    }

    private void OnGUI()
    {
        if (!showConsole) return;

        if (Event.current.Equals(Event.KeyboardEvent("return"))) OnReturn();
        GUI.Box(new Rect(0, 0, Screen.width, 30), "");
        codeBuffer = GUI.TextField(new Rect(10, 0, Screen.width - 10, 20), codeBuffer);

    }

    private void OnReturn()
    {
        if (showConsole)
        {
            HandleConsoleInput();
            codeBuffer = "";
        }
    }

    private void HandleConsoleInput()
    {
        cheatCodeManager.InvokeCheat(codeBuffer);
    }

    private void GoToLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    private void KillMaria()
    {
        GameObject maria = GameObject.FindGameObjectWithTag("Maria");
        maria.SetActive(false);
    }
}

public class CheatCode
{
    public Action action;
    public string code;
    

    public CheatCode(string code, Action action)
    {
        this.action = action;
        this.code = code;
    }

    
}

public class CheatCodeManager
{
    private Dictionary<string, Action> cheatDictionary = new Dictionary<string, Action>();

    public CheatCodeManager()
    {

    }

    public void InvokeCheat(string cheatCode)
    {
        if (cheatDictionary.ContainsKey(cheatCode))
        {
            cheatDictionary[cheatCode].Invoke();
        }
    }

    public void RegisterCheat(CheatCode cheat)
    {
        if (!cheatDictionary.ContainsKey(cheat.code))
        {
            cheatDictionary.Add(cheat.code, cheat.action);
        }
    }
}
