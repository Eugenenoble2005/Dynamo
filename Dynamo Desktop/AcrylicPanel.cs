using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;

namespace Dynamo_Desktop;

public class AcrylicPanel : Panel
{
    public void ToggleBlur()
    {
        if(Effect == null)
        {
            Effect = new BlurEffect()
            {
                Radius = 30
            };
        }
        else
        {
            Effect = null;
        }
    }
}
