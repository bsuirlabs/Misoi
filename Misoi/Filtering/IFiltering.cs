﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Filtering
{
    public interface IFiltering
    {
        Bitmap FiterPicture(Bitmap inputPicture);
    }
}
