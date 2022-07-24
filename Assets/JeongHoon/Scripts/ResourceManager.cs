using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager
{
    public List<Dictionary<string, object>> currencyData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> foodData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> recipeData = new List<Dictionary<string,object>>();
    public List<Dictionary<string, object>> reserveCurrencyData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> reserveFoodData = new List<Dictionary<string, object>>();

    public ResourceManager()
    {
        Load();
    }

    public void Load()
    {
        LoadCurrency();
        LoadFood();
        LoadRecipe();
    }

    public void LoadCurrency()
    {
        currencyData = CSVReader.Read("Tables\\Currency_DataTable");
    }

    public void LoadFood()
    {
        foodData = CSVReader.Read("Tables\\Food_DataTable");
    }

    public void LoadRecipe()
    {
        recipeData = CSVReader.Read("Tables\\Recipe_DataTable");
    }
}
