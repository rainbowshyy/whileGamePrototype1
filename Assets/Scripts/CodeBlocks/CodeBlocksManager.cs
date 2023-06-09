using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CodeBlocksManager : MonoBehaviour
{
    public static CodeBlocksManager Instance;

    [Serializable]
    public struct CodeBlockPrefab
    {
        public CodeBlocks code;
        public GameObject prefab;
    }
    [SerializeField] private CodeBlockPrefab[] codeBlockPrefabsArray;
    public static Dictionary<CodeBlocks, GameObject> codeBlockPrefabs;

    [Serializable]
    public struct InputCodeUI
    {
        public Inputs input;
        public CodeBlockUIParent parent;
    }
    [SerializeField] private InputCodeUI[] inputCodeUIArray;
    private Dictionary<Inputs, CodeBlockUIParent> inputCodeUI;

    private Dictionary<Inputs, List<CodeBlock>> currentBlocks;

    [SerializeField] private List<Projectile> projectiles;
    [SerializeField] private int[] projectilesCodeStep;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        codeBlockPrefabs = new();
        foreach (CodeBlockPrefab e in codeBlockPrefabsArray)
        {
            codeBlockPrefabs.Add(e.code, e.prefab);
        }

        inputCodeUI = new();
        foreach (InputCodeUI e in inputCodeUIArray)
        {
            inputCodeUI.Add(e.input, e.parent);
        }
        DefaultCode();
        projectilesCodeStep = new int[1] { 0 };
    }

    private void Start()
    {
        InputManager.onUp += () => PerformInput(Inputs.Up);
        InputManager.onLeft += () => PerformInput(Inputs.Left);
        InputManager.onDown += () => PerformInput(Inputs.Down);
        InputManager.onRight += () => PerformInput(Inputs.Right);

        GameManager.onTick += () => PerformProjectile();

        VisualizeCode(Inputs.Up);
        VisualizeCode(Inputs.Left);
        VisualizeCode(Inputs.Down);
        VisualizeCode(Inputs.Right);

        CodeBlockUIParent.onChange += GetCode;
    }

    private void DefaultCode()
    {
        currentBlocks = new();

        currentBlocks.Add(Inputs.Up, new List<CodeBlock>() { new MoveBlock(new int[] { 0, 1 }, new bool[] { false, false }) });
        currentBlocks.Add(Inputs.Left, new List<CodeBlock>() { new MoveBlock(new int[] { -1, 0 }, new bool[] { false, false }) });
        currentBlocks.Add(Inputs.Down, new List<CodeBlock>() { new MoveBlock(new int[] { 0, -1 }, new bool[] { false, false }) });
        currentBlocks.Add(Inputs.Right, new List<CodeBlock>() { new MoveBlock(new int[] { 1, 0 }, new bool[] { false, false }) });
        currentBlocks.Add(Inputs.Blue, new List<CodeBlock>());
    }

    private void GetCode(Inputs inputs)
    {
        currentBlocks.Remove(inputs);
        currentBlocks.Add(inputs, inputCodeUI[inputs].GetCode());
        foreach(CodeBlock code in currentBlocks[inputs])
        {
            code.ReadyCode(Player.Instance);
        }
        UpdateProjectile();
    }

    private void VisualizeCode(Inputs input)
    {
        inputCodeUI[input].Clear();
        int i = 0;
        foreach (CodeBlock code in currentBlocks[input])
        {
            GameObject block = Instantiate(codeBlockPrefabs[code.type]);
            block.GetComponent<CodeBlockUIElement>().code = code;
            inputCodeUI[input].PlaceBlock(i, block);
            i += 1;
        }
    }

    private void PerformInput(Inputs input)
    {
        List<CodeBlock> inputBlocks = currentBlocks[input];

        for (int i = 0; i < inputBlocks.Count; i++)
        {
            inputBlocks[i].RunCode(Player.Instance);
        }
    }

    private void PerformProjectile()
    {
        foreach (Projectile agent in projectiles)
        {
            switch (agent.type)
            {
                case Projectiles.Blue:
                    if (currentBlocks[Inputs.Blue].Count > 0)
                        currentBlocks[Inputs.Blue][projectilesCodeStep[0]].RunCode(agent);
                    break;
            }
        }

        for (int i = 0; i < projectilesCodeStep.Length; i++)
        {
            projectilesCodeStep[i]++;
        }

        UpdateProjectile();
    }

    private void UpdateProjectile()
    {
        if (currentBlocks[Inputs.Blue].Count <= projectilesCodeStep[0])
        {
            projectilesCodeStep[0] = 0;
        }
        inputCodeUI[Inputs.Blue].ActivateBlock(projectilesCodeStep[0]);

        foreach (Projectile agent in projectiles)
        {
            switch (agent.type)
            {
                case Projectiles.Blue:
                    if (currentBlocks[Inputs.Blue].Count > 0)
                        currentBlocks[Inputs.Blue][projectilesCodeStep[0]].ReadyCode(agent);
                    break;
            }
        }

        inputCodeUI[Inputs.Blue].ActivateBlock(projectilesCodeStep[0]);
    }
}
