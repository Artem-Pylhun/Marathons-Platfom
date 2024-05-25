using Marathons_Platfom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platform.Repositories.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
