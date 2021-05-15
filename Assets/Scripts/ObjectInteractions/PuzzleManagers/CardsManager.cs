using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum Suits { CLUBS, SPADES, HEARTS, DIAMONDS }
[System.Serializable]
public struct IndivCardHolders { public SpriteRenderer[] chPositions; }
[System.Serializable]
public struct Buttons { public MeshRenderer[] buttons; }

public class CardsManager : Singleton<CardsManager>
{
    [SerializeField] private List<Sprite> clubs, spades, hearts, diamonds;
    [SerializeField] private IndivCardHolders[] cardHolders = new IndivCardHolders[5];
    [SerializeField] private int[] combination = new int[5];
    [SerializeField] private Suits[] suitsCombination = new Suits[5];
    [SerializeField] private Transform[] holderTransforms = new Transform[5];
    [SerializeField] private Buttons[] numberChangerButtons = new Buttons[5];
    [SerializeField] private Buttons[] suitChangerButtons = new Buttons[5];
    [SerializeField] private Material buttonsDeactiveMaterial;
    [SerializeField] private GameObject cardsCanvas;

    private List<Material>[] ncbMats = new List<Material>[5], scbMats = new List<Material>[5];

    //NUMBER OF THE CARDS FRONTING
    private int[] numberIndex;
    //SUIT OF THE CARDS FRONTING
    private Suits[] suitsIndex;
    //WHICH CARDHOLDER IS FRONTING
    private int[] cardPositions;
    //WHICH HOLDER IS ROTATING
    private bool[] holdersRotating = new bool[] { false, false, false, false, false };

    private float timeToRotate = 0.8f, rotationSpeed = 90 * 0.8f;
    private float[] ts = new float[] { 0, 0, 0, 0, 0 };
    private float[] originalRot = new float[] { 0, 0, 0, 0, 0 };
    private int[] inverters = new int[] { 1, 1, 1, 1, 1 };
    private float[] actualRot = new float[] { 0, 0, 0, 0, 0 };
    private bool[] isRotating = new bool[] { false, false, false, false, false };


    public void ActivateClickers()
    {
        SetClickersActive(true);
    }
    public void SetClickersActive(bool active, bool forceCamerasInactive = false)
    {
        Cl_ClickCards[] cls = GetComponentsInChildren<Cl_ClickCards>();
        foreach (Cl_ClickCards cl in cls)
        {
            cl.enabled = active;
            cl.CanBeOutlined = active;
        }
        if (!forceCamerasInactive)
            cardsCanvas.SetActive(!active);
        CamerasManager.Instance.SetCardsCamActive(!active);
    }

    private void Start()
    {
        numberIndex = new int[] { 0, 0, 0, 0, 0 };
        suitsIndex = new Suits[] { Suits.CLUBS, Suits.SPADES, Suits.HEARTS, Suits.DIAMONDS, Suits.CLUBS };
        cardPositions = new int[] { 0, 0, 0, 0, 0 };

        for (int i = 0; i < numberChangerButtons.Length; i++)
        {
            ncbMats[i] = new List<Material>();
            for (int j = 0; j < numberChangerButtons[i].buttons.Length; j++)
            {
                ncbMats[i].Add(numberChangerButtons[i].buttons[j].material);
            }
        }
        for (int i = 0; i < suitChangerButtons.Length; i++)
        {
            scbMats[i] = new List<Material>();
            for (int j = 0; j < suitChangerButtons[i].buttons.Length; j++)
            {
                scbMats[i].Add(suitChangerButtons[i].buttons[j].material);
            }
        }

        for (int i = 0; i < cardHolders.Length; i++)
        {
            UpdateHolder(i, true);
        }

        SetClickersActive(false, true);
    }
    private void Update()
    {
        for (int i = 0; i < holdersRotating.Length; i++)
        {
            if (holdersRotating[i])
            {
                if (!isRotating[i])
                {
                    isRotating[i] = true;
                    StartCoroutine(Rotate(i, Vector3.right, 90 * inverters[i]));
                }
            }
        }
    }
    IEnumerator Rotate(int index, Vector3 axis, float angle, float duration = 0.8f)
    {
        Quaternion from = holderTransforms[index].localRotation;
        Quaternion to = holderTransforms[index].localRotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            holderTransforms[index].rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        holderTransforms[index].rotation = to;
        holdersRotating[index] = false;

        if (inverters[index] == -1)
            NextCard(index);
        else
            PreviousCard(index);

        SetButtonsActive(index, true);
        isRotating[index] = false;
    }

    private void UpdateSuit(int indexCH, bool next)
    {
        if (next)
        {
            if (suitsIndex[indexCH] == Suits.DIAMONDS)
                suitsIndex[indexCH] = Suits.CLUBS;
            else
                suitsIndex[indexCH]++;
        }
        else
        {
            if (suitsIndex[indexCH] == Suits.CLUBS)
                suitsIndex[indexCH] = Suits.DIAMONDS;
            else
                suitsIndex[indexCH]--;
        }

        UpdateHolder(indexCH, true);
    }

