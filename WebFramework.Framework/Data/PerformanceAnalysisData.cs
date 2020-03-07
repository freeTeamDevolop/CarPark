using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Framework.Data
{
    public class PerformanceAnalysisData
    {
        private LinkedList<PerformanceStep> _steps;

        public PerformanceAnalysisData()
        {
            _steps = new LinkedList<PerformanceStep>();
        }

        public List<PerformanceStep> Steps
        {
            get { return _steps.ToList(); }
        }

        public TimeSpan total_cast { get; private set; }

        public int total_castToMilliseconds { get; set; }

        public void Add(string data)
        {
            var lastNode = _steps.Last;
            var cast = TimeSpan.FromMilliseconds(0);
            var now = DateTime.Now;
            if (lastNode != null)
            {
                var lastStep = lastNode.Value;
                if (lastStep != null)
                    cast = (now - lastStep.step_time);
            }
            var step = new PerformanceStep() { cast_from_last_step = cast, step_time = now, data = data };
            total_cast = total_cast.Add(cast);
            _steps.AddLast(step);
        }
    }

    public class PerformanceStep
    {
        public DateTime step_time { get; set; }
        public TimeSpan cast_from_last_step { get; set; }
        public string data { get; set; }
    }
}
