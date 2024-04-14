using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Trials : NetworkBehaviour
{

    [SyncVar]
    public int trials = 3;
    [SyncVar]
    public bool man_convicted = false;
    [SyncVar]
    public bool judge_convicted = false;
    void Start()
    {

    }

    [Command(requiresAuthority = false)]
    public void reduceTrialsScore()
    {
        if (trials > 0) trials = trials - 1;
    }

    [Command(requiresAuthority = false)]
    public void convictMan()
    {
        man_convicted = true;
    }

    [Command(requiresAuthority = false)]
    public void convictJudge()
    {
        judge_convicted = true;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
