using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Domain.DTO.Generic
{
    public class ResponseDTO
    {
        public bool IsSucess { get; set; }
        public int StatusCode { get; set; }
        public string Mensagem { get; set; }
        public int InternalStatusCode { get; set; }
        public string ErrorLocation { get; set; }

        public ResponseDTO()
        {
            IsSucess = true;
            StatusCode = 200;
            InternalStatusCode = 1;
        }

        public ResponseDTO(bool sucess, int internalStatus = 1, string errorLocation = "")
        {
            Construtor(sucess, internalStatus, errorLocation);
        }

        public void Construtor(bool sucess, int internalStatus = 1, string errorLocation = "")
        {
            IsSucess = sucess;

            if (sucess)
            {
                StatusCode = 200;
                InternalStatusCode = internalStatus;
            }
            else
            {
                StatusCode = 400;
                ErrorLocation = errorLocation;
                InternalStatusCode = internalStatus;
            }
        }
    }
}
