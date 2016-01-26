using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Define
{
    public const bool Development =
#if Development
    true
#else
    false
#endif
        ;


    public const bool Debug =
#if Debug
    true
#else
    false
#endif
        ;
}

