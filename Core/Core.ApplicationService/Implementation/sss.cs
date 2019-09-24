using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationService.Implementation
{

    public interface Isss
    {
        List<int> DoSth();

    }
    public class sss: Isss
    {

        public List<int> DoSth()
        {
            return new List<int>() { 4, 5, 9 };
        }

    }
}
