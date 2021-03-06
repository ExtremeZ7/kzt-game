﻿using UnityEngine;
using Controllers;

public class ShowGemCollector : MonoBehaviour
{

    [HideInInspector]
    public bool showingGem;

    [Space(10)]
    public float hideSpeed;
    public float showSpeed;
    public float guiHideDelay;
    [HideInInspector]
    public float hideTimer;

    private Transform gemSprite;
    private Transform hidePoint;
    private Transform showPoint;

    private bool currentGemStatus;

    void Start()
    {
        gemSprite = transform.GetChild(0);
        hidePoint = transform.GetChild(1);
        showPoint = transform.GetChild(2);

        gemSprite.position = new Vector3(gemSprite.position.x,	hidePoint.position.y, 0.0f);
        currentGemStatus = GameController.Instance.items.hasGem;
    }

    void Update()
    {

        if (((currentGemStatus != GameController.Instance.items.hasGem) && GameController.Instance.items.hasGem) || Input.GetKeyDown("space"))
            hideTimer = guiHideDelay;

        gemSprite.gameObject.GetComponent<Animator>().SetBool("Collected", GameController.Instance.items.hasGem);

        showingGem = !Help.UseAsTimer(ref hideTimer);

        gemSprite.position = Vector3.MoveTowards(gemSprite.transform.position,
            (showingGem ? showPoint.position : hidePoint.position),
            (showingGem ? showSpeed : hideSpeed) * Time.deltaTime);

        currentGemStatus = GameController.Instance.items.hasGem;
    }
}
