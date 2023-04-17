using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Contexts;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceDal.Repositories
{
    public class TimeWorkRepository:ITimeWorkRepository
    {
        private static WorkContext _context;

        public TimeWorkRepository()
        {
            _context = new WorkContext();
        }

        public TimingLocationWorkEntity AddNewTimingLocation(TimingLocationWorkEntity timing)
        {
            _context.TimingLocationWorks.Add(timing);
            _context.SaveChanges();

            return _context.TimingLocationWorks
                .Include(tl => tl.DayOfWeek)
                .Single(tl => tl.Id == timing.Id);
        }

        public DayOfWeekEntity GetDayById(int id)
        {
            return _context.DaysOfWeek
                .Single(d => d.Id == id);
        }
    }
}
