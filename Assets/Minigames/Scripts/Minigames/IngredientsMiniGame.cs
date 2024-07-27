using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using Ingredients;
using UnityEngine.UI;

public class IngredientsMiniGame : Minigame
{
    [SerializeField]
    List<PotionEntry> potions;
    [SerializeField]
    float degreesPerSecond = 180f,graceAngle = 10f;
    [SerializeField]
    Text infoBox;
    Transform wheel;

    float radius;

    List<GameObject> ingredients = new List<GameObject>();

    float currentAngle = 0;

    List<Slot> slots = new List<Slot>();

    protected override void OnAwake()
    {
        wheel = transform.Find("Wheel");
        Transform slotHolder = wheel.Find("SlotHolder");
        radius = slotHolder.localPosition.magnitude;
        Destroy(slotHolder.gameObject);

        SpawnIngredients();
        StartCoroutine(SpinWheel());
    }
    void SpawnIngredients()
    {
        foreach (PotionEntry potion in potions)
        {
            foreach (IngredientEntry ingredient in potion.GetIngredients())
            {
                GameObject newIngredient = Instantiate(ingredient.GetPrefab());
                newIngredient.name = ingredient.name;
                newIngredient.transform.parent = wheel;
                newIngredient.transform.localPosition = Vector3.zero;
                ingredients.Add(newIngredient);
            }
        }
    }
    void SetupIngredientWheel()
    {
        ShuffleList(ref ingredients);
        PlaceIngredients();
    }
    void PlaceIngredients()
    {
        slots.Clear();
        float angleBetweenIngredients = 360f / ingredients.Count;
        float workingAngle = 0;
        foreach(GameObject ingredient in ingredients)
        {
            Slot slot = new Slot(workingAngle, graceAngle, ingredient);
            slots.Add(slot);

            Vector3 ingredientPosition =  Quaternion.AngleAxis(workingAngle, Vector3.forward) * Vector3.up;
            ingredientPosition = ingredientPosition.normalized * radius;
            ingredient.transform.localPosition = ingredientPosition;
            ingredient.transform.up = wheel.TransformDirection(ingredientPosition);
            ingredient.SetActive(true);
            workingAngle += angleBetweenIngredients;
        }
    }
    void ShuffleList<T>(ref List<T> list)
    {
        List<T> tempList = new List<T>();

        while(list.Count > 0)
        {
            int randomIndex = Random.Range(0, list.Count);
            tempList.Add(list[randomIndex]);
            list.RemoveAt(randomIndex);
        }

        list = tempList;
    }
    void ReadIngredients(List<GameObject> ingr)
    {
        print("----------------");
        foreach(GameObject go in ingr)
        {
            print(go.name);
        }
        print("----------------");
    }
    protected override void OnCloseMiniGame(bool success)
    {

    }

    protected override void OnMinigameEnd(bool success)
    {
        infoBox.text = "Win: " + success;
    }

    protected override void OnMinigameLoad()
    {
        
    }

    protected override void OnPrepareMiniGame()
    {
        SetupIngredientWheel();
        ShuffleList(ref potions);
        infoBox.text = "";
    }

    protected override IEnumerator RunGame()
    {
        float timeElapsed = 0;
        int potionIndex = 0;
        PotionEntry currentPotion = potions[potionIndex];
        List<string> requiredIngredients = new List<string>();
        SetupNextPotion(currentPotion);

        while(timeElapsed < miniGameTime)
        {
            timeElapsed += Time.deltaTime;
            timer.SetTimer(miniGameTime, miniGameTime - timeElapsed);

            yield return new WaitForEndOfFrame();
            if (InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
            {
                float angle = currentAngle + 180;
                if (angle > 360)
                    angle -= 360f;
                if(GetCurrentSlot(angle, out Slot result))
                {
                    GameObject foundIngredient = result.GetIngredient();

                    foundIngredient.SetActive(false);
                    if (requiredIngredients.Contains(foundIngredient.name))
                    {
                        requiredIngredients.Remove(foundIngredient.name);
                        if(requiredIngredients.Count == 0)
                        {
                            potionIndex++;
                            if(potionIndex == potions.Count)
                            {
                                MinigameEnd(true);
                                yield break;
                            }
                            else
                            {
                                SetupNextPotion(potions[potionIndex]);
                            }
                        }
                    }
                    else
                    {
                        MinigameEnd(false);
                        yield break;
                    }
                }
                else
                {
                    //Do on no available slot
                }
            }
        }
        MinigameEnd(false);
        yield break;

        void SetupNextPotion(PotionEntry newPotion)
        {
            currentPotion = newPotion;
            requiredIngredients.Clear();
            string text = currentPotion.name + "\n";
            foreach (IngredientEntry ingredient in currentPotion.GetIngredients())
            {
                requiredIngredients.Add(ingredient.name);
                text += ingredient.name + "\n";
            }

            infoBox.text = text;
        }
    }

    IEnumerator SpinWheel()
    {
        Quaternion orgRot = wheel.localRotation;

        while (true)
        {
            wheel.localRotation = orgRot * Quaternion.AngleAxis(currentAngle, -Vector3.forward);
            yield return new WaitForEndOfFrame();
            currentAngle += degreesPerSecond * Time.deltaTime;
            if(currentAngle > 360)
            {
                currentAngle -= 360f;
            }
        }
    }

    protected override IEnumerator WaitForGameEnd()
    {
        yield return new WaitForEndOfFrame();
        while (!InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
        {
            yield return new WaitForEndOfFrame();
        }
    }
    bool GetCurrentSlot(float angle,out Slot result)
    {
        foreach(Slot slot in slots)
        {
            if (slot.CheckInRange(angle))
            {
                result = slot;
                return true;
            }
        }
        result = new Slot();
        return false;
    }
    struct Slot
    {
        GameObject _ingredient;
        float minAngle, maxAngle;
        bool active;
        public Slot(float angle,float graceAngle,GameObject ingredient)
        {
            minAngle = angle - graceAngle;
            maxAngle = angle + graceAngle;
            _ingredient = ingredient;
            active = true;
        }
        public void SetSlot(GameObject ingredient)
        {
            _ingredient = ingredient;
        }
        public GameObject GetIngredient()
        {
            active = false;
            return _ingredient;
        }
        public bool CheckInRange(float value)
        {
            if (!active)
                return false;

            if(minAngle < 0)
            {
                return (value > (minAngle + 360f) || value < maxAngle);
            }
            else if(maxAngle > 360)
            {
                return (value < (maxAngle - 360f) || value > minAngle);
            }
            return value < maxAngle && value > minAngle;
        }
    }
}
