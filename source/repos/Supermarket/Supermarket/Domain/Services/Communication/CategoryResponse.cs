using Supermarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Domain.Services.Communication
{
    public class CategoryResponse: BaseResponse
    {
        public Category Category { get; private set; }

        private CategoryResponse(bool success, string message, Category category) : base(success, message)
        {
            Category = category;
        }


        /// <summary> 
        /// Creates a success repsonse
        /// </summary>
        /// <param name="category">Saved Category.</param>
        /// <returns>Response.</returns>
        public CategoryResponse(Category category) : this(true, string.Empty, category)
        {
        }

        /// <summary> 
        /// Creates an error repsonse
        /// </summary>
        /// <param name="message">Error Message.</param>
        /// <returns>Response.</returns>
        public CategoryResponse(string message) : this(false, message, null)
        {
        }



    }
}
