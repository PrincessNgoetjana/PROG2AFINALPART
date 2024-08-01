
// MainWindow.xaml.cs
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private List<Recipe> allRecipes; // Assuming you have a Recipe class or data structure

        public MainWindow()
        {
            InitializeComponent();
            InitializeRecipes(); // Load the recipes from a data source
        }

        private void InitializeRecipes()
        {
            // Load the recipes into the 'allRecipes' list
            allRecipes = GetRecipesFromDataSource();
            UpdateRecipeList();
        }

        private void UpdateRecipeList()
        {
            // Apply the selected filters and update the recipe list
            IEnumerable<Recipe> filteredRecipes = allRecipes;

            // Filter by ingredient
            if (!string.IsNullOrEmpty(ingredientTextBox.Text))
            {
                string ingredient = ingredientTextBox.Text.ToLower();
                filteredRecipes = filteredRecipes.Where(recipe =>
                    recipe.Ingredients.Any(ingredientName => ingredientName.ToLower().Contains(ingredient)));
            }

            // Filter by food group
            if (foodGroupComboBox.SelectedIndex > 0)
            {
                string selectedGroup = foodGroupComboBox.SelectedItem.ToString();
                filteredRecipes = filteredRecipes.Where(recipe => recipe.FoodGroup == selectedGroup);
            }

            // Filter by maximum calories
            if (!string.IsNullOrEmpty(caloriesTextBox.Text) && int.TryParse(caloriesTextBox.Text, out int maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(recipe => recipe.Calories <= maxCalories);
            }

            // Update the recipe list display
            recipeListView.ItemsSource = filteredRecipes;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateRecipeList();
        }
    }
}