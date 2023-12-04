using ETLPipeline.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPipeline
{
    public class Transform
    {
        public IEnumerable<CarReviews> Reviews { get; set; }

        public IEnumerable<Cars> Cars { get; set; }
        
        public IEnumerable<Users> Users { get; set; }

        public List<TransformedData> MergingData()
        {
            var result = (
                from user in Users
                from car in Cars
                from review in Reviews
                where review.UserId == user.Id && review.CarId== car.CarId
                select new TransformedData 
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    CarId = car.CarId,
                    CarName = car.Name,
                    Review = review.Review
                }).ToList();

            return result;
        }
    }
}
