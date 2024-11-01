using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.IO;

public static class TokenArrayExtension
{
    public static int GetTextLength(this Token[] array)
    {
        int length = 0;
        for (int i = 0; i < array.Length; i++)
        {
            var text = array[i].Text;
            if (!string.IsNullOrEmpty(text))
            {
                length += text.Length;
            }
        }
        return length;
    }
}
