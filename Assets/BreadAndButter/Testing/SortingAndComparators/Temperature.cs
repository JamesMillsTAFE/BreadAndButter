using System;

public class Temperature : IComparable
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>
    /// Less than zero      - The current instance precedes the object specified by the CompareTo method in the sort order.
    /// Zero                - This current instance occurs in the same position in the sort order as the object specified by the CompareTo method.
    /// Greater than zero   - This current instance follows the object specified by the CompareTo method in the sort order.
    /// </returns>
    public int CompareTo(object obj)
    {
        if (obj == null)
            // This should be after the passed object - 'hotter' than the passed object 
            return 1;

        // Cast obj to temperature
        Temperature otherTemp = obj as Temperature;

        if (otherTemp != null)
        {
            // Now we can compare them
            if(this.temperature > otherTemp.temperature)
            {
                // It should come after in the list as it is 'hotter' than the passed object
                return 1;
            }
            else if(this.temperature < otherTemp.temperature)
            {
                // It should come before it in the list as it is 'colder' than the passed object
                return -1;
            }

            // The temps are the same so return 0
            return 0;
        }
        else
        {
            // We couldn't cast the object to a temperature so throw an exception
            throw new ArgumentException("Object is not a temperature");
        }
    }

    protected float temperature;

    public Temperature(float _temp)
    {
        temperature = _temp;
    }

    public override string ToString()
    {
        return temperature.ToString("0.0");
    }
}
