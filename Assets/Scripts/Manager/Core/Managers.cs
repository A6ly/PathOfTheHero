using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    static Managers Instance { get { Init(); return instance; } }

    DataManager dataManager = new DataManager();
    SoundManager soundManager = new SoundManager();
    PoolManager poolManager = new PoolManager();

    public static DataManager Data { get { return Instance.dataManager; } }
    public static SoundManager Sound { get { return Instance.soundManager; } }
    public static PoolManager Pool { get { return Instance.poolManager; } }

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
            }

            instance = Utils.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);

            instance.dataManager.Init();
            instance.soundManager.Init();
            instance.poolManager.Init();
        }
    }

    public static void Clear()
    {
        Data.Save();
        Sound.Clear();
        Pool.Clear();
    }
}