    private void UpdateHolder(int indexCH, bool updateFront)
    {
        int frontIndex = numberIndex[indexCH];
        int topIndex = frontIndex == 12 ? 0 : frontIndex + 1;
        int botIndex = frontIndex == 0 ? 12 : frontIndex - 1;
        int backIndex = topIndex == 12 ? 0 : topIndex + 1;

        Sprite newCardFront, newCardBot, newCardBack, newCardTop;
        switch (suitsIndex[indexCH])
        {
            case Suits.CLUBS:
                newCardTop = clubs[topIndex];
                newCardFront = clubs[frontIndex];
                newCardBot = clubs[botIndex];
                newCardBack = clubs[backIndex];
                break;
            case Suits.SPADES:
                newCardTop = spades[topIndex];
                newCardFront = spades[frontIndex];
                newCardBot = spades[botIndex];
                newCardBack = spades[backIndex];
                break;
            case Suits.HEARTS:
                newCardTop = hearts[topIndex];
                newCardFront = hearts[frontIndex];
                newCardBot = hearts[botIndex];
                newCardBack = hearts[backIndex];
                break;
            case Suits.DIAMONDS:
                newCardTop = diamonds[topIndex];
                newCardFront = diamonds[frontIndex];
                newCardBot = diamonds[botIndex];
                newCardBack = diamonds[backIndex];
                break;
            default:
                newCardTop = clubs[topIndex];
                newCardFront = clubs[frontIndex];
                newCardBot = clubs[botIndex];
                newCardBack = clubs[backIndex];
                break;
        }

        int frontCh = cardPositions[indexCH];
        int topCh = frontCh == 3 ? 0 : frontCh + 1;
        int backCh = topCh == 3 ? 0 : topCh + 1;
        int botCh = backCh == 3 ? 0 : backCh + 1;

        if (updateFront)
            cardHolders[indexCH].chPositions[frontCh].sprite = newCardFront;
        cardHolders[indexCH].chPositions[topCh].sprite = newCardTop;
        cardHolders[indexCH].chPositions[backCh].sprite = newCardBack;
        cardHolders[indexCH].chPositions[botCh].sprite = newCardBot;

        CombinationChecker();
    }

    private void NextCard(int indexCH)
    {
        if (numberIndex[indexCH] < 12)
            numberIndex[indexCH]++;
        else
            numberIndex[indexCH] = 0;

        if (cardPositions[indexCH] < 3)
            cardPositions[indexCH]++;
        else
            cardPositions[indexCH] = 0;

        originalRot[indexCH] = holderTransforms[indexCH].localEulerAngles.x;

        UpdateHolder(indexCH, false);
    }
    private void PreviousCard(int indexCH)
    {
        if (numberIndex[indexCH] > 0)
            numberIndex[indexCH]--;
        else
            numberIndex[indexCH] = 12;

        if (cardPositions[indexCH] > 0)
            cardPositions[indexCH]--;
        else
            cardPositions[indexCH] = 3;

        float rot =
        originalRot[indexCH] = holderTransforms[indexCH].localEulerAngles.x;

        UpdateHolder(indexCH, false);
    }

    public void CardButton(int indexCH, bool next)
    {
        inverters[indexCH] = next ? 1 : -1;
        holdersRotating[indexCH] = true;

        SetButtonsActive(indexCH, false);
    }
    public void CardButtonUp(int i)
    {
        CardButton(i, true);
    }
    public void CardButtonDown(int i)
    {
        CardButton(i, false);
    }
    public void SuitButtonUp(int i)
    {
        UpdateSuit(i, true);
    }
    public void SuitButtonDown(int i)
    {
        UpdateSuit(i, false);
    }

    private void SetButtonsActive(int indexCH, bool active)
    {
        for (int i = 0; i < numberChangerButtons[indexCH].buttons.Length; i++)
        {
            numberChangerButtons[indexCH].buttons[i].material = active ? ncbMats[indexCH][i] : buttonsDeactiveMaterial;
            numberChangerButtons[indexCH].buttons[i].GetComponent<Button>().enabled = active;
        }
        for (int i = 0; i < suitChangerButtons[indexCH].buttons.Length; i++)
        {
            suitChangerButtons[indexCH].buttons[i].material = active ? scbMats[indexCH][i] : buttonsDeactiveMaterial;
            numberChangerButtons[indexCH].buttons[i].GetComponent<Button>().enabled = active;
        }
    }

    private void CombinationChecker()
    {
        for (int i = 0; i < combination.Length; i++)
        {
            if (combination[i] != numberIndex[i] || suitsCombination[i] != suitsIndex[i])
            {
                return;
            }
        }

        PuzzleFinished();
    }

    private void PuzzleFinished()
    {
        for (int i = 0; i < cardHolders.Length; i++)
        {
            SetButtonsActive(i, false);
        }

        CamerasManager.Instance.SetCardsCamActive(false);
        CamerasManager.Instance.CanCardsCamActivate = false;

        //END GAME
        GameManager.Instance.EndGame();
    }
}
