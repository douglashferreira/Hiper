using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Domain.DTO.Generic
{
    public class ReturnRequisitionAPIDTO<TRetorno> where TRetorno : class
    {
        public TRetorno returnRequisitionAPI { get; set; }

        public bool returnSucessRequisition;

        public string ErrorMessage { get; set; }
    }
}
