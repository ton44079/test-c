using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestC.Models;

namespace TestC.Services.Interface
{
    public interface IPropService
    {
        IList<PropModel> Search(ParametersModel model);
    }
}
