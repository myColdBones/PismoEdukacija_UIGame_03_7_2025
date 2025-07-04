using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool IsGameRunning = false;
    //resources
    [SerializeField] private int workers;
    [SerializeField] private int nonworkers;

    [SerializeField] private int food;
    [SerializeField] private int wood;
    [SerializeField] private int stone;
    [SerializeField] private int iron;
    [SerializeField] private int gold;

    //time
    private int days;
    private float timer;
    [SerializeField]
    private float IRLSeconds1DayTakes;

    //TMP
    [SerializeField] private TMP_Text TimeTMP;
    [SerializeField] private TMP_Text DaysTMP;

    [Header("Buildings")]
    [SerializeField] private int house; //1 house takes 4
    [SerializeField] private int farm;
    [SerializeField] private int woodcutter;
    [SerializeField] private int blacksmith;
    [SerializeField] private int quarry;
    [SerializeField] private int ironmines;
    [SerializeField] private int goldmines;

    //stuff

    void Start()
    {
        //house total housing and also increase when upgrading
        //house * 4
        //house * 6
        //house * 4 * multiplier = house * 6
        UpdateResourceAndBuildTMPS();
    }
    public void Initialize()
    {
        IsGameRunning = true;
        UpdateResourceAndBuildTMPS();
        SliderUpdatesIRLSeconds();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if(IsGameRunning)
        timer += Time.deltaTime;
        if (timer >= IRLSeconds1DayTakes)
        {
            days++;
            timer = 0;
            EndOfDayCalculations();
        }

        int timerToDayInMinutes = (int)((timer / IRLSeconds1DayTakes) * 60 * 24);
        float timerInHours = timerToDayInMinutes / 60;
        float timerInLeftMinutes = timerToDayInMinutes % 60;
        string timeInHoursAndMinutes = timerInHours.ToString("0") + "h" + ":" + timerInLeftMinutes.ToString("0") + "min";
        TimeTMP.text = timeInHoursAndMinutes;
        DaysTMP.text = $"{days}days";
    }

    [SerializeField] private Slider slider;
    public void SliderUpdatesIRLSeconds()
    {
        IRLSeconds1DayTakes = slider.value;
    }
    private void EndOfDayCalculations()
    {
        FoodGather();
        FoodPrudction();
        FoodConsume();
        WoodGather();
        WoodProduction();
        StoneProduction();
        IronProduction();
        IncreasePopulation();
        UpdateResourceAndBuildTMPS();
    }
    public void DecreasePopulation(int numberOfDead)
    {
        if (numberOfDead % 2 == 0)
        {
            nonworkers--;
            workers--;
        }

    }
    public void IncreasePopulation()
    {
        if (days % 2 == 0)
        {
            if (GetMaxPopulation() > Population())
            {
                nonworkers += house;

                if(Population() > GetMaxPopulation())
                {
                    nonworkers = GetMaxPopulation() - workers;
                }
            }
        }
    }
    public int Population()
    {
        return workers + nonworkers;
    }
    public void FoodGather()
    {
        food += nonworkers / 2;
    }
    public void FoodPrudction()
    {
        food += farm * 4;
    }
    public void FoodConsume()
    {
        food -= Population();
    }
    public void WoodGather()
    {
        wood += nonworkers / 2;
    }
    public void WoodProduction()
    {
        wood += woodcutter * 2;
    }
    public void StoneProduction()
    {
        stone += quarry * 4;
    }
    public void IronProduction()
    {
        stone += ironmines * 5;
    }
    public int GetMaxPopulation()
    {
        //numbber of houses * 4
        int maxPopulation = house * 4;
        return maxPopulation;
    }
    //build 
    public void BuildHouse()
    {
        ProductionCost cost = new ProductionCost(0, 2, 0, 0, 0);
        if (CheckCost(cost))
        {
            BuildCost(cost);
            house ++;
        }
        UpdateResourceAndBuildTMPS();

    }
    public void BuildFarm()
    {
        ProductionCost cost = new ProductionCost(0, 10, 0, 0, 2);
        if (
            CheckCost(cost))
        {
            BuildCost(cost);
            farm++;
        }
        UpdateResourceAndBuildTMPS();
    }
    public void BuildWoodcutter()
    {
        ProductionCost cost = new ProductionCost(0,5,0,1,1);

        if (CheckCost(cost))
        {
            BuildCost(cost);
            woodcutter++;
        }
        UpdateResourceAndBuildTMPS();
    }
    public void BuildStoneQuarry()
    {
        ProductionCost cost = new ProductionCost(0,5,0,0,1);

        if (CheckCost(cost))
        {
            BuildCost(cost);
            quarry++;
        }
        UpdateResourceAndBuildTMPS();
    }
    public void BuildIronMine()
    {
        ProductionCost cost = new ProductionCost(0, 10, 20, 0, 1);

        if (CheckCost(cost))
        {
            BuildCost(cost);
            ironmines++;
        }
        UpdateResourceAndBuildTMPS();
    }
    public bool CheckCost(ProductionCost cost)
    {
        return food >= cost.foodCost && wood >= cost.woodCost && stone >= cost.stoneCost && iron > cost.ironCost && nonworkers >= cost.workerCost;
    }
    public void BuildCost(ProductionCost cost)
    {
            food -= cost.foodCost;
            wood -= cost.woodCost;
            stone -= cost.stoneCost;
            iron -= cost.ironCost;
            nonworkers -= cost.workerCost;
            workers += cost.workerCost;
    }
    [SerializeField] private TMP_Text PopulationTMP;
    [SerializeField] private TMP_Text FoodTMP;
    [SerializeField] private TMP_Text WoodTMP;
    [SerializeField] private TMP_Text StoneTMP;
    [SerializeField] private TMP_Text IronTMP;
    [SerializeField] private TMP_Text HouseTMP;
    [SerializeField] private TMP_Text FarmTMP;
    [SerializeField] private TMP_Text WoodcuttersTMP;
    [SerializeField] private TMP_Text QuarryTMP;
    [SerializeField] private TMP_Text IronmineTMP;

    public void UpdateResourceAndBuildTMPS()
    {
        PopulationTMP.text = $"Population:{Population()}/{GetMaxPopulation()}" +
                             $"\n  Workers:{workers}" +
                             $"\n  Nonworkers:{nonworkers}";
        FoodTMP.text = $"Food:{food}";
        WoodTMP.text = $"Wood:{wood}";
        StoneTMP.text = $"Stone:{stone}";
        IronTMP.text = $"Iron:{iron}";
        HouseTMP.text = $"Houses:{house}" + 
                         "\n     Cost:-2 wood";
        FarmTMP.text = $"Farms:{farm}" + 
                        "\n     Cost:-10 wood"+
                        "\n          -1 nonworker";
        WoodcuttersTMP.text = $"Woodcutters:{woodcutter}" + 
                               "\n     Cost:-5 wood"+
                               "\n          -1 iron"+
                               "\n          -1 nonworker";

        QuarryTMP.text = $"Quarrys:{quarry}" + 
                          "\n     Cost:-5 wood"+
                          "\n          -1nonworker";

        IronmineTMP.text = $"Ironmine:{iron}" + 
                            "\n     Cost:-10 wood"+
                            "\n     Cost:-20 stone"+
                            "\n     Cost:-1 nonworker";




    }
}

public class ProductionCost
{
    public int foodCost;
    public int woodCost;
    public int stoneCost;
    public int ironCost;
    public int workerCost;

    public ProductionCost()
    {

    }

    public ProductionCost(int foodCost, int woodCost, int stoneCost, int ironCost, int workerCost)
    {
        this.foodCost = foodCost;
        this.woodCost = woodCost;
        this.stoneCost = stoneCost;
        this.ironCost = ironCost;
        this.workerCost = workerCost;
    }

}
