using Microsoft.AspNetCore.Mvc;
using Food.Services;
using Food.Models;

namespace Food.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {

        private readonly FoodService _foodService;

        public FoodController(FoodService foodService)
        {
            this._foodService = foodService;
        }

        // GET
        [HttpGet]
        public async Task<List<FoodModel>> Get()
        {
            return await this._foodService.Get();
        }

        // GET BY ID
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<FoodModel>> GetById(string id)
        {
            var foodModel = await this._foodService.GetById(id);
            if (foodModel is null) return NotFound();

            return foodModel;
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post(FoodModel newFoodModel)
        {
            await _foodService.Create(newFoodModel);
            return CreatedAtAction(nameof(Get), new { id = newFoodModel.Id }, newFoodModel);
        }

        // PATCH
        [HttpPatch("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, FoodModel updateFoodModel)
        {
            var foodModel = await this._foodService.GetById(id);
            if (foodModel is null) return NotFound();
            
            updateFoodModel.Id = foodModel.Id;

            await this._foodService.Patch(id, updateFoodModel);
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var foodModel = await this._foodService.GetById(id);
            if (foodModel is null) return NotFound();
           
            await this._foodService.DeleteById(id);

            return NoContent();
        }
    }
}

