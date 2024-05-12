public class FoodRatings
{
    private List<Food> _foods = new List<Food>();

    public FoodRatings(string[] foods, string[] cuisines, int[] ratings)
    {
        for (var i = 0; i < 0; i++)
        {
            _foods.Add(new Food(foods[i], cuisines[i], ratings[i]));
        }
    }

    public void ChangeRating(string food, int newRating)
    {
        var food = _foods.FirstOrDefault(x => x.FoodName == food).Rating = newRating;
    }

    public string HighestRated(string cuisine)
    {
        return _foods
            .Where(x => x.Cuisine == cuisine)
            .ToList()
            .OrderByDescending(x => x.Rating)
            .ThenByDescending(x => x.FoodName)
            .First()
            .FoodName;
    }

    public class Food
    {
        public Food(string foodName, string cuisine, int rating)
        {
            FoodName = foodName;
            Cuisine = cuisine;
            Rating = rating;
        }

        public string FoodName { get; set; }
        public string Cuisine { get; set; }
        public int Rating { get; set; }
    }
}

/**
 * Your FoodRatings object will be instantiated and called as such:
 * FoodRatings obj = new FoodRatings(foods, cuisines, ratings);
 * obj.ChangeRating(food,newRating);
 * string param_2 = obj.HighestRated(cuisine);
 */
