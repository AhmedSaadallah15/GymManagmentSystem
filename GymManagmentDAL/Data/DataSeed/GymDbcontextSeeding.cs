using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed;

public static class GymDbcontextSeeding
{

    public static bool SeedData(GymDbContext dbContext)
    {
        try
        {
            var hasPlan = dbContext.Plans.Any();
            var hasCategory = dbContext.Categorys.Any();

            if (hasPlan && hasCategory) return false;

            if (!hasPlan)
            {
                var plans = GetDataFromJson<Plan>("plans.json");
                if (plans.Any())
                {
                    dbContext.Plans.AddRange(plans);
                }
            }

            if (!hasCategory)
            {
                var category = GetDataFromJson<Category>("categories.json");
                if (category.Any())
                {
                    dbContext.Categorys.AddRange(category);
                }
            }

            return dbContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Seeding Faild {ex}");
            return false; 
        }

    }

    private static List<T> GetDataFromJson<T>(string FileName)
    {
        //GymManagementPL\wwwroot\Files\plans.json
        var Filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files" , FileName);

        if (!File.Exists(Filepath))  throw new FileNotFoundException();

        string readData = File.ReadAllText(Filepath);

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<List<T>>(readData, options) ?? new List<T>();
    } 




}
