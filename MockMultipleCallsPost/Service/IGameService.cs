using System;
using System.Collections.Generic;
using System.Text;

namespace MockMultipleCallsPost.Service
{
    interface IGameService
    {
        string GetGameNames(int[] ids);
    }
}
