using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CsvParser 
{
    [SerializeField] private string csvAddress = "SampleMonster.csv"; // Addressable �̸�
    public List<string[]> Data { get; private set; } = new List<string[]>();
    public Action OnLoaded;

    // CSV ������ Addressable�� ���� �񵿱������� �ε��ϴ� �Լ�
    public void LoadCSVData()
    {
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(csvAddress);

        // �ε� �Ϸ� �� ó��
        handle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                // �ε�� CSV �����͸� �ؽ�Ʈ�� ����
                string csvContent = op.Result.text;

                // CSV �����͸� �Ľ��Ͽ� ó��
                List<string[]> parsedData = ParseCSV(csvContent);
                Data = parsedData;
                OnLoaded?.Invoke();
            }
            else
            {
                Debug.LogError("CSV ���� �ε� ����");
            }
        };
    }

    // CSV �����͸� �Ľ��ϴ� �Լ�
    private List<string[]> ParseCSV(string csvContent)
    {
        List<string[]> parsedData = new List<string[]>();

        // �� �ٲ����� ���е� �� ó��
        string[] rows = csvContent.Split('\n');
        foreach (string row in rows)
        {
            // �� ���� ��ǥ�� ����
            string[] columns = row.Split(',');
            parsedData.Add(columns);
        }

        return parsedData;
    }
}
