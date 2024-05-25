using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;

internal static class Utils
{
    public static Exception NotInitalizedException([CallerMemberName] string? memberName = null)
    {
        return new InvalidOperationException();
    }
}
