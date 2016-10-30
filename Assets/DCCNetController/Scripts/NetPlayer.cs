using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public enum TVCommand//TV端用
{
    StartGame,
    StopGame,
    ResetGame,
    BroadcastYes,
    BroadcastNo
}

public enum ContentType
{
    None,
    Movie,
    GameWithPad,
    GameWithSteer,
    Picture
}

public class NetPlayer : NetworkBehaviour
{
    [SyncVar]
    //[HideInInspector]
    public float sync_H;//服务器更新客户端读取
    [SyncVar]
    //[HideInInspector]
    public float sync_V;//服务器更新客户端读取
    

    //下面的不成功，因为外面获取hostIP值时LocalPlayer并不一定有值，有可能跟服务端调用哪个NetPlayer组件发送消息有关
    //public string hostIP;//用于发送姿态的主客户端（IP后三位）
    //[SyncVar]
    ////[HideInInspector]
    //public string sync_HostIP;//服务器更新客户端读取（IP后三位）

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 要求服务器执行某些命令，这个函数不暴露给客户端插件
    /// </summary>
    /// <param name="cmd">命令名</param>
    /// <param name="type">内容类型</param>
    /// <param name="arg">跟的参数，如播放影片的ID、运行游戏的ID</param>
    [Command]
    public void CmdTVServerExec(TVCommand cmd, ContentType type, string arg)
    {
        //TV端不需要实现
    }

    [ClientRpc]
    void RpcGameAlreadyStart()
    {
        //TODO:
    }

    [ClientRpc]
    void RpcGameAlreadyStop()
    {
        //TODO:
    }

    [ClientRpc]
    public void RpcStartGame(string sceneName)
    {
        //客户端实现
    }

    [ClientRpc]
    public void RpcStopGame()
    {
        //客户端实现
    }

    [ClientRpc]
    public void RpcUpdateHostIP(string hostIP)
    {
        //客户端实现
    }
}
