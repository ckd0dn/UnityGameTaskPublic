using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Start()
    {
        Managers.CSV.LoadCSVData();

        Managers.Resource.LoadAllAsync<Sprite>("Sprites", null);

        Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                StartLoad();
            }
        });
    }

    void StartLoad()
    {
        var player = Managers.Object.Spawn<PlayerController>("Player.prefab");
        var map = Managers.Resource.Instantiate("Map.prefab");
        var monster = Managers.Object.Spawn<MonsterController>("Skeleton.prefab");

    }
}
