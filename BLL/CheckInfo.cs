using System;

namespace BLL
{
    public class CheckInfo
    {
        public bool DateTryParse(string date)
        {
            if (DateTime.TryParse(date, out DateTime result))
                return true;
            else
                return false;
        }
        public bool IntTryParse(string intc)
        {
            if (int.TryParse(intc, out int result) && int.Parse(intc) > 0)
                return true;
            else
                return false;
        }
        public bool DoubleTryParse(string intc)
        {
            if (double.TryParse(intc, out double result) && double.Parse(intc) > 0)
                return true;
            else
                return false;
        }
        public bool CheckShow(TheatreBoxOffice theatreBoxOffice, string countshow)
        {
            if (int.TryParse(countshow, out int result))
            {
                if (int.Parse(countshow) > theatreBoxOffice.shows.Count || int.Parse(countshow) <= 0)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
    }
}