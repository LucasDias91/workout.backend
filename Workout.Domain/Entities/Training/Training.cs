

using System.Globalization;

namespace Workout.Domain.Entities.Training
{
    public class Training : Entity
    {
        public int Code { get; private set; }
        public DateTime Date { get;  private set; }
        public string Group { get; private set; }
        public string Goal { get; private set; }
        public string Exercise { get; private set; }
        public string SxR { get; private set; }
        public string? Technique { get; private set; }
        public char Type { get; private set; }
        protected Training() { }
        public Training(string code,
                        string date,
                        string goal,
                        string exercise,
                        string sxr,
                        string technique,
                        string group,
                        char type)
        {
            Code = Convert.ToInt32(code);
            Date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Goal = goal;
            Exercise = exercise;
            SxR = sxr;
            Technique = technique;
            Activate();
            Group = group;
            Type = type;

        }
    }
}
