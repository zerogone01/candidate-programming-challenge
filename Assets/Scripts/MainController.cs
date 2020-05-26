using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    private GameObject Row;
    private string GameDataFileName = "JsonChallenge.json";
    private Transform DataTable;
    ManagerJson ManagerJson;
    Canvas Canvas;
    // Start is called before the first frame update
    void Start()
    {

        Canvas = GameObject.FindObjectOfType<Canvas>();
        DataTable = Canvas.transform.Find("Panel").Find("Table");
        ManagerJson = new ManagerJson(GameDataFileName);
        Button updateButton = GameObject.FindGameObjectWithTag("UpdateButton").GetComponent<Button>();
        updateButton.onClick.AddListener(() => {
            ManagerJson.InitializeJsonNode();
            RemoveContent();
            InitTable();
        });

        InitTable();
    }

    private void InitTable()
    {
        SetTitleText();
        SetColumnsName();
        SetDataRows();
    }

    private void RemoveContent()
    {
        DataTable.Clear();
    }

    private void UpdateContent()
    {
        SetTitleText();
        SetColumnsName();
        SetDataRows();
    }

    private void SetTitleText()
    {
        var title = Canvas.transform.Find("Panel").Find("Title").GetChild(0).GetComponent<Text>();
        title.text = ManagerJson.GetTitle();
    }

    private void SetColumnsName()
    {
        var row = Instantiate(Resources.Load("Row") as GameObject, DataTable);
        var listColumnsName = ManagerJson.GetNameColumns();

        for (int i = 0; i < listColumnsName.Count; i++)
        {
            var value = Instantiate(Resources.Load("NameColumn") as GameObject);
            value.GetComponent<Text>().text = listColumnsName[i];
            value.GetComponent<Text>().fontStyle = FontStyle.Bold;
            value.GetComponent<Text>().fontSize = 70;
            value.transform.SetParent(row.transform);
        }

        row.gameObject.name = "Header";

    }

    private void SetDataRows()
    {
        var allData = ManagerJson.GetAllData();
        for (int i = 0; i < allData.Count; i++)
        {
            var dataRow = allData[i];
            var row = Instantiate(Resources.Load("Row") as GameObject, DataTable);
            for (int j = 0; j < dataRow.Count; j++)
            {
                var value = Instantiate(Resources.Load("NameColumn") as GameObject);
                value.GetComponent<Text>().text = dataRow[j];
                value.transform.SetParent(row.transform);
            }
        }
    }

}
