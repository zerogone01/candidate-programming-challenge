using SimpleJSON;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class ManagerJson
{
    private string FilePath;
    private JSONNode JsonNode;
    public ManagerJson(string gameDataFileName)
    {
        FilePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        InitializeJsonNode();
    }

    public void InitializeJsonNode()
    {
        string dataAsJson = File.ReadAllText(FilePath);
        JsonNode = JSON.Parse(dataAsJson);
    }

    public String GetTitle()
    {
        return JsonNode["Title"].Value;
    }

    public List<String> GetNameColumns()
    {
        var jsonArray = JsonNode["ColumnHeaders"].AsArray;

        List<String> columnsName = new List<String>();
        if (jsonArray != null)
        {
            for (int i = 0; i < jsonArray.Count; i++)
            {
                columnsName.Add(jsonArray[i].Value);
            }
        }

        return columnsName;
    }

    public List<List<String>> GetAllData()
    {
        var allDataList = new List<List<String>>();
        var dataNode = JsonNode["Data"];
        for(int i = 0; i < dataNode.Count; i++)
        {
            allDataList.Add(GetDataRow(dataNode[i]));
        }

        return allDataList;
    }

    private List<String> GetDataRow(JSONNode dataNode)
    {
        var dataList = new List<String>();
   
        for (int j = 0; j < dataNode.Count; j++)
        {
            dataList.Add(dataNode[j]);
        }

        return dataList;
    }

}