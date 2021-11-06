using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Service.Services.DTOs.AuthenticationDTOs;

namespace Heka.Service.Services.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        Task<LoginDto> JwtAuthentication(UserCredentialDto userCredential);
    }
}
